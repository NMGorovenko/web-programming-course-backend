using MediatR;

namespace Sfu.Shop.UseCases.Chat.UnsubscribeFromRoomById;

/// <summary>
/// Command for unsubscribe from some room;
/// </summary>
public record UnsubscribeFromRoomByIdCommand(Guid ChatRoomId) : IRequest;
