using System.ComponentModel.DataAnnotations;

namespace SCADA.Model;

public class TagRecord
{
    // [Key]
    public int Id { get; set; }
    public Tag Tag { get; set; }

    public int TagId { get; set; }

    public DateTime Timestamp { get; set; }

    public double Value { get; set; }

    public string IOAddress { get; set; }


    public TagRecord() { }

    public TagRecord(Tag tag, double value, string address)
    {
        TagId = tag.Id;
        Tag = tag;
        Timestamp = DateTime.UtcNow;
        Value = value;
        IOAddress = address;
    }
}