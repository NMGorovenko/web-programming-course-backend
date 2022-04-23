using AutoMapper;
using Sfu.Shop.Domain.IdentityEntities;
using Sfu.Shop.UseCases.Common.Dtos.User;

namespace Sfu.Shop.UseCases.Auth;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}