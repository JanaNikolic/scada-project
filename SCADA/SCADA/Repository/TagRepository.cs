using Microsoft.EntityFrameworkCore;
using SCADA.Data;
using SCADA.DTOS;
using SCADA.Model;
using SCADA.Repository.IRepository;
using System.Data.Entity;
using System.Net;

namespace SCADA.Repository;

public class TagRepository : ITagRepository
{
    private readonly DataContext _dataContext;

    public TagRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public List<Tag> GetAll()
    {
        List<Tag> allTags = new List<Tag>(GetAnalogInputs());
        allTags.AddRange(GetDigitalInputs());
        allTags.AddRange(GetDigitalOutputs());
        allTags.AddRange(GetAnalogOutputs());

        return allTags;
    }
    public Tag? GetById(int id)
    {
        return GetAll().FirstOrDefault(x => x.Id == id);
    }

    public List<AnalogInput> GetAnalogInputs()
    {
        return _dataContext.AnalogInputs.ToList();
    }

    public List<AnalogOutput> GetAnalogOutputs()
    {
        return _dataContext.AnalogOutputs.ToList();
    }

    public List<DigitalInput> GetDigitalInputs()
    {
        return _dataContext.DigitalInputs.ToList();
    }

    public List<DigitalOutput> GetDigitalOutputs()
    {
        return _dataContext.DigitalOutputs.ToList();
    }

    public AnalogInput AddAnalogInputTag(AnalogInput value)
    {
        _dataContext.AnalogInputs.Add(value);
        _dataContext.SaveChanges();
        return value;
    }

    public AnalogOutput AddAnalogOutputTag(AnalogOutput value)
    {
        _dataContext.AnalogOutputs.Add(value);
        _dataContext.SaveChanges();
        return value;
    }

    public DigitalInput AddDigitalInputTag(DigitalInput value)
    {
        _dataContext.DigitalInputs.Add(value);
        _dataContext.SaveChanges();
        return value;
    }

    public DigitalOutput AddDigitalOutputTag(DigitalOutput value)
    {
        _dataContext.DigitalOutputs.Add(value);
        _dataContext.SaveChanges();
        return value;
    }

    public void RemoveTag(Tag tag)
    {
        if (tag is AnalogInput analogInput)
        {
            foreach (var alarm in analogInput.Alarms)
            {
                _dataContext.Entry(alarm).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }
        }
        _dataContext.Entry(tag).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        _dataContext.SaveChanges();
    }

    public List<Tag> GetInputs()
    {
        List<Tag> tags = new List<Tag>();
        tags.AddRange(GetAnalogInputs());
        tags.AddRange(GetDigitalInputs());
        return tags;
    }

    public void AddOutputValue(OutputDTO value)
    {
        Tag tag = GetAll().Find(a => a.IOAddress == value.IOAddress);
        tag.Value = value.Value;
        _dataContext.SaveChanges();
    }


    public AnalogInput UpdateAnalogInputScan(int id)
    {
        AnalogInput analogInput = _dataContext.AnalogInputs.Find(id);
        analogInput.IsScanOn = !analogInput.IsScanOn;
        _dataContext.SaveChanges();
        return analogInput;
    }

    public DigitalInput UpdateDigitalInputScan(int id)
    {
        DigitalInput digitalInput = _dataContext.DigitalInputs.Find(id);
        digitalInput.IsScanOn = !digitalInput.IsScanOn;
        _dataContext.SaveChanges();
        return digitalInput;
    }

    public async Task<List<Tag>> GetInputsAsync()
    {
        return await Task.Run(() =>
        {
            List<Tag> allTags = new List<Tag>();
            allTags.AddRange(GetAnalogInputs().Cast<Tag>());
            allTags.AddRange(GetDigitalInputs().Cast<Tag>());

            return allTags;
        });
    }

    public async Task<Tag?> GetInputByAddress(string address)
    {
        List<Tag> allTags = await GetInputsAsync();
        return allTags.FirstOrDefault(t => t.IOAddress == address);
    }

    public void AddTagRecord(TagRecord tagRecord)
    {
        _dataContext.TagRecords.Add(tagRecord);
        _dataContext.SaveChanges();
    }

    public AnalogInput GetAnalogInputById(int tagId)
    {
        return _dataContext.AnalogInputs.Find(tagId);
    }

    public void UpdateTag(Tag tag)
    {
        if (tag is AnalogInput analogInput)
        {
            _dataContext.AnalogInputs.Update(analogInput);
            _dataContext.SaveChanges();
        } else if (tag is DigitalInput digitalInput)
        {
            _dataContext.DigitalInputs.Update(digitalInput);
            _dataContext.SaveChanges();
        } //TODO

    }
}