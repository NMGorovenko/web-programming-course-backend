using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sfu.Shop.Domain.IdentityEntities;
using Sfu.Shop.UseCases.Auth.GetMe;
using Sfu.Shop.UseCases.Auth.Login;
using Sfu.Shop.UseCases.Auth.Logout;
using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.Web.Controllers;

/// <summary>
///  Auth Controller.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    /// <summary>
    /// Login of client.
    /// </summary>
    /// <param name="command">Client login command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost]
    public async Task LoginClient(LoginCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// GetMe.
    /// </summary>
    [HttpGet]
    public async Task<UserDto> GetMe(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetMeQuery(), cancellationToken);
    }

    [HttpDelete]
    public async Task Logout(CancellationToken cancellationToken)
    {
        await mediator.Send(new LogoutCommand(), cancellationToken);
    }
}
