using Messanger.Application.Dtos.Orders;

namespace Messanger.Application.Dtos.Users;
public record UserDto(string Id, string UserName, string Email, List<OrderDto> Orders);
