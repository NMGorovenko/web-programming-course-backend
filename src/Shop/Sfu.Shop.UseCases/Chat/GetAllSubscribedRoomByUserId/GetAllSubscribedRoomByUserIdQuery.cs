using MediatR;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat.GetAllSubscribedRoomByUserId;

/// <summary>
/// Get all subscribed room by user Id.
/// </summary>
public record GetAllSubscribedRoomByUserIdQuery(Guid UserId) : IRequest<IEnumerable<ChatRoomDto>>;
