using Finly.Entities;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

namespace Finly.Data;

public static class Seed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;

        var categories = new List<Category>
        {
            new Category { Name = "Food" },
            new Category { Name = "Transport" },
            new Category { Name = "Restaurant" },
            new Category { Name = "Cinema" },
            new Category { Name = "Health" },
            new Category { Name = "Entertainment" },
            new Category { Name = "Electronics" },
            new Category { Name = "Utilities" }
        };

        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();
    }
}