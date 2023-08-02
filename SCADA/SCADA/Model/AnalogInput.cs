namespace SCADA.Model;

public class AnalogInput : Tag
{
    public string Driver { get; set; }

    public double ScanTime { get; set; }

    public List<Alarm> Alarms { get; set; } = new List<Alarm>();

    public bool IsScanOn { get; set; }

    public double LowLimit { get; set; }

    public double HighLimit { get; set; }

    public string Units { get; set; }

    public AnalogInput() { }

    public AnalogInput(string name, string ioAddress, string description) : base(name, ioAddress, description) { }

    public AnalogInput(string name, string ioAddress, string description, string driver, double scanTime, bool isScanOn, double lowLimit, double highLimit, string units) : base(name, ioAddress, description)
    {
        Driver = driver;
        ScanTime = scanTime;
        IsScanOn = isScanOn;
        LowLimit = lowLimit;
        HighLimit = highLimit;
        Units = units;
    }
}