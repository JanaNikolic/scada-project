using SCADA.Model;

namespace SCADA.Repository.IRepository;

public interface IUserRepository
{
    public User GetUser(int id);
    public User? GetUser(string email);
    public User AddUser(User user);
    public User GetByEmailAndPassword(string email, string password);
}