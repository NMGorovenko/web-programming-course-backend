using AutoMapper;
using Sfu.Shop.Domain.Chat;
using Sfu.Shop.UseCases.Common.Dtos.Chat;

namespace Sfu.Shop.UseCases.Chat;

/// <summary>
/// Chat profile.
/// </summary>
public class ChatProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ChatProfile()
    {
        CreateMap<Message, MessageDto>();
        CreateMap<ChatRoom, ChatRoomDto>();
    }
}
