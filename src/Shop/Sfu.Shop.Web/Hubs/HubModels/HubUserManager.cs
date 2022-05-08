using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.Web.Hubs.HubModels;

/// <summary>
/// Chat user manager.
/// </summary>
public class HubUserManager
{
    private List<HubUser> ConnectedUsers { get; } = new();
    
    /// <summary>
    /// Connect user to chat.
    /// </summary>
    public void ConnectUser(UserDto user, string connectionId)
    {
        var userAlreadyExists = GetConnectedUserByUserId(user.Id);
        if (userAlreadyExists != null)
        {
            userAlreadyExists.AppendConnection(connectionId);
            return;
        }

        var chatUser = new HubUser(user);
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

        if (!userExists.ChatConnections.Any())
        {
            return false;
        }

        var connectionExists = userExists.ChatConnections
            .Select(x => x.ConnectionId)
            .First()
            .Equals(connectionId);
        
        if (!connectionExists)
        {
            return false;
        }

        if (userExists.ChatConnections.Count() == 1)
        {
            ConnectedUsers.Remove(userExists);
            return true;
        }

        userExists.RemoveConnection(connectionId);
        
        return false;
    }
    
    /// <summary>
    /// Returns <see cref="HubUser"/> by connectionId if connection found
    /// </summary>
    /// <param name="connectionId"></param>
    /// <returns></returns>
    public HubUser? GetConnectedUserByConnectionId(string connectionId) =>
        ConnectedUsers
            .FirstOrDefault(x => x.ChatConnections.Select(c => c.ConnectionId)
                .Contains(connectionId));
    
    /// <summary>
    /// Returns <see cref="HubUser"/> by userId.
    /// </summary>
    /// <param name="userId">User Id.</param>
    /// <returns>ChatUser?.</returns>
    public HubUser? GetConnectedUserByUserId(Guid userId) =>
        ConnectedUsers.
            FirstOrDefault(x => userId.CompareTo(x.User.Id) == 0);
    
    
    /// <summary>
    /// Get users in by group name.
    /// </summary>
    /// <param name="groupName">Group name.</param>
    /// <returns>User in group.</returns>
    public IEnumerable<UserDto> GetUsersByGroupName(string groupName)
    {
        var usersInGroup = ConnectedUsers.Where(user =>
            user.ChatConnections.Any(connection => connection.GroupNames.Any(name => name.Equals(groupName))))
            .Select(hubuser => hubuser.User);
        return usersInGroup;
    }
}
