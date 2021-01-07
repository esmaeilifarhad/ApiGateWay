using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIJWTLayerProj.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShahkarController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("Get")]
        public string Get()
        {
            return "shahkar";
        }
    }
}
