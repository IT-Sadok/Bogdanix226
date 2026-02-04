using Finly.Entities;

namespace Finly;

public interface IJWTService
{
    string GenerateJwt(User user);
}