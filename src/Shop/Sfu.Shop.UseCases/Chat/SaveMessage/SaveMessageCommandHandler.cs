using AutoMapper;
using MediatR;
using Sfu.Shop.Domain.Chat;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;

namespace Sfu.Shop.UseCases.Chat.SaveMessage;

public class SaveMessageCommandHandler : IRequestHandler<SaveMessageCommand, Guid>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SaveMessageCommandHandler(AppDbContext dbContext, IMapper mapper, ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.loggedUserAccessor = loggedUserAccessor;
    }
    
    public async Task<Guid> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new Message()
        {
            Text = request.Text,
            CreatedById = loggedUserAccessor.GetCurrentUserId(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ChatRoomId = request.ChatRoomId,
        };

        await dbContext.Messages.AddAsync(message, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
