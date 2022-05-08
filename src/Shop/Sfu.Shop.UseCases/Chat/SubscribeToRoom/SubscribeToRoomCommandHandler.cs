using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.EFCore;
using Sfu.Shop.Domain.IdentityEntities;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;

namespace Sfu.Shop.UseCases.Chat.SubscribeToRoom;

/// <summary>
/// Handler for <see cref="SubscribeToRoomCommand"/>.
/// </summary>
public class SubscribeToRoomCommandHandler : AsyncRequestHandler<SubscribeToRoomCommand>
{
    private readonly AppDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SubscribeToRoomCommandHandler(AppDbContext dbContext, ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(SubscribeToRoomCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = loggedUserAccessor.GetCurrentUserId();
        var chatRoom = await dbContext.ChatRooms
            .Include(room => room.Followers)
            .GetAsync(chatRoom => chatRoom.Id == request.ChatRoomId, cancellationToken);
        var currentUser = await dbContext.Users.GetAsync(user => user.Id == currentUserId, cancellationToken);
        chatRoom.Followers.Add(currentUser);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
