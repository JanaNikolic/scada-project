namespace SCADA.Model;

public class DigitalOutput : Tag
{
    public double InitialValue { get; set; }

    public DigitalOutput() { }

    public DigitalOutput(string name, string ioAddress, string description, double initialValue) : base(name, ioAddress, description)
    {
        InitialValue = initialValue;
    }
}
