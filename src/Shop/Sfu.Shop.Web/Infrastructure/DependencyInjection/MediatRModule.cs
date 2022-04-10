using MediatR;
using Sfu.Shop.UseCases.Products.GetProducts;

namespace Sfu.Shop.Web.Infrastructure.DependencyInjection;

/// <summary>
/// Register Mediator as dependency.
/// </summary>
internal static class MediatRModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddMediatR(typeof(GetAllProductsQuery).Assembly);
    }
}