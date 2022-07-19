using AuthenticationService.Database;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        DatabaseContext _context;

        public AuthenticationController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public static User user = new User();

        private readonly IConfiguration _configuration;

       
      

        [HttpPost("/api/v1.0/flight/login")]
        public IEnumerable<UserModel> GetUsers(login login)
        {
            CreatePasswordHash(login.password, out byte[] passwordHash, out byte[] passwordSalt);

            user.UserName = login.UserName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            //if (user.UserName != login.UserName)
            //{
            //    return BadRequest("User Not Found");
            //}

            //if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            //{
            //    return BadRequest("Wrong Password");
            //}
            string Logintoken = CreateToken(user);

            var RefeshToken = GenerateRefreshToken();
            SetRefreshToken(RefeshToken);
        
            var data = _context.Users.Where(u =>(u.UserName == login.UserName && u.Password== login.password)).Select(u =>
            new UserModel
            {
                Name = u.Name,
                UserID = u.UserID,
                UserName = u.UserName,
                Role = u.Role,
                Token= Logintoken


            }
            ).ToList();

            data[0].Token = Logintoken;
            return data;
        }

       
      
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

      private RefreshToken GenerateRefreshToken()
        {
            var refreshtoekn = new RefreshToken
            {
                Token = Convert.ToString(RandomNumberGenerator.GetInt32(64)),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(2)

            };

            return refreshtoekn;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookiesOptions);
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshtoken = Request.Cookies["refreshToken"];
            if (!user.RefreshToken.Equals(refreshtoken))
            {
                return Unauthorized("Invalid Refresh Token");

            }

            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token Expired");
            }

            string token = CreateToken(user);
            var newRefreshToekn = GenerateRefreshToken();
            SetRefreshToken(newRefreshToekn);

            return Ok(token);
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
         new Claim(ClaimTypes.Name,user.UserName),
         new Claim(ClaimTypes.Role,"Admin")
            };


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
              _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;


        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
