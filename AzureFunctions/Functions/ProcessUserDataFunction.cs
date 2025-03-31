using AzureFunctions.Interfaces;
using AzureFunctions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctions.Functions;

public class ProcessUserDataFunction(IServiceBusSenderService serviceBusSenderService)
{
    [Function("ProcessUserDataFunction")]
    public async Task Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
        FunctionContext context)
    {
        var logger = context.GetLogger("ProcessUserDataFunction");
        logger.LogInformation("HTTP trigger function processed a request.");

        // Read and parse the request body
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var userData = JsonConvert.DeserializeObject<UserData>(requestBody);
        
        try
        {
            // Send user data to Azure Service Bus
            if (userData != null) await serviceBusSenderService.SendToServiceBus(userData, logger);
            logger.LogInformation("User data successfully sent to Azure Service Bus.");
        }
        catch (Exception ex)
        {
            logger.LogError($"Error sending data to Service Bus: {ex.Message}");
        }
    }
}