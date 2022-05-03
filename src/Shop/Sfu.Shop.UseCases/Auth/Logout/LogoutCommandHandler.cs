using MediatR;
using Microsoft.AspNetCore.Identity;
using Sfu.Shop.Domain.IdentityEntities;

namespace Sfu.Shop.UseCases.Auth.Logout;

/// <summary>
/// Handler for <see cref="LogoutCommand"/>.
/// </summary>
public class LogoutCommandHandler : AsyncRequestHandler<LogoutCommand>
{
    private SignInManager<User> signInManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    public LogoutCommandHandler(SignInManager<User> signInManager)
    {
        this.signInManager = signInManager;
    }

    /// <inheritdoc />
    protected override async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();
    }
}
