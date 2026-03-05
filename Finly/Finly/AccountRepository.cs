using Finly.Entities;
using Finly.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

namespace Finly;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.UserId == userId, cancellationToken);
    }
}