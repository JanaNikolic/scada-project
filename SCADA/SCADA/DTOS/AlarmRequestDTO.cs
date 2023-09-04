namespace SCADA.DTOS;

public class AlarmRequestDTO
{
    public int TagId { get; set; }
    public int Threshold { get; set; }
    public string Priority { get; set; }
    public string Type { get; set; }

    public AlarmRequestDTO(int analogId, int threshold, string priority, string type)
    {
        TagId = analogId;
        Threshold = threshold;
        Priority = priority;
        Type = type;
    }

    public AlarmRequestDTO()
    {
    }
}