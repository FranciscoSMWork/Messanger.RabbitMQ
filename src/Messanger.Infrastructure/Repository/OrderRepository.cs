using Messanger.Application.Abstractions.Repositories;
using Messanger.Domain.Entity;
using Messanger.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Messanger.Infrastructure.Repository;
public class OrderRepository : IOrderRepository
{

    private readonly MessangerDbContext context;

    public OrderRepository(MessangerDbContext messangerDbContext)
    {
        this.context = messangerDbContext;
    }

    public async Task<Order> AddAsync(Order order)
    {
        await this.context.Orders.AddAsync(order);
        await this.context.SaveChangesAsync();
        return order;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await this.context.Orders
            .Include(c => c.User)   
            .AsNoTracking()
            .ToListAsync();
    }
}
