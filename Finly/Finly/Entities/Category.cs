namespace Finly.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<Transaction> Transactions { get; set; } = new();
}