using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _13AShopCart.Models
{
    public class ActivationCode
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public int PurchaseId { get; set; }
    }
}