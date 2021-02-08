using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIGateway.Controllers.MVC
{
    public class AccountController : Controller
    {
        DataAccess.Context.MyContext _db = new DataAccess.Context.MyContext();
        // GET: Account
        public ActionResult Register()
        {

            var res = (from u in _db.Users
                       join ur in _db.UserRoles
                       on u.UserId equals ur.UserId
                       join r in _db.Roles
                       on ur.RoleId equals r.RoleId
                       select new
                       {
u.UserId,
u.Username,
u.FirstName,
u.LastName,
u.CreateDate,
r.RoleId,
r.Rolename
                       }
                     ).ToList();


            return View();
        }
        [HttpPost]
        public ActionResult Register(string Username, string Password,string FirstName,string LastName, string LoginPass) {
            if (Properties.Settings.Default.LoginPass != LoginPass)
            {
                TempData["Error"] = "رمز عبور نادرست است";
                return RedirectToAction("Register");
            }
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                TempData["Error"] = "نام کاربری یا پسورد نمیتواند خالی باشد";
                return RedirectToAction("Register");
            }
            var oldUser=_db.Users.SingleOrDefault(q => q.Username == Username);
            if (oldUser != null)
            {
                TempData["Error"] = "نام کاربری قبلا ثبت شده است";
                return RedirectToAction("Register");
            }

            DomainClass.Models.User user = new DomainClass.Models.User() { 
            Username= Username,
            Password= UtilityAndServices.Utility.Utility.RSAEncryption(Password, APIGateway.Properties.Settings.Default.PublicKey),
            CreateDate=DateTime.Now,
            FirstName= FirstName,
            LastName= LastName
            };
            _db.Users.Add(user);
            _db.SaveChanges();
            TempData["Error"] = "با موفقیت ثبت شد";
            return RedirectToAction("Register");
        }
    }

    public class RegisterVM 
    {
        public DomainClass.Models.User user { get; set; }
        public List<DomainClass.Models.Role> roles { get; set; }

    }
}