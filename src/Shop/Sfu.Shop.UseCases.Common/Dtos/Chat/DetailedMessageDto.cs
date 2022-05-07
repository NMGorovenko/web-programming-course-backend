using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.UseCases.Common.Dtos.Chat;

public record DetailedMessageDto
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Message text.
    /// </summary>
    public string Text { get; init; }
    
    /// <summary>
    /// Created by.
    /// </summary>
    public UserDto CreatedBy { get; init; }
    
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
    public ChatRoomDto ChatRoom { get; init; }
    
    /// <summary>
    /// Associated with chat room id.
    /// </summary>
    public Guid ChatRoomId { get; init; }
}
