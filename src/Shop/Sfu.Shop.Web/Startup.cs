using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Sfu.Shop.Domain.IdentityEntities;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.Web.Infrastructure.Middlewares;
using Sfu.Shop.Web.Infrastructure.Startup;

namespace Sfu.Shop.Web;

/// <summary>
/// Entry point for ASP.NET Core app.
/// </summary>
public class Startup
{
    private readonly IConfiguration configuration;
    
    /// <summary>
    /// Entry point for web application.
    /// </summary>
    /// <param name="configuration">Global configuration.</param>
    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Configure application services on startup.
    /// </summary>
    /// <param name="services">Services to configure.</param>
    /// <param name="environment">Application environment.</param>
    public void ConfigureServices(IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddControllersWithViews()
            .AddJsonOptions(new JsonOptionsSetup().Setup);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = new PathString("/Auth/Login");
            options.AccessDeniedPath = new PathString($"/Auth/AccessDenied");
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.Cookie.HttpOnly = true;
            options.Cookie.Name = "CookieAuth";

            options.Cookie.SameSite = SameSiteMode.None;
        });

        // CORS.
        string[] frontendOrigin = null;
        services.AddCors(new CorsOptionsSetup(
            environment.IsDevelopment(),
            frontendOrigin
        ).Setup);

        // Identity
        services.AddIdentity<User, AppIdentityRole>(options =>
            {
                // off all required in password in Identity Settings
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AppDbContext>();
        
        // Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("AppDatabase")));
        services.AddAsyncInitializer<DatabaseInitializer>();
        
        // Other dependencies.
        Infrastructure.DependencyInjection.AutoMapperModule.Register(services);
        Infrastructure.DependencyInjection.ApplicationModule.Register(services, configuration);
        Infrastructure.DependencyInjection.MediatRModule.Register(services);
        Infrastructure.DependencyInjection.SystemModule.Register(services);
    }

    /// <summary>
    /// Configure web application.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="environment">Application environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        // Swagger
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCookiePolicy();

        app.UseStaticFiles();
        
        // Custom middlewares.
        app.UseMiddleware<ApiExceptionMiddleware>();
        
        // MVC.
        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseCors(builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials()
        );

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
        });
    }
}
