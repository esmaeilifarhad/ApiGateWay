using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Model
{
    public class UserRole
    {
        public int userRoleId { get; set; }
        public int userId { get; set; }
        public int roleId { get; set; }


        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
