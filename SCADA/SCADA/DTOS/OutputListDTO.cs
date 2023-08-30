using SCADA.Model;

namespace SCADA.DTOS
{
    public class OutputListDTO
    {
        public List<AnalogOutput> AnalogOutputList { get; set; }
        public List<DigitalOutput> DigitalOutputList { get; set; }

        public OutputListDTO() { }
    }
}
