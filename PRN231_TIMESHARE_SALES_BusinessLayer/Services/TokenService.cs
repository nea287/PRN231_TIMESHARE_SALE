using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateRefreshToken(string email, string roleName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:RefreshTokenSecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, roleName)
            };

            var refreshToken = new JwtSecurityToken(
                _configuration["Token:Issuer"],
                _configuration["Token:Audience"],
                claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["Token:RefreshTokenExpirationInDays"])),
                signingCredentials: creds
                );

            var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

            return refreshTokenString;
        }

        public (string accessToken, string refreshToken) GenerateAccessToken(string email, string roleName)
        {

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, roleName)
            };

                var accessToken = new JwtSecurityToken(
                    _configuration["Token:Issuer"],
                    _configuration["Token:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Token:ExpirationInMinutes"])),
                    signingCredentials: creds
                    );

                var refreshToken = GenerateRefreshToken(email, roleName);

                var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

                return (accessTokenString, refreshToken);
            }
        }
}
