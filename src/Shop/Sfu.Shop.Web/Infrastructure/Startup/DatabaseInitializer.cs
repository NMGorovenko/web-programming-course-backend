using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Extensions.Hosting.AsyncInitialization;
using Sfu.Shop.Domain.Entities;
using Sfu.Shop.Domain.IdentityEntities;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Identity;
using Sfu.Shop.Web.Infrastructure.Seeders;

namespace Sfu.Shop.Web.Infrastructure.Startup;

/// <summary>
/// Contains database migration helper methods.
/// </summary>
internal sealed class DatabaseInitializer : IAsyncInitializer
{
    private readonly AppDbContext appDbContext;
    private readonly RoleManager<AppIdentityRole> roleManager;
    private readonly UserManager<User> userManager;

    /// <summary>
    /// Database initializer. Performs migration and data seed.
    /// </summary>
    /// <param name="appDbContext">Data context.</param>
    public DatabaseInitializer(AppDbContext appDbContext,
        RoleManager<AppIdentityRole> roleManager,
        UserManager<User> userManager)
    {
        this.appDbContext = appDbContext;
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    /// <inheritdoc />
    public async Task InitializeAsync()
    {
        await appDbContext.Database.MigrateAsync();
        await AddDefaultRolesAsync();
        if (!userManager.Users.Any())
        {
            await SeedUsers(1000);
        }
        
        if (!appDbContext.Products.Any())
        {
            await SeedProducts(120);
        }
        
        await appDbContext.SaveChangesAsync(CancellationToken.None);
    }

    private async Task AddDefaultRolesAsync()
    {
        if (await roleManager.FindByNameAsync(ApplicationRoleNames.Admin) == null)
        {
            await AddRole(ApplicationRoleNames.Admin);
        }
        if (await roleManager.FindByNameAsync(ApplicationRoleNames.User) == null)
        {
            await AddRole(ApplicationRoleNames.User);
        }
    }

    private async Task<AppIdentityRole> AddRole(string roleName)
    {
        var role = new AppIdentityRole();
        await roleManager.SetRoleNameAsync(role, roleName);
        await roleManager.CreateAsync(role);
        return role;
    }

    private async Task SeedUsers(int amount)
    {
        var password = "12345";
        var seeder = new Seeder(userManager, appDbContext);
        for (int i = 0; i < amount; ++i)
        {
            var user = seeder.GenerateUser();
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, ApplicationRoleNames.User);
        }
    }
    
    private async Task SeedProducts(int amount)
    {
        var seeder = new Seeder(userManager, appDbContext);
        for (int i = 0; i < amount; ++i)
        {
            var product = seeder.GenerateProduct();
            await appDbContext.Products.AddAsync(product, CancellationToken.None);
        }
    }
}