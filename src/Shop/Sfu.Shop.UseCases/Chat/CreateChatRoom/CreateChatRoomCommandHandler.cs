using MediatR;
using Sfu.Shop.Domain.Chat;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;

namespace Sfu.Shop.UseCases.Chat.CreateChatRoom;

/// <summary>
/// Handler for <see cref="CreateChatRoomCommand"/>.
/// </summary>
public class CreateChatRoomCommandHandler : AsyncRequestHandler<CreateChatRoomCommand>
{
    private readonly AppDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateChatRoomCommandHandler(AppDbContext dbContext, ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc />
    protected override async Task Handle(CreateChatRoomCommand request, CancellationToken cancellationToken)
    {
        var chatRoom = new ChatRoom()
        {
            Name = request.Name,
            CreatedById = loggedUserAccessor.GetCurrentUserId(),
            CreatedAt = DateTime.UtcNow,
        };
        
        await dbContext.ChatRooms.AddAsync(chatRoom, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
