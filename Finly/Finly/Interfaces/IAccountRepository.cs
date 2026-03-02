using Finly.Entities;

namespace Finly.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
}