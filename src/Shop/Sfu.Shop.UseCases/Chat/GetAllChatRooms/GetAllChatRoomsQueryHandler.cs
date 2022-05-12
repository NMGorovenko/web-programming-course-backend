using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat.GetAllChatRooms;

/// <summary>
/// Handler for <see cref="GetAllChatRoomsQuery"/>.
/// </summary>
public class GetAllChatRoomsQueryHandler : IRequestHandler<GetAllChatRoomsQuery, IEnumerable<ChatRoomDto>>
{
    private readonly AppDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IMapper mapper;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public GetAllChatRoomsQueryHandler(AppDbContext dbContext, IMapper mapper,  ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.loggedUserAccessor = loggedUserAccessor;
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<ChatRoomDto>> Handle(GetAllChatRoomsQuery request, CancellationToken cancellationToken)
    {
        var chatRooms = await mapper
            .ProjectTo<ChatRoomDto>(dbContext.ChatRooms)
            .ToListAsync(cancellationToken);

        return chatRooms;
    }
}
