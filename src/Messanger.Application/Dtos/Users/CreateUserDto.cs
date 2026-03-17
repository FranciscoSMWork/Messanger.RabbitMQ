using System.ComponentModel.DataAnnotations;

namespace Messanger.API.Dtos.Users;

public class CreateUserDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string RePassword { get; set; }
}
