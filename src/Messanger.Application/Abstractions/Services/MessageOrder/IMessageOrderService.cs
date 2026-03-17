using Messanger.Application.Dtos.Orders;
namespace Messanger.Application.Abstractions.Services.MessageOrder;

public interface IMessageOrderService
{
    Task ExecuteAsync(CreateOrderDto request);
}
