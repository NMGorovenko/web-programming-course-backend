using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.Web.Hubs.HubModels;

/// <summary>
/// User connected to the chat
/// </summary>
public class HubUser
{
    private readonly List<HubConnection> chatConnections;
    

    /// <summary>
    /// All user connections to chat hubs.
    /// </summary>
    public IEnumerable<HubConnection> ChatConnections => chatConnections;

    /// <summary>
    /// User identity name
    /// </summary>
    public UserDto User { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public HubUser(UserDto user)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        chatConnections = new ();
    }
    
    /// <summary>
    /// UTC time connected
    /// </summary>
    public DateTime? ConnectedAt
    {
        get
        {
            if (chatConnections.Any())
            {
                return chatConnections
                    .OrderByDescending(x => x.ConnectedAt)
                    .Select(x => x.ConnectedAt)
                    .First();
            }

            return null;
        }
    }
    
    /// <summary>
    /// Append connection for user.
    /// </summary>
    /// <param name="connectionId">Connection Id.</param>
    public void AppendConnection(string connectionId)
    {
        if (connectionId == null)
        {
            throw new ArgumentNullException(nameof(connectionId));
        }

        var connection = new HubConnection
        {
            ConnectedAt = DateTime.UtcNow,
            ConnectionId = connectionId
        };

        chatConnections.Add(connection);
    }
    
    /// <summary>
    /// Remove connection from user.
    /// </summary>
    public void RemoveConnection(string connectionId)
    {
        if (connectionId == null)
        {
            throw new ArgumentNullException(nameof(connectionId));
        }

        var connection = chatConnections.SingleOrDefault(x => x.ConnectionId.Equals(connectionId));
        if (connection == null)
        {
            return;
        }
        chatConnections.Remove(connection);
    }

    /// <summary>
    /// Append connection to some chat room.
    /// </summary>
    /// <param name="connectionId">Connection Id.</param>
    /// <param name="chatRoomId">Chat room Id.</param>
    public void AddToGroup(string connectionId, Guid chatRoomId)
    {
        var connectionExists = chatConnections
            .FirstOrDefault(connection => connection.ConnectionId == connectionId);

        if (connectionExists == null)
        {
            throw new ArgumentNullException(nameof(connectionId));
        }
        
        connectionExists.AddToGroup(chatRoomId.ToString());
    }
}
