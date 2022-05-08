using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat.GetAllSubscribedRoomByUserId;


/// <summary>
/// Handler for <see cref="GetAllSubscribedRoomByUserIdQuery"/>.
/// </summary>
internal class GetAllSubscribedRoomByUserIdQueryHandler : IRequestHandler<GetAllSubscribedRoomByUserIdQuery, IEnumerable<ChatRoomDto>>
{
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public GetAllSubscribedRoomByUserIdQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<ChatRoomDto>> Handle(GetAllSubscribedRoomByUserIdQuery request, CancellationToken cancellationToken)
    {
        var subscriberChatRooms = await mapper.ProjectTo<ChatRoomDto>(dbContext.ChatRooms)
            .Where(chatRoom => chatRoom.Followers.Any(follower => follower.Id == request.UserId))
            .ToListAsync(cancellationToken);

        return subscriberChatRooms;
    }
}
