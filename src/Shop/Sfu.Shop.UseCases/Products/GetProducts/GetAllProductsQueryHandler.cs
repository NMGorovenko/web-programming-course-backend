using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Common.Pagination;
using Saritasa.Tools.EFCore.Pagination;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Dtos;
using Sfu.Shop.UseCases.Common.Dtos.Products;

namespace Sfu.Shop.UseCases.Products.GetProducts;

/// <summary>
/// Handler for <inheritdoc cref="GetAllProductsQuery"/>
/// </summary>
internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedList<ProductDto>>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetAllProductsQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<PagedList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productsQuery = mapper.ProjectTo<ProductDto>(dbContext.Products.AsNoTracking()).OrderBy(p => p.Id);
        var pagedProducts = await EFPagedListFactory.FromSourceAsync(productsQuery, request.page, request.pageSize, cancellationToken);
        
        return pagedProducts;
    }
}