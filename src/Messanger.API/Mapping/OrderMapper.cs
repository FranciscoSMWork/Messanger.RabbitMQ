using Messanger.API.Dtos.Orders;
using Messanger.Application.Dtos.Orders;

namespace Messanger.API.Mapping;

public static class OrderMapper
{

    public static CreateOrderDto toCreateDto(this CreateOrderRequest request)
    {
        return new CreateOrderDto(request.CustomerName, request.Amount, request.UserId);
    }
}
