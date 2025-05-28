using Autofac.Extensions.DependencyInjection;
using Autofac;
using Demo.Web.Data;
using Demo.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Demo.Web;
using Serilog;
using Serilog.Events;
using System.Reflection;
using Demo.Infrastructure;
using Autofac.Core;
using Demo.Infrastructure.Extensions;
using Demo.Application.Features.Books.Commands;

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

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();


    builder.Services.AddControllersWithViews();

    //Register the dependency
    //builder.Services.AddTransient<IItem, Item>();

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