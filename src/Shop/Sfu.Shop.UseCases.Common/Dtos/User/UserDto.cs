namespace Sfu.Shop.UseCases.Common.Dtos.User;

/// <summary>
/// Dto for user.
/// </summary>
public record UserDto
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; init; }
}
