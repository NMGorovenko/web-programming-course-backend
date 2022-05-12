using MediatR;
using Microsoft.AspNetCore.SignalR;
using Sfu.Shop.UseCases.Auth.GetMe;
using Sfu.Shop.UseCases.Chat.GetChatRoom;
using Sfu.Shop.UseCases.Chat.GetMessage;
using Sfu.Shop.UseCases.Chat.SaveMessage;
using Sfu.Shop.UseCases.Common.Dtos.Chat;
using Sfu.Shop.Web.Hubs.HubModels;
using Sfu.Shop.Web.Models;

namespace Sfu.Shop.Web.Hubs;

/// <summary>
/// Chat hub.
/// </summary>
public class ChatHub : Hub
{
    private readonly IMediator mediator;
    private readonly HubUserManager hubUserManager;
    private readonly IHubContext<NotificationHub> notificationHub;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ChatHub(IMediator mediator, HubUserManager hubUserManager, IHubContext<NotificationHub> notificationHub)
    {
        this.mediator = mediator;
        this.hubUserManager = hubUserManager;
        this.notificationHub = notificationHub;
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

        var usersInChat = hubUserManager.GetUsersByGroupName(savedMessage.ChatRoomId.ToString());
        var followers =
            (await mediator.Send(new GetChatRoomByIdQuery(savedMessage.ChatRoomId), CancellationToken.None)).Followers;
        var usersToNotify = followers.Except(usersInChat);
        
        await notificationHub.Clients.Users(usersToNotify.Select(user=>user.Id.ToString())).SendAsync("ReceiveNotify", "You are get new message");
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
        var currentChatUser = hubUserManager.GetConnectedUserByConnectionId(connectionId);
        currentChatUser?.AddToGroup(connectionId, chatRoom.ChatRoomId);
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
        hubUserManager.ConnectUser(user, connectionId);
        await base.OnConnectedAsync();
    }

    /// <inheritdoc />
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;
        var chatUser = hubUserManager.GetConnectedUserByConnectionId(connectionId);
        var chatConnection = chatUser?.ChatConnections
            .FirstOrDefault(connection => connection.ConnectionId == connectionId);
        
        foreach (var groupName in chatConnection?.GroupNames ?? Array.Empty<string>())
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync("Notify", $"{chatUser.User.FirstName} left from this channel");
        }
        
        hubUserManager.DisconnectUser(connectionId);
        await base.OnDisconnectedAsync(exception);
    }
}
