namespace Messanger.Application.Dtos.Orders;
public record CreateOrderDto(string CustomerName, decimal Amount, string userId);
