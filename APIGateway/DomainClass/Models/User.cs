using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.Models
{
   public class User:BaseClass
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        
        public string Password { get; set; }
       [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public List<UserRole> userRoles { get; set; }
        public List<Log> Logs { get; set; }
    }
}
