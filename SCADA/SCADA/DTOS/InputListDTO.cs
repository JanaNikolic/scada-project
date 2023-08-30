using SCADA.Model;

namespace SCADA.DTOS
{
    public class InputListDTO
    {
        public List<AnalogInput> AnalogInputList { get; set; }
        public List<DigitalInput> DigitalInputList { get; set; }

        public InputListDTO() { }
    }
}
