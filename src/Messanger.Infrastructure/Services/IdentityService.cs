using Messanger.Application.Abstractions.Services;
using Messanger.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace Messanger.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly SignInManager<User> _signInManager;

    public IdentityService(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<User> LoginAsync(string userName, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(
           userName, password, false, false);

        if (!result.Succeeded)
        {
            throw new ApplicationException("User not authenticated");
        }

        User user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == userName.ToUpper());
        return user;
    }
}
