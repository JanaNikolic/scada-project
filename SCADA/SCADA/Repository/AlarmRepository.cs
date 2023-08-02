using Microsoft.EntityFrameworkCore;
using SCADA.Data;
using SCADA.Model;
using SCADA.Repository.IRepository;

namespace SCADA.Repository;

public class AlarmRepository : IAlarmRepository
{
    private readonly DataContext _dataContext;
    private readonly ITagRepository _tagRepository;

    public AlarmRepository(DataContext dataContext, ITagRepository tagRepository)
    {
        _dataContext = dataContext;
        _tagRepository = tagRepository;
    }

    public Alarm GetById(int id)
    {
        return _dataContext.Alarms.First(a => a.Id == id);
    }

    public void AddAlarm(Alarm alarm)
    {
        _dataContext.Alarms.Add(alarm);
        _dataContext.Attach(alarm.AnalogInput);
        _dataContext.SaveChanges();
    }

    public void DeleteAlarm(Alarm alarm)
    {
        Tag? tag = _tagRepository.GetById(alarm.TagId);
        if(tag != null)
        {
            var analog = (AnalogInput)tag;
            analog.Alarms.Remove(alarm);
        }
        _dataContext.Entry(alarm).State = EntityState.Deleted;
        _dataContext.SaveChanges();
        
    }

    public void AddActivatedAlarm(AlarmActivated alarm)
    {
        _dataContext.AlarmsActivated.Add(alarm);
        _dataContext.Attach(alarm.Alarm);
        _dataContext.SaveChanges();
    }

    public List<AlarmActivated> GetActivatedAlarmsByDate(DateTime start, DateTime end)
    {
        return _dataContext.AlarmsActivated.Include(a => a.Alarm).Where(a => a.Timestamp >=  start && a.Timestamp <= end).ToList();
    }

    public List<AlarmActivated> GetActivatedAlarmsByPriority(Alarm.AlarmPriority priority)
    {
        return _dataContext.AlarmsActivated.Include(a=>a.Alarm).Where(a => a.Alarm.Priority == priority).ToList();
    }
}