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
    [Route("[controller]")]
    public class ICalController : ControllerBase
    {
        private readonly ILogger<ICalController> _logger;
        private readonly Api _api;

        public ICalController(ILogger<ICalController> logger, Api api)
        {
            _logger = logger;
            _api = api;
        }

        [HttpGet]
        public async Task<string> Get(string portalAddress, string username, string password)
        {
            if (portalAddress?.Length > 0)
            {
                _api.PortalAddress = portalAddress;
            }
            await _api.Logon(username, password);

            var timetable = await _api.GetStudentTimetable();
            var student = timetable.Students.First();
            return student.IDNumber;
        }
    }
}
