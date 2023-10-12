using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PocMTLSClient.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeoplesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PeoplesController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PeoplesController(ILogger<PeoplesController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<People> Get() 
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new People
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("/Peoples/Authorized")]
        public async Task<IEnumerable<People>> GetAuthorized()
        {
            var httpClient = _httpClientFactory.CreateClient("certificateRequired");
            string apiUrl = string.Concat(_configuration.GetSection("apiServiceUrl").Value.ToString(), "WeatherForecast");
            //string apiUrl = @"https://localhost:7269/WeatherForecast";
            var httpResponseMessage = await httpClient.GetAsync(apiUrl);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<People>>(jsonString);
            }

            throw new ApplicationException($"Status code: {httpResponseMessage.StatusCode}");
        }


        [HttpGet("/Peoples/Unauthorized")]
        public async Task<IEnumerable<People>> GetUnAuthorized()
        {
            var httpClient = _httpClientFactory.CreateClient("noCertificate");
            string apiUrl = string.Concat(_configuration.GetSection("apiServiceUrl").Value.ToString(), "WeatherForecast");
            var httpResponseMessage = await httpClient.GetAsync(apiUrl);


            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<People>>(jsonString);
            }

            throw new ApplicationException($"Status code: {httpResponseMessage.StatusCode}");
        }
    }
}
