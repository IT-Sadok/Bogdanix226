using Finly.DTO;

public interface ITransactionService
{
    Task AddTransactionAsync(CreateTransactionModel model, int userId, CancellationToken cancellationToken);
    Task DeleteTransactionAsync(int transactionId, int userId, CancellationToken cancellationToken);
    Task<List<TransactionHistoryModel>> GetHistoryAsync(int userId, DateTime fromDate, CancellationToken cancellationToken);
    
}