
using APIJWTLayerProj.Services;
using DomainClass.Model;
using DomainClass.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace APIJWTLayerProj.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] UserDto user)
        {
            var res = _userService.Authenticate(user.Username, user.Password);
            if (res == null)
                return Unauthorized();
            return Ok(res.token);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IEnumerable<User> GetAllUser()
        {
            return _userService.GetAll();
        }
        [Authorize(Roles = "User")]
        [HttpGet("User")]
        public string RoleNameUser()
        {
            return "you have access to User Role";
        }
    }
}
