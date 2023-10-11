using Microsoft.AspNetCore.Mvc;
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
        public PeoplesController(ILogger<PeoplesController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
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
            var httpResponseMessage = await httpClient.GetAsync("https://pocmtlsserver.local.jeinz/Peoples");

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
            var httpResponseMessage = await httpClient.GetAsync("https://pocmtlsserver.local.jeinz/Peoples");


            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<People>>(jsonString);
            }

            throw new ApplicationException($"Status code: {httpResponseMessage.StatusCode}");
        }
    }
}
