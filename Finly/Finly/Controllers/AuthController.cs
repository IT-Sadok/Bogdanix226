using Finly.DTO;
using Finly.Entities;
using Finly.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Finly;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var token = await _authService.Login(model);
        return Ok(new { token });
    }
}