using Microsoft.EntityFrameworkCore;
using Finly.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyProject.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Category> Categories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Food" },
            new Category { Id = 2, Name = "Transport" },
            new Category { Id = 3, Name = "Iban" },
            new Category { Id = 4, Name = "Restaurant" },
            new Category { Id = 5, Name = "Travells" },
            new Category { Id = 6, Name = "Cinema" },
            new Category { Id = 7, Name = "Health" },
            new Category { Id = 8, Name = "Entertainment" },
            new Category { Id = 9, Name = "Electronics" },
            new Category { Id = 10, Name = "Utilities" }
        );
    }
}