using System.ComponentModel.DataAnnotations;
using Sfu.Shop.Domain.IdentityEntities;

namespace Sfu.Shop.Domain.Chat;

/// <summary>
/// Message entity.
/// </summary>
public record Message
{
    /// <summary>
    /// Id.
    /// </summary>
    [Key]
    public Guid Id { get; init; }
    
    /// <summary>
    /// Message text.
    /// </summary>
    public string Text { get; init; }
    
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
    /// UpdatedAt at.
    /// </summary>
    public DateTime UpdatedAt { get; init; }
    
    /// <summary>
    /// Deleted at.
    /// </summary>
    public DateTime? DeletedAt { get; init; } 
    
    /// <summary>
    /// Associated with chat room.
    /// </summary>
    public ChatRoom ChatRoom { get; init; }
    
    /// <summary>
    /// Associated with chat room id.
    /// </summary>
    public Guid ChatRoomId { get; init; }
}
