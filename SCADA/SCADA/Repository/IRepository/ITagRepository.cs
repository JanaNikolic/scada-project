using SCADA.Model;

namespace SCADA.Repository.IRepository;

public interface ITagRepository
{
    public List<Tag> GetAll();
    public Tag? GetById(int id);
    public List<AnalogInput> GetAnalogInputs();
    public List<AnalogOutput> GetAnalogOutputs();
    public List<DigitalInput> GetDigitalInputs();
    public List<DigitalOutput> GetDigitalOutputs();
    
}