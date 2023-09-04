namespace SCADA.Model;

public class AlarmActivated
{
    public int Id { get; set; }

    public DateTime Timestamp { get; set; }

    public Alarm Alarm { get; set; }

    public int AlarmId { get; set; }

    public AlarmActivated() { }

    public AlarmActivated(Alarm alarm)
    {
        Timestamp = DateTime.UtcNow;
        Alarm = alarm;
        AlarmId = alarm.Id;
    }
}