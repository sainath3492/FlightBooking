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
        [HttpPost("/api/v1.0/flight/login")]
        public IEnumerable<UserModel> GetUsers(login login)
        {
            var data = _context.Users.Where(u =>(u.UserID == login.userid && u.Password== login.password)).Select(u =>
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
