using challenge.DAL;
using challenge.DAL.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace challenge.Managers
{
    public class AuthenticateManager : IAuthenticateManager
    {
        protected readonly IRepository<User> _repository;
        protected readonly string _tokenKey;

        public AuthenticateManager(IConfiguration configuration, IRepository<User> repository)
        {
            _repository = repository;
            _tokenKey = configuration.GetValue<string>("TokenKey");
        }

        public string Authenticate(string username, string password)
        {
            var user = _repository.Get(u => u.UserName == username && u.Pass == password);
            if (user == null)
            {
                return null;    
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}
