using Finly.DTO;

namespace Finly.Models;


public interface IAuthenticationService
{
    Task<string> Login(LoginModel model);
    Task Register(RegisterModel model);
}

