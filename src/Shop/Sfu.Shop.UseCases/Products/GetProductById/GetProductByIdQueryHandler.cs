using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.EFCore;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Dtos.Products;

namespace Sfu.Shop.UseCases.Products.GetProductById;

internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetProductByIdQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await mapper.ProjectTo<ProductDto>(dbContext.Products.AsNoTracking())
            .GetAsync(p => p.Id == request.id,cancellationToken);

        return product;
    }
}