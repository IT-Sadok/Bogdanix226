using Finly.DTO;
using Finly.Entities;
using Microsoft.AspNetCore.Identity;

namespace Finly;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IJWTService _jwtService;

    public UserService(UserManager<User> userManager, IJWTService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<AuthTokenModel> RegisterUserAsync(CreateUserModel model)
    {
        var user = new User
        {
            UserName = model.UserName,
            Email = model.Email,
            Account = new Account
            {
                DateCreated = DateTime.UtcNow,
                Description = model.AccountDescription
            }
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ",
                result.Errors.Select(e => e.Description)));
        }

        return new AuthTokenModel
        {
            AccessToken = _jwtService.GenerateJwt(user)
        };
    }
}