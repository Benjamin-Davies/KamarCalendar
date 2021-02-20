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
    public class ICalController : ControllerBase
    {
        private readonly ILogger<ICalController> _logger;
        private readonly Api _api;
        private readonly ICalService _ical;

        public ICalController(ILogger<ICalController> logger, Api api, ICalService ical)
        {
            _logger = logger;
            _api = api;
            _ical = ical;
        }

        [HttpGet("{portalAddress}/{username}/{password}")]
        public async Task<string> Get(string portalAddress, string username, string password)
        {
            if (portalAddress.Length > 1)
            {
                _api.PortalAddress = portalAddress;
            }
            await _api.Logon(username, password);

            var calendar = await _ical.GenerateCalendar();
            return calendar;
        }
    }
}
