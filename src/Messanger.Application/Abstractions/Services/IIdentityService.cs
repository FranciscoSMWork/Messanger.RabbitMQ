using Messanger.Domain.Entity;

namespace Messanger.Application.Abstractions.Services;

public interface IIdentityService
{
    Task<User> LoginAsync(string email, string password);
}
