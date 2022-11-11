using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Logger.Models;

class Receive
{
    public static void Main()
    {
        var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

        IConfiguration config = builder.Build();

        var factory = new ConnectionFactory() { Uri = new Uri(config["RabbitMq:Connection"]) };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: config["RabbitMq:Query"],
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var toDoRabbitMessage = JsonSerializer.Deserialize<ToDoRabbitMessage>(Encoding.UTF8.GetString(body));
                Console.WriteLine("***************************");
                if (toDoRabbitMessage == null) Console.WriteLine("Not deserializable message!");
                else
                {
                    var id = toDoRabbitMessage.OldItem == null ? toDoRabbitMessage.NewItem.Id : toDoRabbitMessage.OldItem.Id;
                    Console.WriteLine($"{toDoRabbitMessage.Action.ToString()} action is performed on item #{id}");
                    if (toDoRabbitMessage.OldItem == null) Console.WriteLine($"Old ToDo item is empty");
                    else
                    {
                        Console.WriteLine($"Old ToDo item");
                        PrintToDoItem(toDoRabbitMessage.OldItem);
                    }
                    if (toDoRabbitMessage.NewItem == null) Console.WriteLine($"New ToDo item is empty");
                    else
                    {
                        Console.WriteLine($"New ToDo item");
                        PrintToDoItem(toDoRabbitMessage.NewItem);
                    }
                }
            };
            channel.BasicConsume(queue: config["RabbitMq:Query"],
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    public static void PrintToDoItem(ToDoItem toDoItem)
    {
        Console.WriteLine($"Title: {toDoItem.Title}");
        Console.WriteLine($"Description: {toDoItem.Description}");
        Console.WriteLine($"AccountId: {toDoItem.AccountId}");
        Console.WriteLine($"CreatedBy: {toDoItem.CreatedBy}");
        Console.WriteLine($"CreatedAt: {toDoItem.CreatedAt}");
        Console.WriteLine($"ModifiedBy: {toDoItem.ModifiedBy}");
        Console.WriteLine($"ModifiedOn: {toDoItem.ModifedOn}");
    }
}
