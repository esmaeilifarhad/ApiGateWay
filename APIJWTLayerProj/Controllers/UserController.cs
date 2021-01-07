
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
        public User Login([FromBody] UserDto user)
        {
            var User = _userService.Authenticate(user.Username, user.Password);

            return User;
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
