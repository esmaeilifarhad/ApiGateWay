using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.Models.SendSMS
{
    public class Param
    {
        public string msg { get; set; }
        public string mobileNumber { get; set; }
    }
}