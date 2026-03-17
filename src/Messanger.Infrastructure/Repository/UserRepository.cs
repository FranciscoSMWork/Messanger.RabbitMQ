using Messanger.Application.Abstractions.Repositories;
using Messanger.Domain.Entity;
using Messanger.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Messanger.Infrastructure.Repository;

public class UserRepository : IUserRepository
{

    private UserManager<User> _userManager;
    private MessangerDbContext context;
    public UserRepository(MessangerDbContext messangerDbContext, UserManager<User> userManager)
    {
        this._userManager = userManager;
        this.context = messangerDbContext;
    }
    public async Task CreateAsync(User user, string password)
    {
        var result = await this._userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new Exception(
                string.Join(", ", result.Errors.Select(e => e.Description))
            );
        }
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await this.context.Users
            .Include(u => u.Orders)
            .AsNoTracking()
            .ToListAsync();
    }
}
