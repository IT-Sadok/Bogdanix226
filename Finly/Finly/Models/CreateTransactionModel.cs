using Finly.Models;

public class CreateTransactionModel
{
    public decimal Amount { get; set; }
    public int Type { get; set; } 
    public string? Description { get; set; }
    
    public int CategoryId { get; set; }
}