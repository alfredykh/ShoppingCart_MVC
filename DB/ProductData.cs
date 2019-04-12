using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _13AShopCart.Models;
using System.Data.SqlClient;

namespace _13AShopCart.DB
{
    public class ProductData : Data
    {
        public static List<Product> GetProducts(string searchText)
        {
            List<Product> products = new List<Product>();
            Product product = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql;
                conn.Open();

                if (searchText == null)
                {
                    sql = @"SELECT Product.ProductImage as ImageURL, Product.Id as ProductId, Product.Name as ProductName, Product.Description as ProductDescription, 
                                Product.Price as ProductPrice
                                FROM Product";
                }

                else
                {
                    sql = @"SELECT Product.ProductImage as ImageURL, Product.Id as ProductId, Product.Name as ProductName, Product.Description as ProductDescription, 
                                Product.Price as ProductPrice
                                FROM Product
                                WHERE Product.Name LIKE '%" + searchText + @"%'
                                OR Product.Description LIKE '%" + searchText + @"%'";
                }

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    product = new Product()
                    {
                        ImageURL = (string)reader["ImageURL"],
                        Id = (int)reader["ProductId"],
                        Name = (string)reader["ProductName"],
                        Description = (string)reader["ProductDescription"],
                        Price = (double)reader["ProductPrice"]

                    };

                    products.Add(product);
                }

                conn.Close();
            }

            return products;

        }


        public static Product GetProductByProductId (int ProductId, int id)
        {
            Product product = null;

            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();

            //    string sql = @"SELECT Product.Id as ProductId, Product.Name as ProductName, Product.Description as ProductDescription, 
            //                    Product.Price as ProductPrice
            //                    Where Product.Id = " + ProductId + 
            //                    @"FROM Product";

            //    SqlCommand cmd = new SqlCommand(sql, conn);

            //    SqlDataReader reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        product = new Product()
            //        {
            //            ImageURL = (string)reader["ImageURL"],
            //            Id = (int)reader["ProductId"],
            //            Name = (string)reader["ProductName"],
            //            Description = (string)reader["ProductDescription"],
            //            Price = (double)reader["ProductPrice"]

            //        };

            //        products.Add(product);
            //    }
            //}




            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();

            //    string sql = @"SELECT Product.Id as ProductId, Product.Name, Product.Description, 
            //                    Product.Price, Product.Quantity
            //                    FROM Product";

            //    SqlCommand cmd = new SqlCommand(sql, conn);

            //    SqlDataReader reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        Product product = new Product()
            //        {
            //            Id = (int)reader["ProductId"],
            //            Name = (string)reader["ProductName"],
            //            Description = (string)reader["Description"],
            //            Price = (float)reader["Price"],
            //            Qty = (int)reader["Qty"]

            //        };

            //        products.Add(product);
            //    }
            //}

            //Product zero = new Product();
            //zero.Id = 001;
            //zero.Name = "Apples";
            //zero.Description = "Apples are red in colour. There different types of apples from different parts of the world";
            //zero.Price = (float) 0.65;
            //products.Add(zero);

            //Product one = new Product();
            //one.Id = 002;
            //one.Name = "Oranges";
            //one.Description = "Did the colour or the fruit come first? Who knows?! Who cares? It's sweet and sour taste will have you coming back for more";
            //one.Price = (float)0.35;
            //products.Add(one);

            //Product two = new Product();
            //two.Id = 003;
            //two.Name = "Pears";
            //two.Description = "If we have stopped you with red apples and twarned you with orange oranges. We'll definitely let you go with green pears.";
            //two.Price = (float)0.40;
            //products.Add(two);

            //Product three = new Product();
            //three.Id = 004;
            //three.Name = "Pears";
            //three.Description = "If we have stopped you with red apples and twarned you with orange oranges. We'll definitely let you go with green pears.";
            //three.Price = (float)0.40;
            //products.Add(three);

            //Product four = new Product();
            //four.Id = 005;
            //four.Name = "Peaches";
            //four.Description = "Peaches come from a can, they were put there by a man, in a factory estate downtown.";
            //four.Price = (float)0.80;
            //products.Add(four);

            //Product five = new Product();
            //five.Id = 006;
            //five.Name = "Cantaloupes";
            //five.Description = "It rhymes with antelope but it's atype of fruit";
            //five.Price = (float)3.80;
            //products.Add(five);

            //Product six = new Product();
            //six.Id = 007;
            //six.Name = "Pomegranate";
            //six.Description = "I don't even know where to start";
            //six.Price = (float)1.20;
            //products.Add(six);


            //foreach (Product item in products)
            //{
            //    if (product.Id == ProductId)
            //    {
            //        product = item;
            //    }
            //}

            return product;
        }

    }
}