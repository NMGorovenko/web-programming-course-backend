using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.UseCases.Common.Dtos.Chat;

public record ChatRoomDto
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; init; }
    
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
    /// Followers.
    /// </summary>
    public IEnumerable<UserDto> Followers { get; init; }
}
