using DomainClass.Models;
using System;
using System.Web.Mvc;


namespace APIGateway.Controllers.MVC
{
    //[Authorize(Roles = "Pichak")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            /*
         var res=   UtilityAndServices.Utility.Utility.RSAGenerator();
            //DataAccess.Context.MyContext context = new DataAccess.Context.MyContext();
            
            DataAccess.Repository.GenericRepository<User> db = new DataAccess.Repository.GenericRepository<DomainClass.Models.User>();

            DomainClass.Models.User user = new DomainClass.Models.User()
            {
                FirstName = "فرهاد",
                LastName = "اسماعیلی",
                Username = "Pichak",
                Password = UtilityAndServices.Utility.Utility.RSAEncryption("P!CH@K", APIGateway.Properties.Settings.Default.PublicKey)

            };
            db.Insert(user);

            db.Save();
            */
            return View();
        }
    }
}