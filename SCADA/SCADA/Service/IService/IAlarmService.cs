using SCADA.DTOS;
using SCADA.Model;

namespace SCADA.Service.IService;

public interface IAlarmService
{
    public Alarm GetById(int alarmId);
    List<AlarmResponseDTO> GetAllAlarms();
    List<AlarmActivated> GetAllActivatedAlarms();
    public void AddAlarm(AlarmRequestDTO alarm);
    public void DeleteAlarm(int id);
    public void AddAlarmValue(AlarmActivated alarm, AnalogInput input);
    public List<AlarmActivated> GetActivatedAlarmsByDate(DateTime start, DateTime end);
    public List<AlarmActivated> GetAlarmsByPriority(int priority);
    public List<AlarmResponseDTO> GetAllAlarmsForTag(int tagId);
}