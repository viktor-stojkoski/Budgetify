namespace Budgetify.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Budgetify.Common.Commands;
    using Budgetify.Services.Test;

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

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
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

        [HttpPost("test")]
        public async Task<IActionResult> Test()
        {
            CommandResult<string> result = await _commandDispatcher.ExecuteAsync(new TestCommand("zdravo"));

            return Ok(result);
        }
    }
}
