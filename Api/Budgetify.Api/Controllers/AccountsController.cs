namespace Budgetify.Api.Controllers
{
    using System.Threading.Tasks;

    using Budgetify.Contracts.User.Requests;
    using Budgetify.Services.User.Commands;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using VS.Commands;

    // TODO: Remove this controller ?
    [Route("api/accounts")]
    [ApiController]
    [AllowAnonymous]
    public class AccountsController : ExtendedApiController
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AccountsController(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;

        [HttpPost]
        [Authorize(AuthenticationSchemes = "")]
        public async Task<IActionResult> SignUp([FromBody] CreateUserRequest request) =>
            OkOrError(await _commandDispatcher.ExecuteAsync(
                new CreateUserCommand(
                    City: request.City,
                    FirstName: request.FirstName,
                    LastName: request.LastName,
                    Email: request.Email)));
    }
}
