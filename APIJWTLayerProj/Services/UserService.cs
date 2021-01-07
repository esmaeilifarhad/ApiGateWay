using APIJWTLayerProj.Config;
using DataLayer.Context;
using DomainClass;
using DomainClass.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APIJWTLayerProj.Services
{
    public class UserService : IUserService
    {
        UnitOfWork<User> db = new UnitOfWork<User>();
        UnitOfWork<UserRole> dbUserRole = new UnitOfWork<UserRole>();



        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {

            var users = db.Repository.Get(q => q.username == username && q.password == password);
            var user = users.FirstOrDefault();
            // return null if user not found
            if (user == null)
            {
                //return StatusCode(statusCode);
                return null;
            }


            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new ClaimsIdentity();
            var Roles = dbUserRole.Repository.Get(q => q.userId == user.userId, null, "Role");
            foreach (var item in Roles)
            {
                claims.AddClaims(new[]
            {

                new Claim(ClaimTypes.Role, item.Role.title)
            });
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Repository.Get();
        }
    }
}
