using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using _13AShopCart.DB;
using _13AShopCart.Models;
using _13AShopCart.Util;

    public class CartData : Data
    {
        public static bool IsActiveSessionId(string sessionId)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				string sql = @"SELECT COUNT(*) FROM User1
					WHERE sessionId = '" + sessionId + "'";
				SqlCommand cmd = new SqlCommand(sql, conn);
				int count = (int)cmd.ExecuteScalar();
				return (count == 1);
			}
		}

		
		public static List<CartItem> GetCartDetailsByCartId(int cartId)
		{
			List<CartItem> items = new List<CartItem>();

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();

				string sql = @"SELECT p.Id as ProductId,p.Name as ProductName,p.Description as ProductInfo, p.ProductImage as ImgUrl,p.price as ProductPrice, c.Quantity as Qty
										from Product as p,Cart as c
										where c.ProductId=p.Id AND c.Id=" + cartId;
				SqlCommand cmd = new SqlCommand(sql, conn);

				SqlDataReader reader = cmd.ExecuteReader();
				CartItem item = new CartItem();
				while (reader.Read())
				{
					items.Add(new CartItem() {
						ProductId = (int)reader["ProductId"],
						ProductName = (string)reader["ProductName"],
						ProductDescription = (string)reader["ProductInfo"],
						Price = (Double)reader["ProductPrice"],
						Qty = (int)reader["Qty"],
						ImgUrl = (string)reader["ImgUrl"]
					});        
					
				};
				return items;
			}
			
		}

		public static void WriteItemsToPurchase(List<CartItem> items , int cartID)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();

				// Getting the last purchaseID from Purchase table
				string sql = @"select top 1 PurchaseId as LastPurchaseID from Purchase order by PurchaseId desc ";
				SqlCommand cmd = new SqlCommand(sql, conn);
				SqlDataReader reader = cmd.ExecuteReader();
				int LastPurchaseID=0;
				if (reader.Read())
				{

					{
						LastPurchaseID = (int)reader["LastPurchaseID"];
					};
				};
				int purchaseID = LastPurchaseID + 1;
				reader.Close();

				string sql2 = @"select UserId from Cart where Id='"+cartID+"'";
				cmd = new SqlCommand(sql2, conn);
				SqlDataReader reader2 = cmd.ExecuteReader();
				int UserID = 0;
				if (reader2.Read())
				{

					{
						UserID = (int)reader2["UserId"];
					};
				};

				reader2.Close();
				// Inserting the purchase detail
				// Run this procedure in database
				/*Create procedure [dbo].[AddNewPurchase]  
					(  
					   @PurchaseID int,  
					   @UserID int,  
					   @Date bigint,
					   @ProductID int,  
					   @Quantity int,  
					   @Code varchar (MAX)   
					)  
					as  
					begin  
					   Insert into Purchase values(@PurchaseID,@UserID,@Date,@ProductID,@Quantity,@Code)  
					End 
				 */
				long timestamp;
				foreach (var item in items)
				{
					timestamp = Timestamp.unixTimestamp();
					SqlCommand com = new SqlCommand("AddNewPurchase", conn);
					com.CommandType = CommandType.StoredProcedure;
					com.Parameters.AddWithValue("@PurchaseID", purchaseID);
					com.Parameters.AddWithValue("@UserID",UserID);
					com.Parameters.AddWithValue("@Date", timestamp);
					com.Parameters.AddWithValue("@ProductID", item.ProductId);
					com.Parameters.AddWithValue("@Quantity", item.Qty);
					string s = "";
					string[] str=new string[item.Qty];
					for(int x=0; x<item.Qty; x++)
					{
						string sessionId = Guid.NewGuid().ToString();
						sessionId = string.Concat(sessionId, ",");
						str[x] = sessionId;
						Debug.WriteLine(s);
						
					}
					s=string.Concat(str);
					Debug.WriteLine(s);
					com.Parameters.AddWithValue("@Code", s);
					int i = com.ExecuteNonQuery();
				}                
				conn.Close();
			}
		}

		public static void UpdatePurchase(int cartID, int pid, int qty)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();

				// Getting the last purchaseID from Purchase table
				string sql = @"update Cart set Quantity='" + qty + "'" + " where id='" + cartID + "'" + " and ProductId='" + pid + "'";
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.ExecuteNonQuery();

				conn.Close();
			}
		}

        public static int AddtoCart(int ProductId, int? cartId, string sessionId)
        {
            int userId = 0;
            if (cartId == null)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT MAX(Cart.Id) as CartId FROM Cart";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cartId = (int)reader["CartId"];
                    }
                    conn.Close();

                    cartId++;

                    conn.Open();
                    string sqlCheckUserId = @"SELECT User1.UserId FROM User1 WHERE User1.SessionId = '" + sessionId + @"'";

                    SqlCommand cmd2 = new SqlCommand(sqlCheckUserId, conn);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    
                    if(reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            userId = (int)reader2[0];
                        }
                    }
                    
                    conn.Close();

                    
                    conn.Open();
                    string sqlAddNew = @"INSERT INTO Cart (Id, UserId, ProductId, Quantity) VALUES (" + cartId + @", 0," + ProductId + @", 1)"; ;
                    if (userId != 0)
                    {
                        sqlAddNew = @"INSERT INTO Cart (Id, UserId, ProductId, Quantity) VALUES (" + cartId + @", " + userId + @", " + ProductId + @", 1)";
                    }
                    SqlCommand cmd3 = new SqlCommand(sqlAddNew, conn);
                    cmd3.ExecuteNonQuery();
                    conn.Close();
                
                }
            }

            else if (cartId != null)
            {
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    int productIdMatch = 0;
                    var userIdMatch = 0;

                    conn.Open();
                    string sql = @"SELECT Cart.ProductId as CartProductId, Cart.UserId as UserId FROM Cart WHERE Cart.Id =" + cartId;

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (ProductId == (int)reader["CartProductId"]) // if product ID is present
                        {
                            productIdMatch = ProductId;
                        }

                        if ((int)reader["UserId"] != 0 && reader["UserId"] != null)
                        {
                            userIdMatch = (int)reader["UserId"];
                        }

                    }
                    conn.Close();




                    conn.Open();
                    string sqlWrite = "";

                    if (productIdMatch != 0) // if product is present in cart and userId is irrelevant
                    {
                        string sqlIncrement = @"Update Cart Set Cart.Quantity = Cart.Quantity + 1 WHERE Cart.Id =" + cartId + @" AND Cart.ProductId =" + productIdMatch;
                        sqlWrite = sqlIncrement;
                    }

                    else if (productIdMatch == 0 && userIdMatch != 0) // if product is not present in cart and userId is present
                    {
                        string SqlAddNew = @"INSERT INTO Cart (Id, UserId, ProductId, Quantity) VALUES (" + cartId + @"," + userIdMatch + @"," + ProductId + @", 1)";
                        sqlWrite = SqlAddNew;
                    }

                    else if (productIdMatch == 0 && userIdMatch == 0) // if product is not present in cart and userId is not present
                    {
                        string sqlAddNew = @"INSERT INTO Cart (Id, ProductId, Quantity) VALUES (" + cartId + @"," + ProductId + @", 1)";
                        sqlWrite = sqlAddNew;
                    }

                    SqlCommand cmd2 = new SqlCommand(sqlWrite, conn);
                    cmd2.ExecuteNonQuery();

                    conn.Close();
                }
            }
            
            return (int) cartId;
        }

    }

