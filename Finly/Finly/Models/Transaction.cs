namespace Finly.Entities;

public enum TransactionType
{
    Income = 1,
    Expense = 2
}

public class Transaction
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public int AccountId { get; set; }
    public Account Account { get; set; }
}