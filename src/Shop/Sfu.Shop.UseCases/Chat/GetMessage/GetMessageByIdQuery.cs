using MediatR;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat.GetMessage;

/// <summary>
/// Get message by Id.
/// </summary>
/// <param name="Id">Message Id.</param>
public record GetMessageByIdQuery(Guid Id) : IRequest<DetailedMessageDto>;
