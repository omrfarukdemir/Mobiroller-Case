using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mobiroller.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mobiroller.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IOptions<JwtOptions> _jwtOptions;

        public JwtTokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string CreateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));

            var jwtToken = new JwtSecurityToken
            (
                issuer: _jwtOptions.Value.Issuer,
                audience: "Anyone",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}