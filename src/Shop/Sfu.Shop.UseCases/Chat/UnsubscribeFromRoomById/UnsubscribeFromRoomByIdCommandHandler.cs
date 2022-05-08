using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.EFCore;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;

namespace Sfu.Shop.UseCases.Chat.UnsubscribeFromRoomById;

/// <summary>
/// Handler for <see cref="UnsubscribeFromRoomByIdCommand"/>.
/// </summary>
public class UnsubscribeFromRoomByIdCommandHandler : AsyncRequestHandler<UnsubscribeFromRoomByIdCommand>
{
    private readonly AppDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    
    /// <summary>
    /// Constructor.
    /// </summary>
    public UnsubscribeFromRoomByIdCommandHandler(AppDbContext dbContext, ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
    }
    

    /// <inheritdoc />
    protected override async Task Handle(UnsubscribeFromRoomByIdCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = loggedUserAccessor.GetCurrentUserId();
        var chatRoom = await dbContext.ChatRooms
            .Include(room => room.Followers)
            .GetAsync(chatRoom => chatRoom.Id == request.ChatRoomId, cancellationToken);
        var currentUser = await dbContext.Users.GetAsync(user => user.Id == currentUserId, cancellationToken);
        chatRoom.Followers.Remove(currentUser);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
