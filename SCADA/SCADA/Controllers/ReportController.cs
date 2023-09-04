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
        public IActionResult GetAlarmsInRangeReport([FromBody] TimeRange timeRange)
        {
            return Ok(this._reportService.getAllAlarmsInTimeRange(timeRange));
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("alarms/{priority}")]
        public IActionResult GetAlarmsByPriorityReport(string priority)
        {
            return Ok(_reportService.getAllAlarmsByPriority((AlarmPriority)Enum.Parse(typeof(AlarmPriority), priority)));
        }

        [IgnoreAntiforgeryToken]
        [HttpGet("records-in-range")]
        public IActionResult GetTagRecordsInRangeReport()
        {
            return Ok();
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
        [HttpGet("{id}")]
        public IActionResult GetTagValues(int id)
        {
            return Ok(_reportService.getAllRecordsByTag(id));
        }
    }
}
