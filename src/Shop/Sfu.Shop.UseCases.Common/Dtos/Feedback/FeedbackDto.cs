using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.UseCases.Common.Dtos.Feedback;

public record FeedbackDto
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// User that left feedback.
    /// </summary>
    public UserDto FeedbackUser { get; init; }
    
    /// <summary>
    /// Test of the feedback.
    /// </summary>
    public string Text { get; init; }
    
    /// <summary>
    /// Estimation.
    /// </summary>
    public int Estimation { get; init; }
    
    /// <summary>
    /// Product id.
    /// </summary>
    public Guid ProductId { get; init; }
    
    /// <summary>
    /// Created at.
    /// </summary>
    public DateTime CreatedAt { get; init; }
}