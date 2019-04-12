using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using _13AShopCart.DB;
using _13AShopCart.Models;
using System.Diagnostics;
using _13AShopCart.Util;

namespace _13AShopCart.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string Username, string Password, string sessionId)
        {
            if (Username == null) return View();

            User user = UserData.GetUserByUsername(Username);
            Password = Crypto.Hash(Password);
            if (user == null) return View();
            if (user.Password != Password)
            return View();

            Session["Userid"] = user.UserId;
            sessionId = SessionData.CreateSession(user.UserId, sessionId);
            Session["sessionId"] = sessionId;
            return RedirectToAction("Index", "Gallery", new { sessionId });
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult HelloPartial()
        {
            if (Session["Userid"] != null)
            {
                int userid = (int)Session["Userid"];
                User user = UserData.GetUserByUserId(userid);
                string hello = "Hello, " + user.FirstName + " " + user.LastName;
                ViewData["hello"] = hello;
            }
            return PartialView();
        }
    }
}