using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Application.DTOs;
using TaskMgmt.Application.Interfaces;

namespace TaskMgmt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        var response = await _userService.RegisterAsync(request);
        SetTokenCookie(response.Token);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var response = await _userService.LoginAsync(request);
        SetTokenCookie(response.Token);
        return Ok(response);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("X-Access-Token");
        return NoContent();
    }

    private void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddHours(2),
            SameSite = SameSiteMode.Strict,
            Secure = true
        };
        Response.Cookies.Append("X-Access-Token", token, cookieOptions);
    }
}
