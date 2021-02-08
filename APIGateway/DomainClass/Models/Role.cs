using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.Models
{
   public class Role
    {
        public int RoleId { get; set; }
        public string Rolename { get; set; }
        public string Description { get; set; }
        public List<UserRole> userRoles { get; set; }
    }
}
