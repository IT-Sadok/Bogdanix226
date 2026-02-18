using Finly.DTO;
using Finly.Entities;
using Finly.Interfaces;

namespace Finly;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task AddTransactionAsync(
        CreateTransactionModel model,
        int userId,
        CancellationToken cancellationToken)
    {
        var account = await _repository.GetAccountByUserIdAsync(userId, cancellationToken);

        if (account == null)
            throw new Exception("Account not found");

        var transaction = new Transaction
        {
            Amount = model.Amount,
            Type = (TransactionType)model.Type,
            Description = model.Description,
            CreatedAt = DateTime.UtcNow,
            AccountId = account.Id
        };

        await _repository.AddAsync(transaction, cancellationToken);
    }

    public async Task DeleteTransactionAsync(
        int transactionId,
        int userId,
        CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetTransactionByIdAndUserIdAsync(transactionId, userId, cancellationToken);

        if (transaction == null)
            throw new Exception("Transaction not found");

        await _repository.DeleteAsync(transaction, cancellationToken);
    }

    public async Task<List<TransactionHistoryModel>> GetHistoryAsync(
        int userId,
        DateTime fromDate,
        CancellationToken cancellationToken)
    {
        var transactions = await _repository.GetTransactionsByUserIdAsync(userId, fromDate, cancellationToken);

        return transactions.Select(t => new TransactionHistoryModel
        {
            Id = t.Id,
            Amount = t.Amount,
            Type = t.Type.ToString(),
            Description = t.Description,
            CreatedAt = t.CreatedAt
        }).ToList();
    }
}