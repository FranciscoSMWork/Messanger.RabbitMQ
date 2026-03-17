using Messanger.Application.Dtos.Users;

namespace Messanger.Application.Dtos.Orders;
public record OrderDto(Guid Id, string CustomerName, decimal Amount, UserDto User);
