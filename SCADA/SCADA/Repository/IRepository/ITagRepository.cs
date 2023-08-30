using SCADA.DTOS;
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
    public AnalogInput AddAnalogInputTag(AnalogInput analogInput);
    public AnalogOutput AddAnalogOutputTag(AnalogOutput analogOutput);
    public DigitalInput AddDigitalInputTag(DigitalInput digitalInput);
    public DigitalOutput AddDigitalOutputTag(DigitalOutput digitalOutput);
    public void AddOutputValue(OutputDTO value);
    public void RemoveTag(Tag tag);
    public AnalogInput UpdateAnalogInputScan(int id);
    public DigitalInput UpdateDigitalInputScan(int id);


}