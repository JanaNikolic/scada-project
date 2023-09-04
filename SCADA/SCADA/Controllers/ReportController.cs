using Microsoft.AspNetCore.Mvc;
using SCADA.Model;
using SCADA.Service.IService;
using System;

namespace SCADA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ITagService _tagService;

        public ReportController(ITagService tagService, IReportService reportService)
        {
            _reportService = reportService;
            _tagService = tagService;
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("alarms-in-range")]
        public IActionResult GetAlarmsInRangeReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(this._reportService.getAllAlarmsInTimeRange(startDate, endDate));
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("alarms/{priority}")]
        public IActionResult GetAlarmsByPriorityReport(string priority)
        {
            return Ok(_reportService.getAllAlarmsByPriority((AlarmPriority)Enum.Parse(typeof(AlarmPriority), priority)));
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("records-in-range")]
        public IActionResult GetTagRecordsInRangeReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(_reportService.getAllRecordsInTimeRange(new TimeRange(startDate.ToUniversalTime(), endDate.ToUniversalTime())));
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("ai")]
        public IActionResult GetLastAnalogInputValues()
        {
            return Ok(_tagService.GetInputTags());
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("di")]
        public IActionResult GetLastDigitalInputValues()
        {
            return Ok(_tagService.GetInputTags());
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("tag/{id}")]
        public IActionResult GetTagValues(int id)
        {
            return Ok(_reportService.getAllRecordsByTag(id));
        }
    }
}
