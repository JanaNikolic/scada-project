using System.Text.Json.Serialization;
using System.Xml.Serialization;
using SCADA.DTOS;

namespace SCADA.Model;

public class Alarm
{
    public int Id { get; set; }
    public double Threshold { get; set; }
    public AlarmType Type { get; set; }
    public AlarmPriority Priority { get; set; }
    [JsonIgnore]
    [XmlIgnore]
    public AnalogInput? AnalogInput { get; set; }
    public int TagId { get; set; }
    public DateTime Timestamp { get; set; }

    public Alarm() { }

    public Alarm(double threshold, AlarmType type, AlarmPriority priority, AnalogInput analogInput, int tagId)
    {
        Threshold = threshold;
        Type = type;
        Priority = priority;
        AnalogInput = analogInput;
        TagId = tagId;
        Timestamp = DateTime.UtcNow;
    }

    public Alarm(AlarmRequestDTO alarm)
    {
        Threshold = alarm.Threshold;
        TagId = alarm.TagId;
        Timestamp = DateTime.UtcNow;
    }

    public enum AlarmType
    {
        ABOVE, BELOW
    }
    
    public enum AlarmPriority
    {
        Level1, Level2, Level3
    }
}