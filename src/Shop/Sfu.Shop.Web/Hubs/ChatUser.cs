using Sfu.Shop.UseCases.Common.Dtos.Chat;
using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.Web.Hubs;

/// <summary>
/// User connected to the chat
/// </summary>
public class ChatUser
{
    private readonly List<ChatConnection> connections;
    private readonly List<Guid> chatRoomIds;
    
    /// <summary>
    /// All user connections.
    /// </summary>
    public IEnumerable<ChatConnection> Connections => connections;

    /// <summary>
    /// User identity name
    /// </summary>
    public UserDto User { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ChatUser(UserDto user)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        connections = new ();
        chatRoomIds = new();
    }
    
    /// <summary>
    /// UTC time connected
    /// </summary>
    public DateTime? ConnectedAt
    {
        get
        {
            if (connections.Any())
            {
                return connections
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

        var connection = new ChatConnection
        {
            ConnectedAt = DateTime.UtcNow,
            ConnectionId = connectionId
        };

        connections.Add(connection);
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

        var connection = connections.SingleOrDefault(x => x.ConnectionId.Equals(connectionId));
        if (connection == null)
        {
            return;
        }
        connections.Remove(connection);
    }

    /// <summary>
    /// Append connection to some chat room.
    /// </summary>
    /// <param name="connectionId">Connection Id.</param>
    /// <param name="chatRoomId">Chat room Id.</param>
    public void AddToChatRoom(string connectionId, Guid chatRoomId)
    {
        var connectionExists = connections
            .FirstOrDefault(connection => connection.ConnectionId == connectionId);

        if (connectionExists == null)
        {
            throw new ArgumentNullException(nameof(connectionId));
        }
        
        connectionExists.AddToGroup(chatRoomId.ToString());
    }
}
