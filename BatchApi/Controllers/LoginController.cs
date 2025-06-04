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
using Microsoft.AspNetCore.Identity;
using BatchApi.ViewModels;

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
        // [HttpPost]
        //public IActionResult Login(User user)
        // {
        //     IActionResult response = Unauthorized();
        //     var obj = _context.Users.FirstOrDefault(x => x.UserName ==
        // user.UserName && x.Password == user.Password);
        //     if (obj!=null)
        //     {
        //         //var tokenString = GenerateJSONWebToken(user);
        //         var tokenString = GenerateJSONWebToken(obj);

        //         response = Ok(new { token = tokenString });
        //     }
        //     return response;


        // }

        [HttpPost]
        public IActionResult Login(LoginViewModel user)
        {
            IActionResult response = Unauthorized();
            var obj = _context.Users.FirstOrDefault(x => x.UserName ==
        user.UserName && x.Password == user.Password);
            if (obj != null)
            {
                //var tokenString = GenerateJSONWebToken(user);
                var tokenString = GenerateJSONWebToken(obj);

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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string RoleName = GetRoleName(user.RoleId);
            IEnumerable<Claim> claims = new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                    //new Claim("Id", user.Email),
                        new Claim(ClaimTypes.Name, user.FirstName.ToString() + " " + user.LastName),
                        //new Claim(ClaimTypes.Email, user.Email),
                        //new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                        new Claim(ClaimTypes.Role, RoleName),
                        new Claim(type:"Date", DateTime.Now.ToString()),
                        new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
                };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
             _configuration["Jwt:Audience"],
             claims,
             expires: DateTime.Now.AddMinutes(120),
             signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetRoleName(int roleId)
        {
            string roleName = (from x in _context.Roles
                               where x.RoleId == roleId
                               select x.RoleName).FirstOrDefault();
            return roleName;
                }
    }

}
