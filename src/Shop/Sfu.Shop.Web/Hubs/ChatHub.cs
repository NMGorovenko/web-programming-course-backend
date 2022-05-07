using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sfu.Shop.UseCases.Chat.GetMessage;
using Sfu.Shop.UseCases.Chat.SaveMessage;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.Web.Hubs;

public class ChatHub : Hub
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ChatHub(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    public async Task Send(SimpleMessageDto message)
    {
        var savedMessageId = await mediator.Send(new SaveMessageCommand
        {
            Text = message.Text,
            ChatRoomId = message.ChatRoomId,
        }, CancellationToken.None);

        var savedMessage = await mediator.Send(new GetMessageByIdQuery(savedMessageId), CancellationToken.None);
        await Clients.All.SendAsync("Receive", savedMessage);
    }
}
