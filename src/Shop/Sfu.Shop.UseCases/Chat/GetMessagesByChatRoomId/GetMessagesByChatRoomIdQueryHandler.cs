using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat.GetMessagesByChatRoomId;

/// <summary>
/// handler for <see cref="GetMessagesByChatRoomIdQuery"/>.
/// </summary>
public class GetMessagesByChatRoomIdQueryHandler : IRequestHandler<GetMessagesByChatRoomIdQuery, IEnumerable<DetailedMessageDto>>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;
    

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetMessagesByChatRoomIdQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<DetailedMessageDto>> Handle(GetMessagesByChatRoomIdQuery request, CancellationToken cancellationToken)
    {
        var messages = await mapper.ProjectTo<DetailedMessageDto>(dbContext.Messages)
            .Where(message => message.ChatRoomId == request.ChatRoomId)
            .ToListAsync(cancellationToken);
        
        return messages;
    }
}
