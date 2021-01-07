using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIJWTLayerProj.Controllers
{
    public class AcountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
