using System;
using System.Linq;
using System.Threading.Tasks;

namespace Finly.Options;

public class JwtOptions
{
    public string Issuer { get; set; }
    
    public string Audience { get; set; }
    
    public string Key { get; set; }
    
    public int AccessTokenLifetimeMinutes { get; set; }

}