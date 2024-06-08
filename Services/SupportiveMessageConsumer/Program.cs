using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SupportiveMessageConsumer.Data; // Import the correct namespace
using System;
using System.Text;

var services = new ServiceCollection();
services.AddDbContext<MessagesContext>(options =>
    options.UseNpgsql("Host=postgresql;Database=messagesdb;Username=postgres;Password=password"));
var serviceProvider = services.BuildServiceProvider();

var factory = new ConnectionFactory() { HostName = "rabbitmq" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare(queue: "supportive_messages", durable: false, exclusive: false, autoDelete: false, arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[x] Received {message}");

    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MessagesContext>();
    context.SupportiveMessages.Add(new SupportiveMessage { Message = message });
    context.SaveChanges();
};

channel.BasicConsume(queue: "supportive_messages", autoAck: true, consumer: consumer);
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();