using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Model
{
    public class Role
    {
        public int roleId { get; set; }
        public string title { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
    }
}
