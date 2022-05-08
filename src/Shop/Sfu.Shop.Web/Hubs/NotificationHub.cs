using MediatR;
using Microsoft.AspNetCore.SignalR;
using Sfu.Shop.UseCases.Auth.GetMe;
using Sfu.Shop.UseCases.Chat.GetAllSubscribedRoomByUserId;
using Sfu.Shop.UseCases.Chat.SubscribeToRoom;
using Sfu.Shop.UseCases.Common.Dtos.User;
using Sfu.Shop.Web.Hubs.HubModels;
using Sfu.Shop.Web.Models;

namespace Sfu.Shop.Web.Hubs;

/// <summary>
/// Hub which sending global notifications.
/// </summary>
public class NotificationHub : Hub
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public NotificationHub(IMediator mediator)
    {
        this.mediator = mediator;
    }
}
