using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sfu.Shop.Domain.IdentityEntities;

namespace Sfu.Shop.Domain.Chat;

/// <summary>
/// Chat room entity.
/// </summary>
public record ChatRoom
{
    /// <summary>
    /// Id.
    /// </summary>
    [Key]
    public Guid Id { get; init; }
    
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Created by.
    /// </summary>
    public User CreatedBy { get; init; }
    
    /// <summary>
    /// Created by.
    /// </summary>
    public Guid CreatedById { get; init; }
    
    /// <summary>
    /// Created at.
    /// </summary>
    public DateTime CreatedAt { get; init; }
    
    /// <summary>
    /// Deleted at.
    /// </summary>
    public DateTime? DeletedAt { get; init; } 
    
    /// <summary>
    /// Messages.
    /// </summary>
    public IEnumerable<Message> Messages { get; init; }
    
    /// <summary>
    /// Followers.
    /// </summary>
    public IList<User> Followers { get; init; }
}
