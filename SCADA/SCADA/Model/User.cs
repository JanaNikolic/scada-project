using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SCADA.Model;

public class User
{
    // [Key]
    public int Id { get; set; }
    public String Email { get; set; }
    public String Password { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserRole Role { get; set; }


    public User(String email, String password, UserRole role)
    {
        Email = email;
        Password = password;
        Role = role;
    }

    public enum UserRole
    {
        USER, ADMIN
    }
}