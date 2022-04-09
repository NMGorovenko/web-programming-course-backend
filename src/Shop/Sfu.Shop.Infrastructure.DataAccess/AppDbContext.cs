using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sfu.Shop.Domain.IdentityEntities;

namespace Sfu.Shop.Infrastructure.DataAccess;

public class AppDbContext : IdentityDbContext<User, AppIdentityRole, Guid>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">Options.</param>
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }
}