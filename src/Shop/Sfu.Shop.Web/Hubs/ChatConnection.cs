namespace Sfu.Shop.Web.Hubs;

/// <summary>
/// User connection from some device.
/// </summary>
public record ChatConnection
{
    /// <summary>
    /// Registered at time
    /// </summary>
    public DateTime ConnectedAt { get; set; }

    /// <summary>
    /// Connection Id from client
    /// </summary>
    public string ConnectionId { get; set; } = null!;

    /// <summary>
    /// Connection with group.
    /// </summary>
    public IEnumerable<string> GroupNames => groupNames;

    private readonly List<string> groupNames = new();
    
    /// <summary>
    /// Add this connection to group.
    /// </summary>
    /// <param name="groupName">Group name.</param>
    public void AddToGroup(string groupName)
    {
        if (!groupNames.Any(name => name.Equals(groupName)))
        {
            groupNames.Add(groupName);
        }
    }
}
