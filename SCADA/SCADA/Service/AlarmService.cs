using SCADA.DTOS;
using SCADA.Model;
using SCADA.Repository.IRepository;
using SCADA.Service.IService;

namespace SCADA.Service;

public class AlarmService : IAlarmService
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ITagRepository _tagRepository;

    public AlarmService(IAlarmRepository alarmRepository, ITagRepository tagRepository)
    {
        _alarmRepository = alarmRepository;
        _tagRepository = tagRepository;
    }

    public List<AlarmResponseDTO> GetAllAlarms()
    {
        
        // Tag? tag = _tagRepository.GetById(1);
        // Alarm alarm = new Alarm(80, Alarm.AlarmType.ABOVE, Alarm.AlarmPriority.Level3, (AnalogInput)tag,
        //     tag.Id);
        // _alarmRepository.AddAlarm(alarm);
        //
        // alarm = new Alarm(60, Alarm.AlarmType.ABOVE, Alarm.AlarmPriority.Level2, (AnalogInput)tag,
        //     tag.Id);
        // _alarmRepository.AddAlarm(alarm);
        //
        // tag = _tagRepository.GetById(2);
        // alarm = new Alarm(30, Alarm.AlarmType.ABOVE, Alarm.AlarmPriority.Level1, (AnalogInput)tag,
        //     tag.Id);
        // _alarmRepository.AddAlarm(alarm);
        
        
        var alarms =  _alarmRepository.GetAllAlarms();
        var ret = new List<AlarmResponseDTO>();
        foreach (var a in alarms)
        {
            ret.Add(new AlarmResponseDTO
                {
                    Id = a.Id, 
                    Priority = a.Priority.ToString(), 
                    Threshold = a.Threshold, 
                    Type = a.Type.ToString(), 
                    TagId = a.TagId,
                    Value = a.AnalogInput.Value
                });
        }

        return ret;
    }

    public List<AlarmActivated> GetAllActivatedAlarms()
    {
        return _alarmRepository.GetAllActivatedAlarms();
    }
    
    public void AddAlarm(Alarm alarm)
    {
        Tag? tag = _tagRepository.GetById(alarm.TagId);
        if (tag == null)
        {
            throw new Exception("Tag with this id doesn't exist");
        }
        alarm.AnalogInput = (AnalogInput)tag;
        _alarmRepository.AddAlarm(alarm);
    }

    public void DeleteAlarm(int id)
    {
        var alarm = _alarmRepository.GetById(id);
        if (alarm == null) {
            throw new Exception("Alarm not found");
        }
        _alarmRepository.DeleteAlarm(alarm);
    }

    public void AddAlarmValue(AlarmActivated alarm, AnalogInput input)
    {
        _alarmRepository.AddActivatedAlarm(alarm);
        LogAlarm(alarm, input);
    }
    
    public List<AlarmActivated> GetActivatedAlarmsByDate(DateTime start, DateTime end)
    {
        return _alarmRepository.GetActivatedAlarmsByDate(start, end);
    }

    public List<AlarmActivated> GetAlarmsByPriority(int priority)
    {
        return _alarmRepository.GetActivatedAlarmsByPriority((Alarm.AlarmPriority)priority);
    }
    
    private void LogAlarm(AlarmActivated alarm, AnalogInput analogInput)
    {
        string fileName = "Logs/alarmLog.txt";
        using(StreamWriter writer = new StreamWriter(fileName, true))
        {
            writer.WriteLine("{0}   Analog input: {1}    Priority: {2}    Type: {3} ", alarm.Timestamp ,analogInput.Name, alarm.Alarm.Priority, alarm.Alarm.Type);
        }
    }
    
    public List<AlarmResponseDTO> GetAllAlarmsForTag(int tagId)
    {
        AnalogInput tag = this._tagRepository.GetAnalogInputById(tagId);
        if (tag == null)
        {
            throw new Exception("There is no tag with given id");
        }

        List<AlarmResponseDTO> dtos = new List<AlarmResponseDTO>();
        foreach (Alarm alarm in tag.Alarms)
        {
            dtos.Add(new AlarmResponseDTO
            {
                Id = alarm.Id,
                Priority = alarm.Priority.ToString(),
                Type = alarm.Type.ToString(),
                Threshold = alarm.Threshold,
                TagId = tag.Id
            });
        }

        return dtos;
    }
}