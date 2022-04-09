using Microsoft.AspNetCore.Identity;

namespace Sfu.Shop.Domain.IdentityEntities;

/// <summary>
/// Custom application identity role.
/// </summary>
public class AppIdentityRole : IdentityRole<Guid>
{
}