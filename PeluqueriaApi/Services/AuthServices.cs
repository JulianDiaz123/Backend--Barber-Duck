﻿using Microsoft.IdentityModel.Tokens;
using PeluqueriaApi.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PeluqueriaApi.Services
{
    public class AuthServices
    {
        private string secretKey;
        public AuthServices(IConfiguration config)
        {
            secretKey = config.GetSection("jwtSettings").GetSection("secretKey").ToString() ?? null!;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("id", user.id.ToString()));

            if (user.Roles != null)
            {
                foreach (var rol in user.Roles)
                {
                    claims.AddClaim(new Claim(ClaimTypes.Role, rol.Name));
                }
            }

            var symetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(
                    symetricKey,
                    SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(tokenConfig);
            return token;
        }
    }
}
