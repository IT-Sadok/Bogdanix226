using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Finly.Entities;

[Table(nameof(User))]
public class User: IdentityUser<int>
{
   public int AccountId { get; set; }
   
   [ForeignKey(nameof(AccountId))]
   public Account Account { get; set; }

}