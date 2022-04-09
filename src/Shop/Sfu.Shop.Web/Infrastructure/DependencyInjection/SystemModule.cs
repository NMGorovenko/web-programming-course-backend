using Microsoft.AspNetCore.Mvc.Rendering;
using Sfu.Shop.Web.Infrastructure.Web;

namespace Sfu.Shop.Web.Infrastructure.DependencyInjection;

/// <summary>
/// System specific dependencies.
/// </summary>
internal static class SystemModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IJsonHelper, SystemTextJsonHelper>();
    }
}