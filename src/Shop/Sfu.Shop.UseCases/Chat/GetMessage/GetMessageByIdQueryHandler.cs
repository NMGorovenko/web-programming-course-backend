using AutoMapper;
using MediatR;
using Saritasa.Tools.EFCore;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat.GetMessage;


/// <summary>
/// Handler for <see cref="GetMessageByIdQuery"/>.
/// </summary>
public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, DetailedMessageDto>
{
    
    private readonly AppDbContext dbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetMessageByIdQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    
    /// <inheritdoc />
    public async Task<DetailedMessageDto> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var message = await mapper.ProjectTo<DetailedMessageDto>(dbContext.Messages)
            .GetAsync(message => message.Id == request.Id, cancellationToken);

        return message;
    }
}
