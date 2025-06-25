using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Demo.Application.Features.Books.Commands;
using Demo.Domain;
using Demo.Infrastructure;
using Demo.Infrastructure.Extensions;
using Demo.Web;
using Demo.Web.Data;
using Demo.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Reflection;

#region Bootstrap logger
var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.Build();
Log.Logger = new LoggerConfiguration()
.ReadFrom.Configuration(configuration)
.CreateBootstrapLogger();
#endregion

try
{
    Log.Information("Application starting ......");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var migrationAssembly = Assembly.GetExecutingAssembly();

    #region Autofac configuration
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); //Added autofac in dot net
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new WebModule(connectionString, migrationAssembly?.FullName)); //Eikane onek kichu likte hbe tai amra areka file e eikaner sob kichu rakte pari, module hisabe.
    });
    #endregion

    #region Service collection dependency injection
    #region Service collection dependency injection
        builder.Services.AddKeyedScoped<IProduct, Product1>("Config1");
        builder.Services.AddKeyedScoped<IProduct, Product2>("Config2");
    #endregion
    #endregion

    #region Serilog configureation
    builder.Host.UseSerilog((context, lc) =>
        lc.MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(builder.Configuration)
    );
    #endregion

    #region MediatR Configuration
    builder.Services.AddMediatR(cfg => {
        cfg.RegisterServicesFromAssembly(migrationAssembly);
        cfg.RegisterServicesFromAssembly(typeof(BookAddCommand).Assembly);
    });
    #endregion

    #region Automapper Configuration
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    #endregion

    #region Identity Configuration
    builder.Services.AddIdentity();
    #endregion

    #region Authorizatin Configuration
    builder.Services.AddPolicy();
    #endregion

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();


    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

    //Register the dependency
    //builder.Services.AddTransient<IItem, Item>();

    builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

    app.MapRazorPages()
       .WithStaticAssets();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application crashed");
}
finally
{
    Log.CloseAndFlush();
}