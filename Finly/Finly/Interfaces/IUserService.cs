using Finly.Models;

public interface IUserService
{
    Task<AuthTokenModel> RegisterUserAsync(CreateUserModel model);
}