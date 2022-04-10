using System.ComponentModel.DataAnnotations;

namespace Sfu.Shop.Domain.Entities;

/// <summary>
/// Shop entity.
/// </summary>
public record Category
{
    /// <summary>
    /// Id.
    /// </summary>
    [Key]
    public Guid Id { get; init; }
    
    /// <summary>
    /// Name of these category.
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Products in this category.
    /// </summary>
    public IEnumerable<Product> Products { get; init; }
}