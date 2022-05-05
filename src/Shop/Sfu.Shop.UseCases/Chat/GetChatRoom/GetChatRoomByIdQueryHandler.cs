using AutoMapper;
using MediatR;
using Saritasa.Tools.EFCore;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat.GetChatRoom;


/// <summary>
/// Handler for <see cref="GetChatRoomByIdQuery"/>.
/// </summary>
internal class GetChatRoomByIdQueryHandler : IRequestHandler<GetChatRoomByIdQuery, ChatRoomDto>
{
    private readonly AppDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IMapper mapper;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public GetChatRoomByIdQueryHandler(AppDbContext dbContext, IMapper mapper,  ILoggedUserAccessor loggedUserAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.loggedUserAccessor = loggedUserAccessor;
    }
    
    /// <inheritdoc />
    public async Task<ChatRoomDto> Handle(GetChatRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var chatRoom = await mapper.ProjectTo<ChatRoomDto>(dbContext.ChatRooms)
            .GetAsync(chatRoom => chatRoom.Id == request.Id, cancellationToken);
        
        return chatRoom;
    }
}
