using Finly.Models;
using Finly.Entities;
using Finly.Interfaces;

namespace Finly;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUserContext _userContext;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository,
        IUserContext userContext)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _userContext = userContext;
    }

    public async Task AddTransactionAsync(
        CreateTransactionModel model,
        CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        
        var account = await _accountRepository
            .GetByUserIdAsync(userId, cancellationToken);

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

        await _transactionRepository.AddAsync(transaction, cancellationToken);
        await _transactionRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTransactionAsync(
        int transactionId,
        CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        
        var transaction = await _transactionRepository.GetTransactionByIdAndUserIdAsync(transactionId, userId, cancellationToken);

        if (transaction == null)
            throw new Exception("Transaction not found");

        await _transactionRepository.DeleteAsync(transaction, cancellationToken);
        await _transactionRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<TransactionHistoryModel>> GetHistoryAsync(
        DateTime fromDate,
        CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        
        var transactions = await _transactionRepository.GetTransactionsByUserIdAsync(userId, fromDate, cancellationToken);

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