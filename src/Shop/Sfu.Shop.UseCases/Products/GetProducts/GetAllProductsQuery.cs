using MediatR;
using Saritasa.Tools.Common.Pagination;
using Sfu.Shop.UseCases.Common.Dtos.Products;

namespace Sfu.Shop.UseCases.Products.GetProducts;

/// <summary>
/// Get products.
/// </summary>
public record GetAllProductsQuery(int page, int pageSize) : IRequest<PagedList<ProductDto>>;