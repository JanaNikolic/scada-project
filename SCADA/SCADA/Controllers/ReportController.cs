using Microsoft.AspNetCore.Mvc;

namespace SCADA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        [IgnoreAntiforgeryToken]
        [HttpGet("alarms-in-range")]
        public IActionResult GetAlarmsInRangeReport()
        {
            return Ok();
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("alarms-by-priority")]
        public IActionResult GetAlarmsByPriorityReport()
        {
            return Ok();
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("records-in-range")]
        public IActionResult GetTagRecordsInRangeReport()
        {
            return Ok();
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("ai")]
        public IActionResult GetLastAnalogInputValue()
        {
            return Ok();
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("di")]
        public IActionResult GetLastDigitalInputValue()
        {
            return Ok();
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("{id}")]
        public IActionResult GetTagValues(int id)
        {
            return Ok();
        }
    }
}
