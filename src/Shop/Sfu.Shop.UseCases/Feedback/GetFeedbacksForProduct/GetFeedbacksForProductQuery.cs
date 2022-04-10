using MediatR;
using Saritasa.Tools.Common.Pagination;
using Sfu.Shop.UseCases.Common.Dtos.Feedback;

namespace Sfu.Shop.UseCases.Feedback.GetFeedbacksForProduct;

/// <summary>
/// Get feedback for some product.
/// </summary>
/// <param name="page">Page.</param>
/// <param name="pageSize">Page size.</param>
/// /// <param name="productId">Product id.</param>
public record GetFeedbacksForProductQuery(Guid productId, int page, int pageSize) : IRequest<PagedList<FeedbackDto>>;