namespace SCADA.Model;

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
}