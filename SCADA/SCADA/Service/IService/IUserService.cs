using SCADA.Model;

namespace SCADA.Service.IService;

public interface IUserService
{
    public User GetUser(int id);
    public User AddUser(User user);
    public User? Login(string email, string password);
}