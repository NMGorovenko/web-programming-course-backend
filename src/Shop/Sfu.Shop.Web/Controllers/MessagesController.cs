using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sfu.Shop.UseCases.Chat.GetMessagesByChatRoomId;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.Web.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MessagesController
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public MessagesController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    /// <summary>
    /// Get some chat room.
    /// </summary>
    /// <param name="chatRoomId">Chat room id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    [HttpGet("{chatRoomId}")]
    public async Task<IEnumerable<DetailedMessageDto>> Get(Guid chatRoomId, CancellationToken cancellationToken)
        => await mediator.Send(new GetMessagesByChatRoomIdQuery(chatRoomId), cancellationToken);
}
