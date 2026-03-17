using Messanger.API.Dtos.Users;
using Messanger.Application.Abstractions.Repositories;
using Messanger.Application.Abstractions.Services.Orders;
using Messanger.Application.Dtos.Orders;
using Messanger.Application.Dtos.Users;
using Messanger.Domain.Entity;
using System.Collections.Generic;

namespace Messanger.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository orderRepository;
    public OrderService(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<Order> CreateAsync(CreateOrderDto dto)
    {
        var order = new Order(dto.CustomerName, dto.Amount, dto.userId);
        return await this.orderRepository.AddAsync(order);
    }

    public async Task<List<OrderDto>> ListAsync()
    {
        List<Order> listOrder = await this.orderRepository.GetAllAsync();
        List<OrderDto> listOrderDto = listOrder.Select(order =>
        {
            UserDto userDto = new UserDto
            (
                order.User.Id,
                order.User.UserName,
                order.User.Email,
                null
            );

            return new OrderDto
            (
                order.Id,
                order.CustomerName,
                order.Amount,
                userDto
            );
        }).ToList();

        return listOrderDto;
    }
}
