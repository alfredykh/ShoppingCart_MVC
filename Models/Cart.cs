using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _13AShopCart.Models;

namespace _13AShopCart.Models
{
    public class Cart
    {
        List<CartItem> cartList = new List<CartItem>();

        public void Add(CartItem item)
        {
            cartList.Add(item);
        }

        
    }
}