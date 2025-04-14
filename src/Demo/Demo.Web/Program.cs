using Autofac.Extensions.DependencyInjection;
using Autofac;
using Demo.Web.Data;
using Demo.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Demo.Web;
using Serilog;
using Serilog.Events;

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

    #region Autofac configuration
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); //Added autofac in dot net
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new WebModule()); //Eikane onek kichu likte hbe tai amra areka file e eikaner sob kichu rakte pari, module hisabe.
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

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
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