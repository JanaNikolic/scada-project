namespace SCADA.Model;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

public class Alarm
{
    [Key] public int Id { get; set; }
    public double Threshold { get; set; }
    public AlarmType Type { get; set; }
    public AlarmPriority Priority { get; set; }
    // [JsonIgnore] 
    public AnalogInput? AnalogInput { get; set; }
    public int TagId { get; set; }
    public DateTime Timestamp { get; set; }

    public Alarm(double threshold, AlarmType type, AlarmPriority priority, AnalogInput analogInput, int tagId)
    {
        Threshold = threshold;
        Type = type;
        Priority = priority;
        AnalogInput = analogInput;
        TagId = tagId;
        Timestamp = DateTime.Now;
    }

    public enum AlarmType
    {
        ABOVE, BELOW
    }
    
    public enum AlarmPriority
    {
        Level0, Level1, Level2, Level3, Level4
    }
}