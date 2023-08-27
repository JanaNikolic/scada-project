namespace SCADA.DTOS
{
    public class OutputDTO
    {
        public string IOAddress { get; set; }
        public double Value { get; set; }

        public OutputDTO() { }
        public OutputDTO(string ioAddress, double value)
        {
            IOAddress = ioAddress;
            Value = value;
        }
    }
}
