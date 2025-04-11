using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace AncientAura.Service.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser appUser , UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.Name,appUser.FullName)
            };

            var userRole = await userManager.GetRolesAsync(appUser);
            foreach(var role in userRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)

                ) ;

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
