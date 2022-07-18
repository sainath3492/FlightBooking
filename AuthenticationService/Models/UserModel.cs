using System;
using System.Collections.Generic;
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


        public string Error_Msg { get; set; }



    }

    public class login
    {
        public string UserName { get; set; }


        public string password { get; set; }
    }
}
