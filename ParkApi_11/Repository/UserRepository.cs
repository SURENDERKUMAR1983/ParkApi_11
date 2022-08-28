using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkApi_11.data;
using ParkApi_11.Models;
using ParkApi_11.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkApi_11.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly Appsettings _appsettings;
        public UserRepository(ApplicationDbContext context,IOptions<Appsettings>appSettings)
        {
            _context = context;
            _appsettings = appSettings.Value;
        }
        public User Authenticate(string UserName, string Password)
        {
            var UserInDb = _context.Users.FirstOrDefault(u => u.UserName==UserName && u.Password == Password);
            if (UserInDb == null) return null;

            //JWT code
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, UserInDb.Id.ToString()),
                    new Claim(ClaimTypes.Role, UserInDb.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)                
            };
            var Token = tokenHandler.CreateToken(tokenDescriptor);
            UserInDb.Token = tokenHandler.WriteToken(Token);
            UserInDb.Password = "";
            return UserInDb;

        }

        public bool IsUniqueUser(string UserName)
        {
            var User = _context.Users.FirstOrDefault(u => u.UserName == UserName);
            if (User == null) 
                return true;
            else
                return false;           
        }

        public User Register(string UserName, string Password)
        {
            User user = new User()
            {
                UserName = UserName,
                Password = Password,
                Role = "Admin"
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
