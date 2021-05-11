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
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly Api _api;
        private readonly EventsService _events;

        public EventsController(ILogger<EventsController> logger, Api api, EventsService events)
        {
            _logger = logger;
            _api = api;
            _events = events;
        }

        [HttpGet("{portalAddress}")]
        public async Task<string> Get(string portalAddress)
        {
            if (portalAddress.Length > 1)
            {
                _api.PortalAddress = portalAddress;
            }

            var calendar = await _events.GenerateCalendar();
            return calendar;
        }
    }
}
