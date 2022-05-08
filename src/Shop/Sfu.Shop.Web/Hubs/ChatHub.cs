using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Sfu.Shop.UseCases.Auth.GetMe;
using Sfu.Shop.UseCases.Chat.GetChatRoom;
using Sfu.Shop.UseCases.Chat.GetMessage;
using Sfu.Shop.UseCases.Chat.SaveMessage;
using Sfu.Shop.UseCases.Common.Dtos.Chat;
using Sfu.Shop.Web.Models;

namespace Sfu.Shop.Web.Hubs;

public class ChatHub : Hub
{
    private readonly IMediator mediator;
    private readonly ChatUserManager chatUserManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ChatHub(IMediator mediator, ChatUserManager chatUserManager)
    {
        this.mediator = mediator;
        this.chatUserManager = chatUserManager;
    }
    
    /// <summary>
    /// Send server method.
    /// </summary>
    /// <param name="message">Message model.</param>
    public async Task Send(SimpleMessageDto message)
    {
        var savedMessageId = await mediator.Send(new SaveMessageCommand
        {
            Text = message.Text,
            ChatRoomId = message.ChatRoomId,
        }, CancellationToken.None);

        var savedMessage = await mediator.Send(new GetMessageByIdQuery(savedMessageId), CancellationToken.None);
        await Clients.Group(savedMessage.ChatRoomId.ToString()).SendAsync("Receive", savedMessage);
    }
    
    /// <summary>
    /// Join to the group.
    /// </summary>
    /// <param name="chatRoom">Contain group Id.</param>
    public async Task Join(ChatRoomModel chatRoom)
    {
        var selectedChatRoom = await mediator.Send(new GetChatRoomByIdQuery(chatRoom.ChatRoomId), CancellationToken.None);
        var user = await mediator.Send(new GetMeQuery(), CancellationToken.None);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, selectedChatRoom.Id.ToString());
        var connectionId = Context.ConnectionId;
        var currentChatUser = chatUserManager.GetConnectedUserByConnectionId(connectionId);
        currentChatUser?.AddToChatRoom(connectionId, chatRoom.ChatRoomId);
        await Clients.OthersInGroup(selectedChatRoom.Id.ToString()).SendAsync("Notify", $"{user.FirstName} join in this channel");
    }
    
    /// <summary>
    /// Left from the group.
    /// </summary>
    /// <param name="chatRoom">Contain group Id.</param>
    public async Task Left(ChatRoomModel chatRoom)
    {
        var selectedChatRoom = await mediator.Send(new GetChatRoomByIdQuery(chatRoom.ChatRoomId), CancellationToken.None);
        var user = await mediator.Send(new GetMeQuery(), CancellationToken.None);
        
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, selectedChatRoom.Id.ToString());
        await Clients.OthersInGroup(selectedChatRoom.Id.ToString()).SendAsync("Notify", $"{user.FirstName} left from this channel");
    }

    /// <inheritdoc />
    public override async Task OnConnectedAsync()
    {
        var user = await mediator.Send(new GetMeQuery(), CancellationToken.None);
        var connectionId = Context.ConnectionId;
        chatUserManager.ConnectUser(user, connectionId);
        await base.OnConnectedAsync();
    }

    /// <inheritdoc />
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;
        var chatUser = chatUserManager.GetConnectedUserByConnectionId(connectionId);
        var chatConnection = chatUser.Connections
            .FirstOrDefault(connection => connection.ConnectionId == connectionId);
        
        foreach (var groupName in chatConnection.GroupNames)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync("Notify", $"{chatUser.User.FirstName} left from this channel");
        }
        
        chatUserManager.DisconnectUser(connectionId);
        await base.OnDisconnectedAsync(exception);
    }
}
