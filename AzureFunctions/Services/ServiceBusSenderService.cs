using Azure.Messaging.ServiceBus;
using AzureFunctions.Interfaces;
using AzureFunctions.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctions.Services;

public class ServiceBusSenderService : IServiceBusSenderService
{
    public async Task SendToServiceBus(UserData userData, ILogger logger)
    {
        string? connectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
        string queueName = "user-data-queue";

        await using var client = new ServiceBusClient(connectionString);
        ServiceBusSender sender = client.CreateSender(queueName);

        string messageBody = JsonConvert.SerializeObject(userData);
        ServiceBusMessage message = new ServiceBusMessage(messageBody);

        await sender.SendMessageAsync(message);
        logger.LogInformation($"Sent user data to Service Bus: {messageBody}");
    }
}