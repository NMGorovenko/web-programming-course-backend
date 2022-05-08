using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sfu.Shop.UseCases.Chat.CreateChatRoom;
using Sfu.Shop.UseCases.Chat.GetAllChatRooms;
using Sfu.Shop.UseCases.Chat.GetChatRoom;
using Sfu.Shop.UseCases.Chat.SubscribeToRoom;
using Sfu.Shop.UseCases.Chat.UnsubscribeFromRoomById;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ChatController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Get chat rooms by id.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<ChatRoomDto>> GetAll(CancellationToken cancellationToken)
        => await mediator.Send(new GetAllChatRoomsQuery(), cancellationToken);

    /// <summary>
    /// Get some chat room.
    /// </summary>
    /// <param name="chatRoomId">Chat room id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    [HttpGet("{chatRoomId}")]
    public async Task<ChatRoomDto> Get(Guid chatRoomId, CancellationToken cancellationToken)
        => await mediator.Send(new GetChatRoomByIdQuery(chatRoomId), cancellationToken);
    
    /// <summary>
    /// Create chat room.
    /// </summary>
    /// <param name="model">Chat room model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost]
    public async Task CreateChatRoom(CreateChatRoomCommand model, CancellationToken cancellationToken)
        => await mediator.Send(model, cancellationToken);


    /// <summary>
    /// Subscribe to the room.
    /// </summary>
    /// <param name="roomId">Chat room Id.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpPost("{roomId}")]
    public async Task Subscribe(Guid roomId, CancellationToken cancellationToken)
    {
        await mediator.Send(new SubscribeToRoomCommand(roomId), cancellationToken);
    }
    
    /// <summary>
    /// Unsubscribe to the room.
    /// </summary>
    /// <param name="roomId">Chat room Id.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpDelete("{roomId}")]
    public async Task Unsubscribe(Guid roomId, CancellationToken cancellationToken)
    {
        await mediator.Send(new UnsubscribeFromRoomByIdCommand(roomId), cancellationToken);
    }
}
