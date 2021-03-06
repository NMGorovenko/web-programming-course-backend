using System.ComponentModel.DataAnnotations;

namespace Sfu.Shop.Domain.Entities;

/// <summary>
/// Record entity.
/// </summary>
public record Product
{
    /// <summary>
    /// Id.
    /// </summary>
    [Key]
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
    public IEnumerable<Feedback> Feedback { get; init; }
    
}