using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Finly.Entities;

[Table(nameof(Account))] 
public class Account
{
    [Key]
    public int id { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    
    public string? Description { get; set; }

}