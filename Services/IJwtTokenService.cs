using System.Collections.Generic;
using System.Security.Claims;

namespace Mobiroller.Services
{
    public interface IJwtTokenService
    {
        string CreateToken(IEnumerable<Claim> claims);
    }
}