using System.Xml.Serialization;

namespace SCADA.Model;

[XmlInclude(typeof(AnalogInput))]
[XmlInclude(typeof(DigitalInput))]
[XmlInclude(typeof(AnalogOutput))]
[XmlInclude(typeof(DigitalOutput))]
public class Tag
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string IOAddress { get; set; }
    
    public string Description { get; set; }
    public double Value { get; set; }

    public bool IsDeleted { get; set; } = false;

    public Tag() { }

    public Tag(string name, string ioAddress, string description)
    {
        Name = name;
        IOAddress = ioAddress;
        Description = description;
        IsDeleted = false;
    }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(IOAddress)}: {IOAddress}, {nameof(Description)}: {Description}, {nameof(Value)}: {Value}, {nameof(IsDeleted)}: {IsDeleted}";
    }
}