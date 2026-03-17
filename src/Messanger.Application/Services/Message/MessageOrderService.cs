using Messanger.Application.Abstractions.Services;
using Messanger.Application.Abstractions.Services.MessageOrder;
using Messanger.Application.Dtos.Orders;
using Messanger.Domain.Entity;

namespace Messanger.Application.Services.Message;

public class MessageOrderService : IMessageOrderService
{
    private readonly IMessageBus messageBus;
    public MessageOrderService(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
    }

    public async Task ExecuteAsync(CreateOrderDto request)
    {
        var order = new Order(request.CustomerName, request.Amount, request.userId);
        await messageBus.InitializeAsync();
        await messageBus.PublishAsync(order);
    }
}
