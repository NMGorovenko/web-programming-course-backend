using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.UseCases.Auth.GetMe;

public class GetMeQueryHandler : IRequestHandler<GetMeQuery, UserDto>
{
    private readonly AppDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetMeQueryHandler(AppDbContext dbContext, ILoggedUserAccessor loggedUserAccessor, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
        this.mapper = mapper;
    }
    
    /// <inheritdoc />
    public async Task<UserDto> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        if (!loggedUserAccessor.IsAuthenticated())
        {
            throw new ForbiddenException();
        }
        var query = dbContext.Users.AsQueryable();
        var userId = loggedUserAccessor.GetCurrentUserId();
        var user = await mapper.ProjectTo<UserDto>(query).GetAsync(x=>x.Id == userId, cancellationToken);

        return user;
    }
}