using back_labprog.Business;
using back_labprog.Contracts.Frontend;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
namespace back_labprog.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserBO _userBO;

    public UserController(IUserBO userBO)
    {
        _userBO = userBO;
    }

    [EnableCors]
    [HttpPost("login")]
    public ActionResult Login([FromBody] User userCredentials)
    {
        var response = _userBO.VerifyLogin(userCredentials);
        if (response != null) return Ok(response);
        return Unauthorized();
    }

    [EnableCors]
    [HttpPost("register")]
    public ActionResult Register([FromBody] User userCredentials)
    {
        var alreadyExists = _userBO.VerifyMail(userCredentials);
        if (alreadyExists) return Conflict();
        
        var response = _userBO.Register(userCredentials);
        return Ok(response);
    }

    [EnableCors]
    [HttpGet("list")]
    public IEnumerable<User> GetUsers()
    {
        return _userBO.GetUsers();
    }

    [EnableCors]
    [HttpPost("delete")]
    public ActionResult DeleteUser([FromBody] User userCredentials)
    {
        var response = _userBO.DeleteUser(userCredentials);
        if (!response) return NotFound();
        return Ok(response);
    }
}