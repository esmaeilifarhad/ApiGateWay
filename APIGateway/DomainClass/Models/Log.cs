using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.Models
{
    public class Log : BaseClass
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Url { get; set; }
        public string BeforeAfter { get; set; }
    }
}
