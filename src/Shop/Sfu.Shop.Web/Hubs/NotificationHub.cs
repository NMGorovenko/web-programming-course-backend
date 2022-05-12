using MediatR;
using Microsoft.AspNetCore.SignalR;

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
