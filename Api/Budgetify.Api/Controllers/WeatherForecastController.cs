namespace Budgetify.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Budgetify.Common.Commands;
    using Budgetify.Common.Queries;
    using Budgetify.Queries;
    using Budgetify.Services.Test;
    using Budgetify.Storage.Test.Entities;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            Random? rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("test/{testUid:guid}")]
        public async Task<IActionResult> Test(Guid testUid)
        {
            CommandResult<Test> result = await _commandDispatcher.ExecuteAsync(new TestCommand(testUid));

            return Ok(result);
        }

        [HttpPost("test-query/{uid:guid}")]
        public async Task<IActionResult> TestQuery(Guid uid)
        {
            QueryResult<Queries.Test.Entities.Test> result =
                await _queryDispatcher.ExecuteAsync(new TestQuery(uid));

            return Ok(result);
        }
    }
}
