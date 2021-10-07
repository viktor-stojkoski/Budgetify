namespace Budgetify.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Budgetify.Common.Commands;
    using Budgetify.Common.Jobs;
    using Budgetify.Common.Queries;
    using Budgetify.Queries;
    using Budgetify.Services.Test;

    using Microsoft.AspNetCore.Http;
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
        private readonly IJobService _jobService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            IJobService jobService)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _jobService = jobService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            Random? rng = new();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpPost("test/{testUid:guid}")]
        //public async Task<IActionResult> Test(Guid testUid)
        //{
        //    CommandResult<Test> result = await _commandDispatcher.ExecuteAsync(new TestCommand(testUid));

        //    return Ok(result);
        //}

        [HttpPost("test-query/{uid:guid}")]
        public async Task<IActionResult> TestQuery(Guid uid)
        {
            QueryResult<Queries.Test.Entities.Test> result =
                await _queryDispatcher.ExecuteAsync(new TestQuery(uid));

            return Ok(result);
        }

        [HttpPost("test-command")]
        public async Task<IActionResult> TestCommand([FromForm] IFormFile file)
        {
            return Ok(await _commandDispatcher.ExecuteAsync(new TestCommand(Guid.NewGuid(), file)));
        }
    }
}
