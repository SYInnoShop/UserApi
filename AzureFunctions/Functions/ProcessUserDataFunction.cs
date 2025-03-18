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
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        FunctionContext context)
    {
        var logger = context.GetLogger("ProcessUserDataFunction");
        logger.LogInformation("HTTP trigger function processed a request.");

        // Read and parse the request body
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var userData = JsonConvert.DeserializeObject<UserData>(requestBody);

        if (string.IsNullOrEmpty(userData?.Email))
        {
            var badRequestResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            badRequestResponse.WriteString("Email is required.");
            return badRequestResponse;
        }

        // Send user data to Azure Service Bus
        await serviceBusSenderService.SendToServiceBus(userData, logger);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        return response;
    }
}