using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.UseCases.Common.Identity;

namespace Sfu.Shop.Web.Infrastructure.Web;

/// <summary>
/// Logged user accessor implementation.
/// </summary>
internal class LoggedUserAccessor : ILoggedUserAccessor
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public LoggedUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public Guid GetCurrentUserId()
    {
        if (httpContextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("There is no active HTTP context specified.");
        }

        return httpContextAccessor.HttpContext.User.GetCurrentUserId();
    }

    /// <inheritdoc />
    public bool IsAuthenticated()
    {
        if (httpContextAccessor.HttpContext == null)
        {
            return false;
        }
        return httpContextAccessor.HttpContext.User.TryGetCurrentUserId(out _);
    }

    /// <inheritdoc />
    public bool HasClaim(string claimTypes, string claimValue)
    {
        if (httpContextAccessor.HttpContext == null)
        {
            return false;
        }

        return httpContextAccessor.HttpContext.User.HasClaim(claimTypes, claimValue);
    }

    /// <inheritdoc />
    public bool HasPermission(string permission)
    {
        return HasClaim(CustomClaimTypes.Permission, permission);
    }
}