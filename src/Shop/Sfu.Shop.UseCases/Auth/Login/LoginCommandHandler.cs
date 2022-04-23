using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Sfu.Shop.Domain.IdentityEntities;

namespace Sfu.Shop.UseCases.Auth.Login;

/// <summary>
/// Login command handler.
/// </summary>
internal class LoginCommandHandler : AsyncRequestHandler<LoginCommand>
{
    // Flag indicating if the user account should be locked if the sign in fails.
    private const bool LockUserOnSignFail = false;
    
    private SignInManager<User> signInManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    public LoginCommandHandler(SignInManager<User> signInManager)
    {
        this.signInManager = signInManager;
    }
    
    /// <inheritdoc />
    protected override async Task Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = 
            await signInManager.PasswordSignInAsync(request.Login, request.Password, 
                request.RememberMe, LockUserOnSignFail);
        if (!result.Succeeded)
        {
            throw new DomainException("Login error. Wrong password or login.");
        }
    }
}