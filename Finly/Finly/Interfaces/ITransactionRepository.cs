using Finly.Entities;

namespace Finly.Interfaces;

public interface ITransactionRepository
{
    Task<Account?> GetAccountByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<Transaction?> GetTransactionByIdAndUserIdAsync(int transactionId, int userId, CancellationToken cancellationToken);
    Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId, DateTime fromDate, CancellationToken cancellationToken);
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken);
    Task DeleteAsync(Transaction transaction, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}