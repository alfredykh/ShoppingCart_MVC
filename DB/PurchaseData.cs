using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _13AShopCart.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

using _13AShopCart.Util;
namespace _13AShopCart.DB
{
    public class PurchaseData : Data
    {
        public static List<Purchase> GetPurchaseHistoryBy(string sessionId)
        {
            
            List<Purchase> purchases = new List<Purchase>();

          

            using (SqlConnection conn = new SqlConnection (connectionString) )
            {
                //int userId = 0;
                //conn.Open();
                //string sql = @"SELECT P.PurchaseId as PurchaseId, P.UserId as UserId,
                //                        P.Date as Date, P.ProductId as ProductId,
                //                        P.Quantity as Qty, P.Code as Code, C.Name as Name, C.Description as Description,
                //                        C.DownloadLink as URL, C.productImage as image
                //                        FROM  Purchase P LEFT OUTER JOIN Product C ON P.ProductId = C.Id
                //                        WHERE  P.UserId =  " + userid + @"";
                //conn.Close();

                conn.Open();

                string sql2 = @"SELECT P.PurchaseId as PurchaseId, P.UserId as UserId,
                                        P.Date as Date, P.ProductId as ProductId,
                                        P.Quantity as Qty, P.Code as Code, C.Name as Name, C.Description as Description,
                                        C.DownloadLink as URL, C.productImage as image
                                        FROM  User1 U , Purchase P LEFT OUTER JOIN Product C ON P.ProductId = C.Id
                                        WHERE  U.UserId = P.UserId AND   U.SessionId = '" +  sessionId  + @"' ";
                                 

                SqlCommand cmd2 = new SqlCommand(sql2, conn);

                SqlDataReader reader = cmd2.ExecuteReader();

                Timestamp ts = new Timestamp();

                while (reader.Read())
                {
                    Purchase purchase = new Purchase()
                    {
                        
                        PurchaseId = (int)reader["PurchaseId"],
                        UserId = (int)reader["UserId"],
                        //Username = (string)reader["Username"],
                        Date =Timestamp.dateFromTimestamp((long)reader["Date"]),
                        ProductId = (int)reader["ProductId"],
                        Qty = (int)reader["Qty"],
                        Code = (string)reader["Code"],
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        ActivationCode = (string)reader["Code"],
                        URL = (string)reader["URL"],
                        Image = (string)reader["image"]

                    };
                    purchases.Add(purchase);
                };

                foreach (Purchase purchase in purchases)
                {
                    // To pull out Product data from Product DB
                }
            };
            return purchases;
        }
    }
}