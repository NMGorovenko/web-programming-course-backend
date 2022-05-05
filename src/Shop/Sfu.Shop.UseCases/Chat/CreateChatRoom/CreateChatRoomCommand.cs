using MediatR;

namespace Sfu.Shop.UseCases.Chat.CreateChatRoom;

/// <summary>
/// Create chat room command.
/// </summary>
public record CreateChatRoomCommand : IRequest
{
    public string Name { get; init; }
}
