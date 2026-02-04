using Finly.DTO;

namespace Finly;

public interface IUserService
{
    Task<AuthTokenModel> RegisterUserAsync(CreateUserModel model);
}