using AutoMapper;
using Messanger.API.Dtos.Users;
using Messanger.Application.Abstractions.Repositories;
using Messanger.Application.Abstractions.Services;
using Messanger.Application.Dtos.Orders;
using Messanger.Application.Dtos.Users;
using Messanger.Domain.Entity;

namespace Messanger.Application.Services;

public class UserService : IUserService
{
    private IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UserService(
        IMapper mapper,
        IIdentityService identityService,
        IUserRepository userRepository,
        ITokenService tokenService
        )
    {
        _mapper = mapper;
        _identityService = identityService;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task Store(CreateUserDto dto)
    {
        User user = _mapper.Map<User>(dto);
        await _userRepository.CreateAsync(user, dto.Password);
    }

    public async Task<string> Login(LoginUserDto dto)
    {
        User user = await _identityService.LoginAsync(dto.UserName, dto.Password);
        var token = _tokenService.GenerateToken(user);
        return token;
    }

    public async Task<List<UserDto>> ListAsync()
    {
        List<User> listUser = await _userRepository.GetAllAsync();
        List<UserDto> listUserDto = listUser.Select(user =>
        {
            List<OrderDto> listOrderDto = user.Orders.Select(order =>
            {
                return new OrderDto
                (
                    order.Id,
                    order.CustomerName,
                    order.Amount,
                    null
                );
            }).ToList();

            return new UserDto
            (
                user.Id,
                user.UserName,
                user.Email,
                listOrderDto
            );
        }).ToList();

        return listUserDto;
    }
}
