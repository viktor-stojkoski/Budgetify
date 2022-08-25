namespace Budgetify.Functions.Functions;

using System;
using System.IO;
using System.Threading.Tasks;

using Budgetify.Contracts.Settings;
using Budgetify.Contracts.User.Requests;
using Budgetify.Functions.Contracts.AzureADB2C;
using Budgetify.Services.User.Commands;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using VS.Commands;

public class CreateUserFunction
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IAzureADB2CApiConnectorSettings _azureADB2CApiConnectorSettings;
    private readonly ILogger<CreateUserFunction> _logger;

    public CreateUserFunction(
        ICommandDispatcher commandDispatcher,
        IAzureADB2CApiConnectorSettings azureADB2CApiConnectorSettings,
        ILogger<CreateUserFunction> logger)
    {
        _commandDispatcher = commandDispatcher;
        _azureADB2CApiConnectorSettings = azureADB2CApiConnectorSettings;
        _logger = logger;
    }

    [FunctionName(nameof(CreateUserFunction))]
    public async Task<IActionResult> CreateUserAsync(
        [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "users")] HttpRequest request)
    {
        if (!Authorize(request))
        {
            _logger.LogError("HTTP Basic authentication validation failed.");

            return new UnauthorizedResult();
        }

        string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        CreateUserRequest createUserRequest = JsonConvert.DeserializeObject<CreateUserRequest>(requestBody);

        CommandResult<EmptyValue> result =
            await _commandDispatcher.ExecuteAsync(
                new CreateUserCommand(
                    Email: createUserRequest.Email,
                    FirstName: createUserRequest.FirstName,
                    LastName: createUserRequest.LastName,
                    City: createUserRequest.City));

        return result.Value is not null
            ? new OkObjectResult(new ResponseContent())
            : new OkObjectResult(new ResponseContent("ValidationError", result.Message));
    }

    private bool Authorize(HttpRequest request)
    {
        if (!request.Headers.ContainsKey("Authorization"))
        {
            _logger.LogError("Request does not contain Authorization header.");
            return false;
        }

        string authorizationHeaders = request.Headers.Authorization;

        if (!authorizationHeaders.StartsWith("Basic "))
        {
            _logger.LogError("Authorization header does not start with Basic...");
            return false;
        }

        // Get username and password from headers (removes `Basic` from string)
        string[] credentials =
            System.Text.Encoding.UTF8.GetString(
                Convert.FromBase64String(
                    authorizationHeaders[6..])).Split(":");

        return credentials[0] == _azureADB2CApiConnectorSettings.Username
            && credentials[1] == _azureADB2CApiConnectorSettings.Password;
    }
}
