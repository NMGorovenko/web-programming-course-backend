using MediatR;

namespace Sfu.Shop.UseCases.Chat.SubscribeToRoom;

/// <summary>
/// Subscribe to chat room command.
/// </summary>
/// <param name="ChatRoomId">Chat room Id.</param>
public record SubscribeToRoomCommand(Guid ChatRoomId) : IRequest;
