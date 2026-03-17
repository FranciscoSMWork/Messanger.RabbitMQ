using Messanger.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Messanger.Worker.Consumers;

public class OrderConsumer : BackgroundService
{
    private IConnection _connnection;
    private IChannel _channel;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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

        this._connnection = await factory.CreateConnectionAsync();
        this._channel = await this._connnection.CreateChannelAsync();

        await this._channel.QueueDeclareAsync(queue: "orders",
            durable: true,
            exclusive: false,
            autoDelete: false
        );
    
        var consumer = new AsyncEventingBasicConsumer(this._channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Mensagem recebida: {message}");

            await Task.CompletedTask;
        };

        await this._channel.BasicConsumeAsync(queue: "orders",
            autoAck: true,
            consumer: consumer);

    }

    private async Task ProcessOrder(Order order)
    {
        Console.WriteLine($"Processing order {order.Id}");

        await Task.Delay(30000); // Simula job pesado

        Console.WriteLine($"Order processed!");
    }
}
