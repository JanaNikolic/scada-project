using SCADA.Data;
using SCADA.Model;
using SCADA.Repository.IRepository;

namespace SCADA.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public User? GetUser(int id)
    {
        return _dataContext.Users.FirstOrDefault(u => u.Id == id);
    }

    public User? GetUser(string email)
    {
        return _dataContext.Users.FirstOrDefault(u => u.Email.Equals(email));
    }

    public User AddUser(User user)
    {
        // var analogTags = new List<AnalogInput>()
        // {
        //     new AnalogInput("Struja", "5","Tag za merelje struje" , "Struja", 5
        //         , true, 0, 50, "mA"),
        //     new AnalogInput("Napon","20", "Tag za merelje napona", "Napon", 2, true, 0, 20, "V"),
        //     new AnalogInput("Snaga", "5", "Tag za merelje snage", "Snaga", 2
        //         , true, 0, 50, "KW"),
        // };
        // _dataContext.AnalogInputs.AddRange(analogTags);
        // _dataContext.SaveChanges();
        //
        // var digitalTags = new List<DigitalInput>()
        // {
        //     //public DigitalInput(string name, string ioAddress, string description, string driver, double scanTime, bool isScanOn) : base(name, ioAddress, description)
        //     new DigitalInput("D1", "5", "Opis", "", 3, true),
        //     new DigitalInput("D2", "3", "Opis", "", 1, true),
        //     new DigitalInput("D3", "8", "Opis", "", 2, true),
        // };
        // _dataContext.DigitalInputs.AddRange(digitalTags);
        // _dataContext.SaveChanges();
        
        var entityEntry = _dataContext.Users.Add(user);
        _dataContext.SaveChanges();
        return entityEntry.Entity;
    }

    public User? GetByEmailAndPassword(string email, string password)
    {
        return _dataContext.Users.FirstOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password));
    }
}