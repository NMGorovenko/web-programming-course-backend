using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Sfu.Shop.UseCases.Auth.Login;

/// <summary>
/// Login client command.
/// </summary>
public record LoginCommand : IRequest
{
    /// <summary>
    /// Login.
    /// </summary>
    [Required]
    public string Login { get; init; }
    
    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; }
    
    /// <summary>
    /// Remember client for a period.
    /// </summary>
    public bool RememberMe { get; init; }
}