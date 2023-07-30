namespace SCADA.Model;

public class AnalogOutput : Tag
{
    public double InitialValue { get; set; }

    public double LowLimit { get; set; }

    public double HighLimit { get; set; }

    public string Units { get; set; }

    public AnalogOutput(string name, string ioAddress, string description, double initialValue, double lowLimit, double highLimit, string units) : base(name, ioAddress, description)
    {
        InitialValue = initialValue;
        LowLimit = lowLimit;
        HighLimit = highLimit;
        Units = units;
    }
}
