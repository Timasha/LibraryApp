using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Logic.Auth;
using Server.Logic.Auth.Models;
using Server.Logic.Auth.Responses.Base;

namespace Server.Controllers;

public class AuthController : Controller
{
    private AuthLogicProvider _authLogic;

    public AuthController(AuthLogicProvider authLogic)
    {
        _authLogic = authLogic;
    }
    
    [Route("/user/authenticate")]
    [HttpPost]
    public IActionResult Authenticate([FromForm] string login, [FromForm] string password)
    {

        var response = _authLogic.Authenticate(login, password);
        switch (response.StatusCode)
        {
            case AuthLogicResponsesStatusCode.Ok:
            {
                break;
            }
            case AuthLogicResponsesStatusCode.WrongLoginOrPassword:
            {
                return Json(response);
            }
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, response.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, response.RoleName)
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var task = HttpContext.SignInAsync(claimsPrincipal);
        task.Wait();
        return Json(response);
    }

    [Route("/register/user")]
    [HttpPost]
    public IActionResult RegisterSimpleUser([FromForm] string login, [FromForm] string password)
    {
        var response = _authLogic.RegisterSimpleUser(new User(login, password));



        return Json(response);  
    }

    [Authorize(Policy = "admin")]
    [Route("/register/admin")]
    [HttpPost]
    public IActionResult RegisterAdminUser([FromForm] string login, [FromForm] string password)
    {
        var response = _authLogic.RegisterAdminUser(new User(login, password));

        
        return Json(response);
    }
}