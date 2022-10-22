namespace Budgetify.Functions.Functions;

using System;
using System.IO;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Settings;
using Budgetify.Contracts.User.Repositories;
using Budgetify.Contracts.User.Requests;
using Budgetify.Entities.User.Domain;
using Budgetify.Functions.Contracts.AzureADB2C;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using VS.Commands;

public class UpdateUserClaimsFunction
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IAzureADB2CApiConnectorSettings _azureADB2CApiConnectorSettings;
    private readonly ILogger<UpdateUserClaimsFunction> _logger;
    private readonly IUserRepository _userRepository;

    public UpdateUserClaimsFunction(
        ICommandDispatcher commandDispatcher,
        IAzureADB2CApiConnectorSettings azureADB2CApiConnectorSettings,
        ILogger<UpdateUserClaimsFunction> logger,
        IUserRepository userRepository)
    {
        _commandDispatcher = commandDispatcher;
        _azureADB2CApiConnectorSettings = azureADB2CApiConnectorSettings;
        _logger = logger;
        _userRepository = userRepository;
    }

    [FunctionName("UpdateUserClaimsFunction")]
    public async Task<IActionResult> UpdateUserClaimsAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest request)
    {
        if (!Authorize(request))
        {
            _logger.LogError("Http Basic authentication validation failed.");

            return new UnauthorizedResult();
        }

        string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        UpdateUserClaimsRequest? updateUserClaimsRequest =
            JsonConvert.DeserializeObject<UpdateUserClaimsRequest>(requestBody);

        if (updateUserClaimsRequest is null)
        {
            _logger.LogError("Request invalid.");

            return new BadRequestResult();
        }

        Result<User> userResult = await _userRepository.GetUserAsync(updateUserClaimsRequest.Email);

        if (userResult.IsFailureOrNull)
        {
            _logger.LogError(userResult.Message);

            return new BadRequestResult();
        }

        return new OkObjectResult(new ResponseContent
        {
            Id = userResult.Value.Id
        });
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
