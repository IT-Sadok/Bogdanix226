using Finly.DTO;
using Finly.Entities;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

namespace Finly;

public class TransactionService : ITransactionService
{
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddTransactionAsync(CreateTransactionModel model, int userId)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.UserId == userId);

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

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTransactionAsync(int transactionId, int userId)
    {
        var transaction = await _context.Transactions
            .Include(t => t.Account)
            .FirstOrDefaultAsync(t =>
                t.Id == transactionId &&
                t.Account.UserId == userId);

        if (transaction == null)
            throw new Exception("Transaction not found");

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TransactionHistoryModel>> GetHistoryAsync(int userId)
    {
        return await _context.Transactions
            .Where(t => t.Account.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TransactionHistoryModel
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = t.Type.ToString(),
                Description = t.Description,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync();
    }
}