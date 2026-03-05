using Finly.Entities;
using Microsoft.EntityFrameworkCore;

namespace Finly.Data.Seed;

public static class CategorySeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Food" },
            new Category { Id = 2, Name = "Transport" },
            new Category { Id = 3, Name = "Subscriptions" },
            new Category { Id = 4, Name = "Entertainment" },
            new Category { Id = 5, Name = "Health" }
        );
    }
}