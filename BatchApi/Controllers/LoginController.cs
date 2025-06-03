using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BatchApi.Models;
using BatchApi.Context;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using BatchApi.Migrations;
using Newtonsoft.Json;
using System.Data;

namespace BatchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private AppDbContext _context;
        IConfiguration _configuration;
        public LoginController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
       public IActionResult Login(User user)
        {
            IActionResult response = Unauthorized();
            var obj = _context.Users.FirstOrDefault(x => x.UserName ==
        user.UserName && x.Password == user.Password);
            if (obj!=null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;


        }

        //private string GenerateJSONWebToken(User user)
        //{
        //     
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
        //      _configuration["Jwt:Audience"],
        //      null,
        //      expires: DateTime.Now.AddMinutes(120),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        private string GenerateJSONWebToken(User user)
        {

            //var roleName = GetRoleName(user.RoleId);
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //// THIS IS FOR CLAIMS

            //var claims = new List<Claim>
            //{
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  // Fixed issue
            //    new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            //    new Claim(JwtRegisteredClaimNames.Name, $"{user.UserName}"),
            //    //new Claim(ClaimTypes.Email, user.Email),
            //    new Claim(ClaimTypes.Role, roleName),
            //    new Claim("DateOnly", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"))
            //};

            //foreach (var role in _context.Roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            //}
            //var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],

            //  _configuration["Jwt:Audience"],

            //  expires: DateTime.Now.AddMinutes(120),
            //  signingCredentials: credentials);

            //return new JwtSecurityTokenHandler().WriteToken(token);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roleName = GetRoleName(user.RoleId);
            var claims = new[]
           {
                new Claim("userId", user.UserName),
                new Claim("role", GetRoleName(user.RoleId))
            };

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_my_secret_key"));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

           
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetRoleName(int roleId)
        {
            string roleName = (from x in _context.Roles
                               where x.RoleId == roleId
                               select x.RoleName).ToString();
            return roleName;
                }
    }

}
