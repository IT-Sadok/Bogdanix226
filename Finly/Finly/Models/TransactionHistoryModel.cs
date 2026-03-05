using Finly.Models;

public class TransactionHistoryModel
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
    public string? Description { get; set; }
    public string Category { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}