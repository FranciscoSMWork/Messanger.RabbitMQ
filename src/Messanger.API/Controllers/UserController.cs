using Messanger.API.Dtos.Users;
using Messanger.Application.Abstractions.Services;
using Messanger.Application.Dtos.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messanger.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService userService;
    public UserController (IUserService usuarioService)
    {
        userService = usuarioService;
    }

    [HttpPost("store")]
    public async Task<IActionResult> Store ([FromBody] CreateUserRequest request)
    {
        CreateUserDto dto = new CreateUserDto
        {
            Username = request.Username,
            Password = request.Password,
            RePassword = request.RePassword
        };

        await userService.Store(dto);
        return Ok("User created successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        LoginUserDto dto = new LoginUserDto
        {
            UserName = request.Username,
            Password = request.Password,
        };

        string token = await userService.Login(dto);
        return Ok(token);
    }

    [Authorize]
    [HttpGet("list")]
    public async Task<IActionResult> List()
    {
        List<UserDto> listUserDto = await userService.ListAsync();
        return Ok(listUserDto);
    }
}
