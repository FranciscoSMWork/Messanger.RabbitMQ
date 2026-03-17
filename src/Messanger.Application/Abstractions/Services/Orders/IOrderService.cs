using Messanger.Application.Dtos.Orders;
using Messanger.Domain.Entity;

namespace Messanger.Application.Abstractions.Services.Orders;
public interface IOrderService
{
    Task<Order> CreateAsync(CreateOrderDto request);
    Task<List<OrderDto>> ListAsync();
}
