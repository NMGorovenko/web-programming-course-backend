using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.Web.Hubs;

/// <summary>
/// Chat user manager.
/// </summary>
public class ChatUserManager
{
    private List<ChatUser> ConnectedUsers { get; } = new();
    
    /// <summary>
    /// Connect user to chat.
    /// </summary>
    public void ConnectUser(UserDto user, string connectionId)
    {
        var userAlreadyExists = GetConnectedUserByUserId(user.Id!.Value);
        if (userAlreadyExists != null)
        {
            userAlreadyExists.AppendConnection(connectionId);
            return;
        }

        var chatUser = new ChatUser(user);
        chatUser.AppendConnection(connectionId);
        ConnectedUsers.Add(chatUser);
    }
    
    /// <summary>
    /// Disconnect user from connection.
    /// If we found the connection is last, than we remove user from user list.
    /// </summary>
    /// <param name="connectionId">Connection Id.</param>
    public bool DisconnectUser(string connectionId)
    {
        var userExists = GetConnectedUserByConnectionId(connectionId);
        
        if (userExists == null)
        {
            return false;
        }

        if (!userExists.Connections.Any())
        {
            return false;
        }

        var connectionExists = userExists.Connections
            .Select(x => x.ConnectionId)
            .First()
            .Equals(connectionId);
        
        if (!connectionExists)
        {
            return false;
        }

        if (userExists.Connections.Count() == 1)
        {
            ConnectedUsers.Remove(userExists);
            return true;
        }

        userExists.RemoveConnection(connectionId);
        
        return false;
    }
    
    /// <summary>
    /// Returns <see cref="ChatUser"/> by connectionId if connection found
    /// </summary>
    /// <param name="connectionId"></param>
    /// <returns></returns>
    public ChatUser? GetConnectedUserByConnectionId(string connectionId) =>
        ConnectedUsers
            .FirstOrDefault(x => x.Connections.Select(c => c.ConnectionId)
                .Contains(connectionId));
    
    /// <summary>
    /// Returns <see cref="ChatUser"/> by userId.
    /// </summary>
    /// <param name="userId">User Id.</param>
    /// <returns>ChatUser?.</returns>
    private ChatUser? GetConnectedUserByUserId(Guid userId) =>
        ConnectedUsers.
            FirstOrDefault(x => userId.CompareTo(x.User.Id) == 0);
}
