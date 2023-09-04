namespace SCADA.DTOS;

public class LoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    
    public LoginDTO(String email, String password)
    {
        Email = email;
        Password = password;
    }
}