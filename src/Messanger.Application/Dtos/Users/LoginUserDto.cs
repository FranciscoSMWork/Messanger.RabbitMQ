using System.ComponentModel.DataAnnotations;

namespace Messanger.Application.Dtos.Users;

public class LoginUserDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
