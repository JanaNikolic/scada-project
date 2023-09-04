using SCADA.Model;
using SCADA.Repository.IRepository;
using SCADA.Service.IService;

namespace SCADA.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public User? GetUser(int id)
    {
        return _userRepository.GetUser(id);
    }

    public User AddUser(User user)
    {
        return _userRepository.AddUser(user);
    }

    public User? Login(string email, string password)
    {
        return _userRepository.GetByEmailAndPassword(email, password);
    }
    
}