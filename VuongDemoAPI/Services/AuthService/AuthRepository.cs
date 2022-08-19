using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VuongDemoAPI.Models;

namespace VuongDemoAPI.Services.AuthService
{
  public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public object SymmetricSecurity { get; private set; }

        public async Task<Response<string>> Login(string username, string password)
        {
            var response = new Response<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(username));

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else
            {
                var hmac = new System.Security.Cryptography.HMACSHA512(user.Salt);
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if (computeHash.SequenceEqual(user.Password) == false)
                {
                    response.Success = false;
                    response.Message = "Wrong password";
                }
                else
                {
                    response.Data = CreateToken(user);
                    response.Message = "Login success";
                }
            }

            return response;
        }

        public async Task<Response<int>> Register(User user, string password)
        {
            Response<int> response = new Response<int>();

            if (await UserExist(user.UserName))
            {
                response.Success = false;
                response.Message = "User already exist";
                return response;
            }

            var hmac = new System.Security.Cryptography.HMACSHA512();

            user.Password = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            user.Salt = hmac.Key;

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Register Completed";
            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExist(string username)
        {
            if (await _context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
