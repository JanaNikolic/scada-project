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
    public List<Tag> GetInputs();
    public Task<List<Tag>> GetInputsAsync();
    public Task<Tag?> GetInputByAddress(string address);
    public void AddTagRecord(TagRecord tagRecord);
    public AnalogInput GetAnalogInputById(int tagId);
    public void UpdateTag(Tag tag);
    public Task<TagRecord?> GetTagRecordByAddress(string address);
    public Task<List<Tag>> GetSimulationDriverTags();
    public Task<List<Tag>> GetRTUInputsAsync();
}