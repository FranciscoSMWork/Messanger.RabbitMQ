using Messanger.Domain.Entity;
namespace Messanger.Application.Abstractions.Repositories;

public interface IOrderRepository
{
    Task<Order> AddAsync(Order order);
    Task<List<Order>> GetAllAsync();
}
