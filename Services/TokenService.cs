using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Library.Interfaces.Services;
using Library.Models;
using Microsoft.IdentityModel.Tokens;

namespace Library.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT__SigningKe"]!));
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
new Claim(JwtRegisteredClaimNames.GivenName,user.UserName!),
new Claim(JwtRegisteredClaimNames.Email,user.Email!),
new Claim(JwtRegisteredClaimNames.Gender,"Male")
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = creds,
                Issuer = _config["JWT__Issuer"],
                Audience = _config["JWT__Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}