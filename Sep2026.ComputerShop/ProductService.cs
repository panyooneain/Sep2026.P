
using Microsoft.Data.SqlClient;
using System.Data;

namespace Sep2026.PanYoonEain_ComputerShop
{
    public class ProductService
    {
        // ADD PRODUCT
        public int AddProduct(Product product)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string query = @"
                    INSERT INTO Products
                    (ProductName, Brand, Price, Quantity)
                    OUTPUT INSERTED.ProductId
                    VALUES
                    (@ProductName, @Brand, @Price, @Quantity)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 100).Value = product.ProductName;
                    cmd.Parameters.Add("@Brand", SqlDbType.NVarChar, 100).Value = product.Brand;

                    SqlParameter priceParameter = cmd.Parameters.Add("@Price", SqlDbType.Decimal);
                    priceParameter.Precision = 18;
                    priceParameter.Scale = 2;
                    priceParameter.Value = product.Price;

                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;

                    con.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // VIEW ALL PRODUCTS
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con = DbConnection.GetConnection())
            {
                string query = "SELECT * FROM Products ORDER BY ProductId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString()!,
                                Brand = reader["Brand"].ToString()!,
                                Price = Convert.ToDecimal(reader["Price"]),
                                Quantity = Convert.ToInt32(reader["Quantity"])
                            };

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }

        // SEARCH PRODUCT BY ID
        public Product? GetProductById(int id)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string query = "SELECT * FROM Products WHERE ProductId = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = id;

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Product
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString()!,
                                Brand = reader["Brand"].ToString()!,
                                Price = Convert.ToDecimal(reader["Price"]),
                                Quantity = Convert.ToInt32(reader["Quantity"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        // UPDATE PRODUCT
        public bool UpdateProduct(Product product)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string query = @"
                    UPDATE Products
                    SET ProductName = @ProductName,
                        Brand = @Brand,
                        Price = @Price,
                        Quantity = @Quantity
                    WHERE ProductId = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = product.ProductId;
                    cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 100).Value = product.ProductName;
                    cmd.Parameters.Add("@Brand", SqlDbType.NVarChar, 100).Value = product.Brand;

                    SqlParameter priceParameter = cmd.Parameters.Add("@Price", SqlDbType.Decimal);
                    priceParameter.Precision = 18;
                    priceParameter.Scale = 2;
                    priceParameter.Value = product.Price;

                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;

                    con.Open();

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // DELETE PRODUCT
        public bool DeleteProduct(int id)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string query = "DELETE FROM Products WHERE ProductId = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = id;

                    con.Open();

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // SELL PRODUCT
        public bool SellProduct(int id, int quantity)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string query = @"
                    UPDATE Products
                    SET Quantity = Quantity - @Quantity
                    WHERE ProductId = @ProductId
                    AND Quantity >= @Quantity";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;

                    con.Open();

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
