using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace _13AShopCart.Util
{
    public static class Crypto
    {
        public static string Hash(string Password)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)));
        }
    }
}