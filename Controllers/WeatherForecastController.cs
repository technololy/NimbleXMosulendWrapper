using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MosulendWrapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Interfaces.IDataService dataService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, Interfaces.IDataService dataService)
        {
            _logger = logger;
            this.dataService = dataService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("/GetWeather")]
        public async Task<dynamic> GetWeather()
        {
            var response = await dataService.GetWeatherFromOpenAPI();
            //return (response);
            return Content(JsonConvert.SerializeObject(response), "application/json");
            //return  Ok(Summaries.ToList());
        }
    }
}
