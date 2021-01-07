using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIJWTLayerProj.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                Services.ApiServices.ApiSerivce serivce = new Services.ApiServices.ApiSerivce();
                var res = serivce.call(null, "http://localhost:56272/api/User/login", new { username = "admin", password = "1234" }, RestSharp.Method.POST);

                var objResult = JsonSerializer.Deserialize<DomainClass.Model.User>(res);

                //------------------
                Services.ApiServices.ApiSerivce serivce2 = new Services.ApiServices.ApiSerivce();
                var res2 = serivce2.call(objResult.token, "http://localhost:56272/api/Student/Get", null, RestSharp.Method.GET);

                var objResult2 = JsonSerializer.Deserialize<List<DomainClass.Model.Student>>(res2);
                //------------------

                //Services.ApiServices.ApiSerivce serivce3 = new Services.ApiServices.ApiSerivce();
                //var res3 = serivce3.call(null, "http://localhost:56272/api/Student/Update", null, RestSharp.Method.POST);

                return View();
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }
    }
}
