using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Common.Pagination;
using Saritasa.Tools.EFCore.Pagination;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Dtos.Feedback;
using Sfu.Shop.UseCases.Common.Dtos.Products;
using Sfu.Shop.UseCases.Products.GetProducts;

namespace Sfu.Shop.UseCases.Feedback.GetFeedbacksForProduct;

public class GetFeedbacksForProductQueryHandler : IRequestHandler<GetFeedbacksForProductQuery, PagedListMetadataDto<FeedbackDto>>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetFeedbacksForProductQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<PagedListMetadataDto<FeedbackDto>> Handle(GetFeedbacksForProductQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = mapper
            .ProjectTo<FeedbackDto>(dbContext.Feedbacks.AsNoTracking())
            .Where(feedback => feedback.ProductId == request.productId)
            .OrderBy(p => p.CreatedAt);
        var pagedProducts = await EFPagedListFactory.FromSourceAsync(feedbacks, request.page, request.pageSize, cancellationToken);
        
        return pagedProducts.ToMetadataObject();
    }
}
