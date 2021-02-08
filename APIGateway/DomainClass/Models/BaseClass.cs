using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.Models
{
  public  class BaseClass
    {
        public BaseClass()
        {
            CreateDate = DateTime.Now;
        }
        public DateTime CreateDate { get; set; }
        public string IpAddress { get; set; }
    }
}
