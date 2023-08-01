using Microsoft.EntityFrameworkCore;
using SCADA.Data;
using SCADA.Model;
using SCADA.Repository.IRepository;

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
        return _dataContext.AnalogInputs.Include(ai => ai.Alarms).ToList();
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
}