using KAMAR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KamarCalendar.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Api _api;

        public HomeController(ILogger<HomeController> logger, Api api)
        {
            _logger = logger;
            _api = api;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var settings = await _api.GetSettings();
            return settings.SchoolName;
        }
    }
}
