using AuthenticationService.Database;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        DatabaseContext _context;

        public AuthenticationController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<UserModel> GetUsers(int userid, string password)
        {
            var data = _context.Users.Where(u =>(u.UserID == userid && u.Password== password)).Select(u =>
            new UserModel
            {
                Name = u.Name,
                UserID = u.UserID,
                UserName = u.UserName,
                Role = u.Role


            }
            ).ToList();
            return data;
        }
    }
}
