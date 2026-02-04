using Finly.DTO;

namespace Finly;

public interface ITransactionService
{
    Task AddTransactionAsync(CreateTransactionModel model, int userId);
    Task DeleteTransactionAsync(int transactionId, int userId);
    Task<List<TransactionHistoryModel>> GetHistoryAsync(int userId);
}