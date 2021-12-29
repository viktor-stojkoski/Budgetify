namespace Budgetify.Api.Controllers
{
    using Budgetify.Common.Results;

    using Microsoft.AspNetCore.Mvc;

    using VS.Commands;
    using VS.Queries;

    [ApiController]
    public class ExtendedApiController : ControllerBase
    {
        protected IActionResult OkOrError<T>(Result<T> result)
        {
            IActionResult? errorResponse = GetErrorResponse(result);

            if (errorResponse is not null)
            {
                return errorResponse;
            }

            return Ok(result);
        }

        protected IActionResult OkOrError(ResultBase result)
        {
            IActionResult? errorResponse = GetErrorResponse(result);

            if (errorResponse is not null)
            {
                return errorResponse;
            }

            return Ok(result);
        }

        protected IActionResult OkOrError<T>(CommandResult<T> result)
        {
            if (result.IsFailure)
            {
                return new ObjectResult(result.Message)
                {
                    StatusCode = (int)result.HttpStatusCode
                };
            }

            return Ok(result);
        }

        protected IActionResult OkOrError<T>(QueryResult<T> result)
        {
            if (result.IsFailure)
            {
                return new ObjectResult(result.Message)
                {
                    StatusCode = (int)result.HttpStatusCode
                };
            }

            return Ok(result);
        }

        private static IActionResult? GetErrorResponse(ResultBase result)
        {
            if (result.IsFailure)
            {
                return new ObjectResult(result.Message)
                {
                    StatusCode = (int)result.HttpStatusCode
                };
            }

            return null;
        }
    }
}
