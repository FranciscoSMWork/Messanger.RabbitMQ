using AutoMapper;
using Messanger.API.Dtos.Users;
using Messanger.Domain.Entity;

namespace Messanger.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>();
    }
}
