using Finly.Models;

public interface ITransactionService
{
    Task<int> AddTransactionAsync(CreateTransactionModel model, CancellationToken cancellationToken);
    Task DeleteTransactionAsync(int transactionId, CancellationToken cancellationToken);
    Task<List<TransactionHistoryModel>> GetHistoryAsync(DateTime fromDate, CancellationToken cancellationToken);
}
