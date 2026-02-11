using Finly.DTO;
using Finly.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Finly;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IJWTService _jwtService;

    public AuthController(
        UserManager<User> userManager,
        IJWTService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginModel model,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
            return Unauthorized("Invalid credentials");

        var isPasswordValid =
            await _userManager.CheckPasswordAsync(user, model.Password);

        if (!isPasswordValid)
            return Unauthorized("Invalid credentials");

        var token = _jwtService.GenerateJwt(user);

        return Ok(new
        {
            token
        });
    }
}