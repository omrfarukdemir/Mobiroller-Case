using MediatR;
using Microsoft.AspNetCore.Identity;
using Mobiroller.DTOs;
using Mobiroller.Queries.Models;
using Mobiroller.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Mobiroller.Queries.Handlers
{
    public class GenerateTokenQueryHandler : IRequestHandler<GenerateTokenQuery, JwtDto>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public GenerateTokenQueryHandler(
            UserManager<IdentityUser> userManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        }

        public async Task<JwtDto> Handle(GenerateTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new Exception("Invalid Username or Password");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = GenerateClaims(user, roles.ToList());

            var token = new JwtDto()
            {
                Token = _jwtTokenService.CreateToken(claims)
            };

            return token;
        }

        private IEnumerable<Claim> GenerateClaims(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>()
            {
                new (ClaimTypes.NameIdentifier,user.Id),
                new (ClaimTypes.Email,user.Email),
                new (ClaimTypes.Name,user.UserName)
            };

            roles.ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));

            return claims;
        }
    }
}