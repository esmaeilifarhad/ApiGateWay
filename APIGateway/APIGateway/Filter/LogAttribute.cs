using DomainClass.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace APIGateway.Filter
{
    public class WebAPIActionFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        //DataAccess.Context.MyContext _db = new DataAccess.Context.MyContext();
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //    var user = _db.Users.SingleOrDefault(q => q.Username == HttpContext.Current.User.Identity.Name);
            //    DomainClass.Models.Log log = new Log()
            //    {
            //        UserId = user.UserId,
            //        Url = actionExecutedContext.Request.RequestUri.ToString(),
            //        BeforeAfter = "After"

            //    };
            //    _db.Logs.Add(log);
            //    _db.SaveChanges();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
           //var user= _db.Users.SingleOrDefault(q => q.Username == HttpContext.Current.User.Identity.Name);
           // DomainClass.Models.Log log = new Log() {
           //     UserId = user.UserId,
           //    Url= actionContext.Request.RequestUri.ToString(),
           //    BeforeAfter="Before"

           // };
           // _db.Logs.Add(log);
           // _db.SaveChanges();
           // PersonController.Messages.Add("OnActionExecuting");
        }
    }

    public class WebAPIExceptionFilter : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //PersonController.Messages.Add("OnException");
            //actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Something went wrong") };
        }
    }
}