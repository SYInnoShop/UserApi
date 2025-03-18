using AzureFunctions.Models;
using Microsoft.Extensions.Logging;

namespace AzureFunctions.Interfaces;

public interface IServiceBusSenderService
{
    Task SendToServiceBus(UserData userData, ILogger logger);
}