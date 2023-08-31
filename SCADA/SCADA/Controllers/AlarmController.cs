using Microsoft.AspNetCore.Mvc;
using SCADA.Model;
using SCADA.Service.IService;

namespace SCADA.Controllers;

[ApiController]
[Route("[controller]")]
public class AlarmController : ControllerBase
{
    private readonly IAlarmService _alarmService;

    public AlarmController(IAlarmService alarmService)
    {
        _alarmService = alarmService;
    }
    
    [IgnoreAntiforgeryToken]
    [HttpGet]
    public IActionResult getAllAlarms()
    {
        var alarms = _alarmService.GetAllAlarms();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(alarms);
    }
    
    [IgnoreAntiforgeryToken]
    [HttpGet("activated")]
    public IActionResult getAllActivatedAlarms()
    {
        var alarms = _alarmService.GetAllActivatedAlarms();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(alarms);
    }
    
    [IgnoreAntiforgeryToken]
    [HttpGet("{tagId}")]
    public IActionResult getAllAlarmsForTag(int tagId)
    {
        var alarms = _alarmService.GetAllAlarmsForTag(tagId);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(alarms);
    }
    
    [IgnoreAntiforgeryToken]
    [HttpPost]
    public IActionResult AddAlarm([FromBody]Alarm alarm)
    {
        try
        {
            _alarmService.AddAlarm(alarm);
            return Ok();
        }catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [IgnoreAntiforgeryToken]
    [HttpDelete("{id}")]
    public IActionResult DeleteAlarm(int id)
    {
        try
        {
            _alarmService.DeleteAlarm(id);
            return NoContent();
        }catch(Exception e)
        {
            return NotFound(e.Message);
        }
    }
}