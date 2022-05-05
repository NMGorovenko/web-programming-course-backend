using MediatR;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat.GetAllChatRooms;

public record GetAllChatRoomsQuery() : IRequest<IEnumerable<ChatRoomDto>>;
