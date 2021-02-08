using APIGateway.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIGateway.Controllers
{

    //[MyBasicAuthenticationFilter]
    [Authorize(Roles = "SendSMS")]

    public class SendSMSController : ApiController
    {

        [HttpPost]
       
        public IHttpActionResult Post(Models.SendSMS.Param param)
        {
            var isauthenticate = User.Identity.IsAuthenticated;
            var res = User.IsInRole("Admin");
            var res2 = User.IsInRole("Guess");
            var res3 = User.IsInRole("Guess2");

            var result = Models.SendSMS.SendSMS.SendSMSMethod(param.mobileNumber, param.msg);
            return Ok(result);

        }
    }
}
