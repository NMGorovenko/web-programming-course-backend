using MediatR;
using Sfu.Shop.UseCases.Common.Dtos.Products;

namespace Sfu.Shop.UseCases.Products.GetProductById;

/// <summary>
/// Get product by id.
/// </summary>
/// <param name="id"></param>
public record GetProductByIdQuery(Guid id) : IRequest<ProductDto>;