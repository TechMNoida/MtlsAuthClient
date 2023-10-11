using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocMTLSServer.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PeoplesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PeoplesController> _logger;

        public PeoplesController(ILogger<PeoplesController> logger)
        {
            _logger = logger;
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
    }
}
