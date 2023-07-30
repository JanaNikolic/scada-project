namespace SCADA.Model;

public class DigitalOutput : Tag
{
    public bool InitialValue { get; set; }

    public DigitalOutput(string name, string ioAddress, string description, bool initialValue) : base(name, ioAddress, description)
    {
        InitialValue = initialValue;
    }
}
