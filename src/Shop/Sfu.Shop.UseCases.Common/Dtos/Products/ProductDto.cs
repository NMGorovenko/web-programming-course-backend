namespace Sfu.Shop.UseCases.Common.Dtos.Products;

/// <summary>
/// Dto for product.
/// </summary>
public record ProductDto()
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Title.
    /// </summary>
    public string Title { get; init; }
    
    /// <summary>
    /// Image of the product.
    /// </summary>
    public string ImageUrl { get; init; }
    
    /// <summary>
    /// Price.
    /// </summary>
    public decimal Price { get; init; }
    
    /// <summary>
    /// Feedback for these product.
    /// </summary>
    public double FeedbackScore { get; init; }
    
    /// <summary>
    /// Amount feedbacks.
    /// </summary>
    public int AmountFeedbacks { get; init; }
}