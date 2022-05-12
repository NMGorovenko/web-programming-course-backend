using MediatR;

namespace Sfu.Shop.UseCases.Chat.SaveMessage;

/// <summary>
/// Save message command.
/// </summary>
public record SaveMessageCommand : IRequest<Guid>
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
