using SCADA.Model;

namespace SCADA.Repository.IRepository;

public interface IAlarmRepository
{
    public List<Alarm> GetAllAlarms();
    public List<AlarmActivated> GetAllActivatedAlarms();
    public Alarm GetById(int id);
    public void AddAlarm(Alarm alarm);
    public void DeleteAlarm(Alarm alarm);
    public List<AlarmActivated> GetActivatedAlarmsByDate(DateTime start, DateTime end);
    public List<AlarmActivated> GetActivatedAlarmsByPriority(Alarm.AlarmPriority priority);
    public void AddActivatedAlarm(AlarmActivated alarm);
}