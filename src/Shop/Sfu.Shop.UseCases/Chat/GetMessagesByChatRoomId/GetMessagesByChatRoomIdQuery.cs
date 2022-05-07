using MediatR;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat.GetMessagesByChatRoomId;

/// <summary>
/// Get messages by chat room Id.
/// </summary>
public record GetMessagesByChatRoomIdQuery(Guid ChatRoomId) : IRequest<IEnumerable<DetailedMessageDto>>;
