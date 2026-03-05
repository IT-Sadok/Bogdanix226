using Finly.Entities;
using Finly.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

namespace Finly.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetAccountByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.UserId == userId, cancellationToken);
    }

    public async Task<Transaction?> GetTransactionByIdAndUserIdAsync(int transactionId, int userId, CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .Include(t => t.Account)
            .FirstOrDefaultAsync(t => t.Id == transactionId && t.Account.UserId == userId, cancellationToken);
    }

    public async Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId, DateTime fromDate, CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .Where(t => t.Account.UserId == userId && t.CreatedAt >= fromDate)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        await _context.Transactions.AddAsync(transaction, cancellationToken);
    }

    public async Task DeleteAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        _context.Transactions.Remove(transaction);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}