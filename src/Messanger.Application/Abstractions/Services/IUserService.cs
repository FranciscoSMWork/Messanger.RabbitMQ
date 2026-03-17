using Messanger.API.Dtos.Users;
using Messanger.Application.Dtos.Users;

namespace Messanger.Application.Abstractions.Services;

public interface IUserService
{
    Task Store(CreateUserDto dto);
    Task<string> Login(LoginUserDto dto);
    Task<List<UserDto>> ListAsync();
}
