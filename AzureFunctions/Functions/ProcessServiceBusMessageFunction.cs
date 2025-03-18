using AzureFunctions.Models;
using AzureFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctions.Functions;

public class ProcessServiceBusMessageFunction(SaveToSqlService saveToSqlService)
{
    [Function("ProcessServiceBusMessageFunction")]
    public async Task Run(
        [ServiceBusTrigger("user-data-queue", Connection = "ServiceBusConnectionString")] string queueItem,
        FunctionContext context)
    {
        var logger = context.GetLogger("ProcessServiceBusMessageFunction");
        logger.LogInformation($"Received message: {queueItem}");

        var userData = JsonConvert.DeserializeObject<UserData>(queueItem);

        if (userData != null) await saveToSqlService.SaveToSqlDatabase(userData, logger);
    }
}