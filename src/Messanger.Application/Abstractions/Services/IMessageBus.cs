namespace Messanger.Application.Abstractions.Services;

public interface IMessageBus
{
    Task InitializeAsync();
    Task PublishAsync<TMessage>(TMessage message);
}
