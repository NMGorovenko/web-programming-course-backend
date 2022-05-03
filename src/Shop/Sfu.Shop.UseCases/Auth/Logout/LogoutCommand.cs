using MediatR;

namespace Sfu.Shop.UseCases.Auth.Logout;

/// <summary>
/// Logout command.
/// </summary>
public record LogoutCommand : IRequest;
