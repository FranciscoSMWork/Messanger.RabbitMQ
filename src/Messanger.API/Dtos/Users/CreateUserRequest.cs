using System.ComponentModel.DataAnnotations;

namespace Messanger.API.Dtos.Users;

public class CreateUserRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string RePassword { get; set; }
}
