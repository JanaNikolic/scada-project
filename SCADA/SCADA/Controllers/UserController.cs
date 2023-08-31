using Microsoft.AspNetCore.Mvc;
using SCADA.DTOS;
using SCADA.Model;
using SCADA.Service.IService;

namespace SCADA.Controllers;

// [EnableCors]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        User? user = _userService.GetUser(id);
        if(user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [IgnoreAntiforgeryToken]
    [HttpPost]
    public IActionResult AddUser([FromBody]User user)
    {
        try
        {
            _userService.AddUser(user);
            return Ok();
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [IgnoreAntiforgeryToken]
    [HttpPost("login")]
    public IActionResult Login([FromBody]LoginDTO user)
    {
        try
        {
            var login = _userService.Login(user.Email, user.Password);
            if (login == null)
                return BadRequest("Invalid credentials!");
            return Ok(login);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}