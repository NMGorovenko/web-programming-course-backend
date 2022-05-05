using MediatR;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat.GetChatRoom;


/// <summary>
/// Get chat room by id query.
/// </summary>
public record GetChatRoomByIdQuery(Guid Id) : IRequest<ChatRoomDto>;
