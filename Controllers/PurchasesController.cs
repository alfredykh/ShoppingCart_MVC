using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using _13AShopCart.Models;
using _13AShopCart.DB;



namespace _13AShopCart.Controllers
{
    public class PurchasesController : Controller
    {
        // GET: Purchases
        public ActionResult GetPurchaseHistory(string sessionId)
        {
            sessionId = (string)Session["sessionid"];
            List<Purchase> purchases = PurchaseData.GetPurchaseHistoryBy(sessionId);

            //ViewData["cartId"] = cartId;
            
            ViewData["purchases"] = purchases;
          

            //ViewData["results"] = results;

            if ( sessionId != null)
            {
                return View();

            }


            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        
    }

    

}
