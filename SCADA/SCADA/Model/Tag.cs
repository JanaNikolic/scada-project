namespace SCADA.Model;

public class Tag
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string IOAddress { get; set; }
    
    public string Description { get; set; }

    public bool isDeleted { get; set; } = false;

    public Tag(string name, string ioAddress, string description)
    {
        Name = name;
        IOAddress = ioAddress;
        Description = description;
        isDeleted = false;
    }
}