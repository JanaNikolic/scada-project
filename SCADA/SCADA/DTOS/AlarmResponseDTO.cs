namespace SCADA.DTOS;

public class AlarmResponseDTO
{
    public int Id { get; set; }
    public int TagId { get; set; }
    public double Threshold { get; set; }
    public string Priority { get; set; }
    public string Type { get; set; }
    public double Value { get; set; }

    public AlarmResponseDTO() { }

}