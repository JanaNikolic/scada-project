using System.ComponentModel.DataAnnotations;

namespace SCADA.Model;

public class User
{
    [Key]
    public int Id { get; set; }
    public String Username { get; set; }
    public String Email { get; set; }
    public String Password { get; set; }
    public UserRole Role { get; set; }


    public User(String username, String email, String password, UserRole role)
    {
        Username = username;
        Email = email;
        Password = password;
        Role = role;
    }

    public enum UserRole
    {
        USER, ADMIN
    }
}