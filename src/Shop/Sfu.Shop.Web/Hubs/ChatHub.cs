using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Sfu.Shop.Web.Hubs;


public class ChatHub : Hub
{
    public async Task Send(string message, string userName)
    {
        await Clients.All.SendAsync("Receive", message, userName);
    }
}
