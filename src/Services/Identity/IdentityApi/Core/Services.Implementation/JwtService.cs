using Core.Services.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Implementation
{
    public class JwtService : IJwtService
    {
        private IConfiguration _iConfigeration { get; }

        public JwtService(IConfiguration iConfiguration)
        {
            _iConfigeration = iConfiguration;
        }

        public string GetJwt(string email)
        {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_iConfigeration["key"]!);
                var jwtDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("email", email) }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateJwtSecurityToken(jwtDescriptor);
                return tokenHandler.WriteToken(token);
        }
    }
}
