using back_labprog.Business;
using back_labprog.Contracts.Frontend;
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

    [HttpPost("login")]
    public ActionResult Login([FromBody] User userCredentials)
    {
        var response = _userBO.VerifyLogin(userCredentials);
        if (response != null) return Ok(response);
        return Unauthorized();
    }

    [HttpPost("register")]
    public ActionResult Register([FromBody] User userCredentials)
    {
        var alreadyExists = _userBO.VerifyMail(userCredentials);
        if (alreadyExists) return Conflict();
        
        var response = _userBO.Register(userCredentials);
        return Ok(response);
    }
}