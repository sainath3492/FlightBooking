using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Database
{
    public class Role
    {

        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        [Key]
        public int RoleID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
