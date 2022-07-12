﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Database
{
    public class User
    {

        public User()
        {
            UserRole = new HashSet<UserRole>();

        }
        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public string Role { get; set; }


        public virtual ICollection<UserRole> UserRole { get; set; }

}
}