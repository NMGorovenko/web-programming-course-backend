namespace Sfu.Shop.UseCases.Common.Dtos.Chat;

/// <summary>
/// Message dto for getting from frontend.
/// </summary>
public record SimpleMessageDto
{
    /// <summary>
    /// Message text.
    /// </summary>
    public string Text { get; init; }
    
    /// <summary>
    /// Associated with chat room id.
    /// </summary>
    public Guid ChatRoomId { get; init; }
}
