using MediatR;

namespace Sfu.Shop.UseCases.Feedback.AddFeedbackForProduct;


/// <summary>
/// Add feedback for product command.
/// </summary>
public record AddFeedbackForProductCommand : IRequest
{
    
    /// <summary>
    /// Product id.
    /// </summary>
    public Guid ProductId { get; init; }
    
    /// <summary>
    /// Text.
    /// </summary>
    public string Text { get; init; }
    
    /// <summary>
    /// Estimation.
    /// </summary>
    public int Estimation { get; init; }
}