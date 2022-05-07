using AutoMapper;
using MediatR;
using Sfu.Shop.Infrastructure.Abstractions.Interfaces;
using Sfu.Shop.Infrastructure.DataAccess;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat.SaveMessage;

/// <summary>
/// Save message command.
/// </summary>
public record SaveMessageCommand : IRequest<Guid>
{
    /// <summary>
    /// Message text.
    /// </summary>
    public string Text { get; init; }
    
    /// <summary>
    /// Associated with chat room id.
    /// </summary>
    public Guid ChatRoomId { get; init; }
}
