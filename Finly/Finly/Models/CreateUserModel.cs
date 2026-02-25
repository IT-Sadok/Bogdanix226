using System;
using System.Linq;
using System.Threading.Tasks;

namespace Finly.Models;

public class CreateUserModel
{
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    public string AccountDescription { get; set; }
}