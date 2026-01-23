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
    
    public async Task<AuthModel> RegisterUserAsync(CreateUserModel model)
    {
        await _userManager.CreateAsync(new User
        {

            Account = new Account()
            {
                DateCreated = DateTime.UtcNow,
                Description = model.AccountDescription
            },

            UserName = model.UserName,
            Email = model.Email
        }, model.Password);

        return new AuthModel
        {
            AccessToken = _jwtService.JwtGenerate(model.UserName)
        };
    }
    
}


public interface IUserService
{
    Task<AuthModel> RegisterUserAsync(CreateUserModel model);
    
    
    
}