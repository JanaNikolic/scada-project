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
        var entityEntry = _dataContext.Users.Add(user);
        _dataContext.SaveChanges();
        return entityEntry.Entity;
    }

    public User? GetByEmailAndPassword(string email, string password)
    {
        return _dataContext.Users.FirstOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password));
    }
}