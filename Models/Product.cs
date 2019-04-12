using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _13AShopCart.Models
{
    public class Product
    {
        public string ImageURL { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }

    }
}