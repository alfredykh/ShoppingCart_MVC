using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _13AShopCart.Models;
using _13AShopCart.DB;
using _13AShopCart.Controllers;

namespace _13AShopCart.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery

        public ActionResult Index(string sessionId, int? cartId, string Name)
        {
            //Session["browserSession"] = HttpContext.Session.SessionID;
            if (sessionId == null)  //if(sessionId == null)
            {
                sessionId = Guid.NewGuid().ToString();
                //Session["sessionId"] = HttpContext.Session.SessionID;
                //sessionId = Session["sessionId"].ToString();
            }

            List<Product> items = ProductData.GetProducts(Name);
            double itemCount = items.Count;
            double rows = Math.Ceiling(itemCount / 3);

            ViewData["itemCount"] = itemCount;
            ViewData["rows"] = rows;

            ViewData["sessionId"] = sessionId;
            ViewData["cartId"] = cartId;
            ViewData["products"] = items;

            return View();
        }


        //public ActionResult Search(string sessionId, int? cartId, string searchText)
        //{
        //    Session["browserSession"] = HttpContext.Session.SessionID;
        //    if (sessionId == null)  //if(sessionId == null)
        //    {
        //        sessionId = Guid.NewGuid().ToString();
        //        //Session["sessionId"] = HttpContext.Session.SessionID;
        //        //sessionId = Session["sessionId"].ToString();
        //    }

        //    List<Product> items = ProductData.SearchProducts(searchText);
        //    double itemCount = items.Count;
        //    double rows = Math.Ceiling(itemCount / 3);

        //    ViewData["itemCount"] = itemCount;
        //    ViewData["rows"] = rows;

        //    ViewData["sessionId"] = sessionId;
        //    ViewData["cartId"] = cartId;
        //    ViewData["products"] = items;

        //    return View("Index", new { sessionId = sessionId, cartId = cartId, searchText = searchText });
        //}


        public ActionResult ViewCart()
        {

            return RedirectToAction("Cart", "ViewCart");
        }

        public ActionResult Add(int ProductId, int? cartId, string sessionId)
        {
            cartId = CartData.AddtoCart(ProductId, cartId, sessionId);
            TempData["cartId"] = cartId;
           
            return RedirectToAction("Index", new {sessionId = sessionId, cartId = cartId});
        }
    }
}