using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _13AShopCart.DB;

namespace _13AShopCart.Controllers
{
    public class LogoutController : Controller
    {
        public ActionResult Index(string sessionId)
        {
            SessionData.RemoveSession(sessionId);
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}