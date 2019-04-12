using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using _13AShopCart.Models;
using _13AShopCart.DB;


    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
        int cartID = (int)TempData["cartId"];
            List<CartItem> items = CartData.GetCartDetailsByCartId(cartID);
        ViewData["cartId"] = cartID;
            ViewData["Items"] = items;
            
            return View();
        }
        public ActionResult UpdateQty(int cartID, int pid, int qty)
        {
            CartData.UpdatePurchase(cartID,pid,qty);
            return null;

        }
        public ActionResult Checkout(int cartID)
        {
            List<CartItem> items = CartData.GetCartDetailsByCartId(cartID);
            CartData.WriteItemsToPurchase(items,cartID);
            return View();
        }
        public ActionResult Gallery(int cartID)
        {
        string ss = (string)Session["sessionId"];
            return RedirectToAction("Index","Gallery",new { sessionId = ss, cartId = cartID });
        }
    }
