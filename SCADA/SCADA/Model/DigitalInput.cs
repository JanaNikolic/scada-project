namespace SCADA.Model;

public class DigitalInput : Tag
{
    public string Driver { get; set; }

    public double ScanTime { get; set; }

    public bool IsScanOn { get; set; }

    public DigitalInput(string name, string ioAddress, string description, string driver, double scanTime, bool isScanOn) : base(name, ioAddress, description)
    {
        Driver = driver;
        ScanTime = scanTime;
        IsScanOn = isScanOn;
    }
}
