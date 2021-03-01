using KAMAR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KamarCalendar.Services;

namespace KamarCalendar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogonController : ControllerBase
    {
        private readonly ILogger<LogonController> _logger;
        private readonly Api _api;

        public LogonController(ILogger<LogonController> logger, Api api)
        {
            _logger = logger;
            _api = api;
        }

        [HttpGet("{portalAddress}/{username}/{password}")]
        public async Task<string> Get(string portalAddress, string username, string password)
        {
            if (portalAddress.Length > 1)
            {
                _api.PortalAddress = portalAddress;
            }
            await _api.Logon(username, password);

            return _api.Key;
        }
    }
}
