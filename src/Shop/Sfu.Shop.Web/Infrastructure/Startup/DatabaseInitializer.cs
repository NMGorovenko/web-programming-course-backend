using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Extensions.Hosting.AsyncInitialization;
using Sfu.Shop.Domain.IdentityEntities;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Identity;

namespace Sfu.Shop.Web.Infrastructure.Startup;

/// <summary>
/// Contains database migration helper methods.
/// </summary>
internal sealed class DatabaseInitializer : IAsyncInitializer
{
    private readonly AppDbContext appDbContext;
    private readonly RoleManager<AppIdentityRole> roleManager;

    /// <summary>
    /// Database initializer. Performs migration and data seed.
    /// </summary>
    /// <param name="appDbContext">Data context.</param>
    public DatabaseInitializer(AppDbContext appDbContext,
        RoleManager<AppIdentityRole> roleManager)
    {
        this.appDbContext = appDbContext;
        this.roleManager = roleManager;
    }

    /// <inheritdoc />
    public async Task InitializeAsync()
    {
        await appDbContext.Database.MigrateAsync();
        await AddDefaultRolesAsync();
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
}