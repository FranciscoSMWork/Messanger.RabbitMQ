using Messanger.Application.Abstractions.Services;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Messanger.Infrastructure.Services.RabbitMq;

public class RabbitMqService : IMessageBus
{
    private IConnection _connection;
    private IChannel _channel;

    public async Task InitializeAsync()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/",
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
        };

        this._connection = await factory.CreateConnectionAsync();
        this._channel = await this._connection.CreateChannelAsync();

        await this._channel.QueueDeclareAsync(
            queue: "orders",
            durable: true,
            exclusive: false,
            autoDelete: false
        );
    }

    public async Task PublishAsync<TMessage>(TMessage message)
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await this._channel.BasicPublishAsync(
            exchange: "",
            routingKey: "orders",
            body: body
         );
    }
}
