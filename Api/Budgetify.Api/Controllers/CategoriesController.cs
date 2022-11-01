namespace Budgetify.Api.Controllers;

using System;
using System.Threading.Tasks;

using Budgetify.Contracts.Category.Requests;
using Budgetify.Queries.Category.Queries.GetCategories;
using Budgetify.Queries.Category.Queries.GetCategory;
using Budgetify.Services.Category.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Commands;
using VS.Queries;

[Route("api/categories")]
[Authorize]
[ApiController]
public class CategoriesController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public CategoriesController(
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetCategoriesQuery()));

    [HttpGet("{categoryUid:Guid}")]
    public async Task<IActionResult> GetCategoryAsync([FromRoute] Guid categoryUid) =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetCategoryQuery(categoryUid)));

    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new CreateCategoryCommand(
                Name: request.Name,
                Type: request.Type)));

    [HttpPut("{categoryUid:Guid}")]
    public async Task<IActionResult> UpdateCategoryAsync(
        [FromRoute] Guid categoryUid,
        [FromBody] UpdateCategoryRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new UpdateCategoryCommand(
                CategoryUid: categoryUid,
                Name: request.Name,
                Type: request.Type)));

    [HttpDelete("{categoryUid:Guid}")]
    public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid categoryUid) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(new DeleteCategoryCommand(categoryUid)));
}
