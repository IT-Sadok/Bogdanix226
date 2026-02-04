using Microsoft.AspNetCore.Identity;

namespace Finly.Entities;

public class User : IdentityUser<int>
{
    public Account Account { get; set; } 
}