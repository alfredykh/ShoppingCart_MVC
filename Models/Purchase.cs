using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _13AShopCart.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public String Date { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ActivationCode { get; set; }
        public string URL { get; set; }
        public string Image { get; set; }

    }
}