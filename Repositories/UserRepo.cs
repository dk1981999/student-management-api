using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using student_management_api.Data;
using student_management_api.Models;
using student_management_api.Repositories.IRepositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace student_management_api.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;

        public UserRepo(ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }
        public UserModel Authenticate(string userName, string password)
        {
            var user = _db.Users.SingleOrDefault(x => x.userName == userName && x.password == password);

            //user not found
            if (user == null)
            {
                return null;
            }

            //if user was found, generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);
            user.password = "";

            return user;
        }

        public bool IsUniqueUser(string userName)
        {
            var user = _db.Users.SingleOrDefault(x => x.userName == userName);

            //return null if user not found
            if (user == null)
                return true;

            return false;
        }

        public UserModel Register(string userName, string password)
        {
            UserModel userObj = new UserModel()
            {
                userName = userName,
                password = password
            };

            _db.Users.Add(userObj);
            _db.SaveChanges();
            userObj.password = "";
            return userObj;
        }
    }
}
