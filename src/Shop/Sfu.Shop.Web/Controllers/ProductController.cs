using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Common.Pagination;
using Sfu.Shop.UseCases.Common.Dtos.Feedback;
using Sfu.Shop.UseCases.Common.Dtos.Products;
using Sfu.Shop.UseCases.Feedback.AddFeedbackForProduct;
using Sfu.Shop.UseCases.Feedback.GetFeedbacksForProduct;
using Sfu.Shop.UseCases.Products.GetProductById;
using Sfu.Shop.UseCases.Products.GetProducts;

namespace Sfu.Shop.Web.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ProductController(IMapper mapper, IMediator mediator)
    {
        this.mapper = mapper;
        this.mediator = mediator;
    }

    /// <summary>
    /// Get all products with pagination.
    /// </summary>
    /// <param name="page">Page.</param>
    /// <param name="pageSize">Page size.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    [HttpGet]
    public async Task<PagedListMetadataDto<ProductDto>> Get(int page = 1, int pageSize = 20, CancellationToken cancellationToken = default) =>
        (await mediator.Send(new GetAllProductsQuery(page, pageSize), cancellationToken)).ToMetadataObject();

    /// <summary>
    /// Get product by id.
    /// </summary>
    /// <param name="productId">Product id.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    [HttpGet("{productId}")]
    public async Task<ProductDto> Get(Guid productId, CancellationToken cancellationToken) => 
        await mediator.Send(new GetProductByIdQuery(productId), cancellationToken);
    
    /// <summary>
    /// Get feedback for specific product.
    /// </summary>
    /// <param name="productId">Product id.</param>
    /// <param name="page">Page.</param>
    /// <param name="pageSize">Page size.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns></returns>
    [HttpGet("{productId}/feedbacks")]
    public async Task<PagedListMetadataDto<FeedbackDto>> GetFeedbacks(Guid productId, int page = 1, int pageSize = 20, CancellationToken cancellationToken = default) => 
        await mediator.Send(new GetFeedbacksForProductQuery(productId, page, pageSize), cancellationToken);



    /// <summary>
    /// Add feedback to product.
    /// </summary>
    [Authorize]
    [HttpPost("{productId}/feedbacks")]
    public async Task AddFeedback(AddFeedbackForProductCommand model, CancellationToken cancellationToken) =>
        await mediator.Send(model, cancellationToken);


}
