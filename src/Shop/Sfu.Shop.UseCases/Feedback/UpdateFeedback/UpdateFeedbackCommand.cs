using MediatR;

namespace Sfu.Shop.UseCases.Feedback.UpdateFeedback;

/// <summary>
/// Update feedback command.
/// </summary>
public record UpdateFeedbackCommand : IRequest
{
    /// <summary>
    /// Product id.
    /// </summary>
    public Guid FeedbackId { get; init; }
    
    /// <summary>
    /// Text.
    /// </summary>
    public string Text { get; init; }
    
    /// <summary>
    /// Estimation.
    /// </summary>
    public int Estimation { get; init; }
}
