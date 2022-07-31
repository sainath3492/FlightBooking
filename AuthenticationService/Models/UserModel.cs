using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Models
{
    public class UserModel
    {

        public int UserID { get; set; }
        public string UserName  { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }
        public string Role { get; set; }

        public string Email { get; set; }
        public string Error_Msg { get; set; }



    }
    public class NewLogin
    {
       
        public string UserName { get; set; }
        public string Password { get; set; }

       
    }
    public class login
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}
