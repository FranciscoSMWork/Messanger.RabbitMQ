using Messanger.Domain.Entity;
namespace Messanger.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task CreateAsync(User user, string password);
    Task<List<User>> GetAllAsync();
}
