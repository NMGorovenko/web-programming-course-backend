using System.ComponentModel.DataAnnotations;
using Sfu.Shop.Domain.IdentityEntities;

namespace Sfu.Shop.Domain.Entities;

/// <summary>
/// Feedback entity.
/// </summary>
public record Feedback
{
    /// <summary>
    /// Id.
    /// </summary>
    [Key]
    public Guid Id { get; init; }
    
    /// <summary>
    /// User that left feedback.
    /// </summary>
    public User FeedbackUser { get; init; }
    
    /// <summary>
    /// User that left feedback.
    /// </summary>
    public Guid FeedbackUserId { get; init; }
    
    /// <summary>
    /// Test of the feedback.
    /// </summary>
    public string Text { get; init; }
    
    /// <summary>
    /// Estimation.
    /// </summary>
    [Range(1, 5)]
    public int Estimation { get; init; }
    
    /// <summary>
    /// Product.
    /// </summary>
    public Product Product { get; init; }
    
    /// <summary>
    /// Product id.
    /// </summary>
    public Guid ProductId { get; init; }
}