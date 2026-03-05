using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finly.Entities;

[Table(nameof(Account))] 
public class Account
{
    [Key]
    public int Id { get; set; } 
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
    
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}