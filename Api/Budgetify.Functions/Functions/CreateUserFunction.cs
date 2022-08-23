namespace Budgetify.Functions.Functions;

using System;
using System.IO;
using System.Threading.Tasks;

using Budgetify.Contracts.Settings;
using Budgetify.Contracts.User.Requests;
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
    private readonly IAzureADB2CSettings _azureADB2CSettings;
    private readonly ILogger<CreateUserFunction> _logger;

    public CreateUserFunction(
        ICommandDispatcher commandDispatcher,
        IAzureADB2CSettings azureADB2CSettings,
        ILogger<CreateUserFunction> logger)
    {
        _commandDispatcher = commandDispatcher;
        _azureADB2CSettings = azureADB2CSettings;
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
        _logger.LogInformation(requestBody); // TODO: Remove
        CreateUserRequest createUserRequest = JsonConvert.DeserializeObject<CreateUserRequest>(requestBody);

        CommandResult<EmptyValue>? result =
            await _commandDispatcher.ExecuteAsync(
                new CreateUserCommand(
                    Email: createUserRequest.Email,
                    FirstName: createUserRequest.FirstName,
                    LastName: createUserRequest.LastName,
                    City: createUserRequest.City));

        return result is not null
            ? new OkObjectResult(result.Value)
            : new BadRequestObjectResult("");
    }

    private bool Authorize(HttpRequest request)
    {
        if (!request.Headers.ContainsKey("Authorization"))
        {
            return false;
        }

        string authorizationHeaders = request.Headers.Authorization;

        _logger.LogInformation(authorizationHeaders); // TODO: Remove

        if (!authorizationHeaders.StartsWith("Basic "))
        {
            return false;
        }

        // Get username and password from headers (removes `Basic` from string)
        string[] credentials =
            System.Text.Encoding.UTF8.GetString(
                Convert.FromBase64String(
                    authorizationHeaders[6..])).Split(":");

        return credentials[0] == _azureADB2CSettings.ApiConnectorUsername
            && credentials[1] == _azureADB2CSettings.ApiConnectorPassword;
    }
}
