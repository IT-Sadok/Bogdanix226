using Finly.Models;
using Finly.Entities;
using Finly.Models;
using Microsoft.AspNetCore.Identity;

namespace Finly;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly IJWTService _jwtService;

    public AuthenticationService(
        UserManager<User> userManager,
        IJWTService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<string> Login(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null || 
            !await _userManager.CheckPasswordAsync(user, model.Password))
            throw new UnauthorizedAccessException();

        return _jwtService.GenerateJwt(user);
    }

    public async Task Register(RegisterModel model)
    {
        var user = new User
        {
            Email = model.Email,
            UserName = model.Email
        };

        await _userManager.CreateAsync(user, model.Password);
    }
}
