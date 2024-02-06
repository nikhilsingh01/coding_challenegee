using Microsoft.Data.SqlClient;
using order_management_system.Models;
using order_management_system.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace order_management_system.Repository
{
    internal class OrderManagementRepository : IOrderManagementRepository
    {

        

        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;
        public OrderManagementRepository()
        {

            sqlconnection = new SqlConnection(DBUtil.GetConnectionString());
            cmd = new SqlCommand();
        }

        public bool cancelOrder(int userid, int orderid)
        {
            try
            {
                
                {
                    sqlconnection.Open();
                    SqlCommand command = sqlconnection.CreateCommand();
                    command.CommandText = "DELETE FROM Orders WHERE UserId = @UserId AND OrderId = @OrderId";
                    command.Parameters.AddWithValue("@UserId", userid);
                    command.Parameters.AddWithValue("@OrderId", orderid);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Order successfully canceled.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Failed to cancel the order.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public User checkUser(string username,string password)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT * FROM Users WHERE username = @username AND password=@password;";
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Create a Customer object with details
                    User user = new User();
                    user.UserId = (int)reader["userid"]; // Assuming user_id is the actual column name in the database
                    user.Username = reader["username"].ToString();
                    user.Password = reader["password"].ToString();
                    user.Role = reader["role"].ToString();
                    // Add other properties as needed

                    cmd.Parameters.Clear();
                    return user;
                }
                else
                {
                    Console.WriteLine($"Customer with email '{username}' not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions if needed
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        public bool createOrder(User user, List<Product> products)
        {
            try
            {
                
                {
                    sqlconnection.Open();

                   
                  
                    int userId;

                    
                   
                        userId = user.UserId;
                    

                    // Create the order
                    foreach (Product product in products)
                    {
                        SqlCommand command = sqlconnection.CreateCommand();
                        command.CommandText = "INSERT INTO Orders (UserId, ProductId) VALUES (@UserId, @ProductId)";
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@ProductId", product.ProductId);
                        command.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating order: {ex.Message}");
                return false;
            }
        }

        public bool createProduct(User user, Product product)
        {
            try
            {
                if (user != null && user.Role == "Admin")
                {
                    // Connect to the database
                    
                    {
                        sqlconnection.Open();

                        // Insert the product into the database
                        SqlCommand command = sqlconnection.CreateCommand();
                        command.CommandText = "INSERT INTO Product (ProductId, ProductName, Description, Price, QuantityInStock, Type) VALUES (@ProductId, @ProductName, @Description, @Price, @QuantityInStock, @Type)";
                        command.Parameters.AddWithValue("@ProductId", product.ProductId);
                        command.Parameters.AddWithValue("@ProductName", product.ProductName);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);
                        command.Parameters.AddWithValue("@Type", product.Type);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Product created successfully.");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Failed to create the product.");
                            return false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Login failed. You are not authorized to create products.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        public bool createUser(User user)
        {
            try
            {
                // Connect to the database
                
                {
                    sqlconnection.Open();

                    // Insert the user into the database
                    SqlCommand command = sqlconnection.CreateCommand();
                    command.CommandText = "INSERT INTO Users (UserId, Username, Password, Role) VALUES (@UserId, @Username, @Password, @Role)";
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("User created successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Failed to create the user.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public Product findProduct(int productid)
        {
            try
            {
                
                {
                    sqlconnection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlconnection;
                    cmd.CommandText = "SELECT ProductId, ProductName, Price, QuantityInStock FROM Product WHERE ProductId = @ProductId";
                    cmd.Parameters.AddWithValue("@ProductId", productid);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Create a new Product object with details from the database
                            Product product = new Product
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                Price = Convert.ToDouble(reader["Price"]),
                                QuantityInStock = Convert.ToInt32(reader["QuantityInStock"])
                                // Add other properties as needed
                            };

                            return product;
                        }
                    }

                    // Close the connection after use
                    sqlconnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Handle the exception or log it as needed
            }

            // Return null if the product is not found
            return null;


            // Return null if the product with the specified ID is not found
            sqlconnection.Close();
            return null;
            
        }

        public List<Product> getAllProducts()
        {
            List<Product> products = new List<Product>();

            try
            {
                // Connect to the database
                
                {
                    sqlconnection.Open();

                    // Retrieve all products from the database
                    SqlCommand command = sqlconnection.CreateCommand();
                    command.CommandText = "SELECT * FROM Product";
                    SqlDataReader reader = command.ExecuteReader();

                    // Iterate through the results and add each product to the list
                    while (reader.Read())
                    {
                        Product product = new Product(
                            Convert.ToInt32(reader["ProductId"]),
                            Convert.ToString(reader["ProductName"]),
                            Convert.ToString(reader["Description"]),
                            Convert.ToDouble(reader["Price"]),
                            Convert.ToInt32(reader["QuantityInStock"]),
                            Convert.ToString(reader["Type"])
                        );
                        products.Add(product);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Return an empty list in case of an exception
                return products;
            }
            finally
            {
                // Close the connection in the finally block to ensure it's always closed
                sqlconnection.Close();
            }

            // Return the list of products
            return products;

        }

        public List<Product> getOrderbyUser(User user)
        {
            List<Product> orderedProducts = new List<Product>();

            try
            {
                
                {
                    sqlconnection.Open();

                    // Retrieve ordered products by user from the database
                    SqlCommand command = sqlconnection.CreateCommand();
                    command.CommandText = "SELECT p.ProductId, p.ProductName, p.Description, p.Price, p.QuantityInStock, p.Type " +
                                          "FROM Orders o " +
                                          "JOIN Product p ON o.ProductId = p.ProductId " +
                                          "WHERE o.UserId = @UserId";
                    command.Parameters.AddWithValue("@UserId", user.UserId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterate through the results and add each ordered product to the list
                        while (reader.Read())
                        {
                            Product product = new Product(
                                Convert.ToInt32(reader["ProductId"]),
                                Convert.ToString(reader["ProductName"]),
                                Convert.ToString(reader["Description"]),
                                Convert.ToDouble(reader["Price"]),
                                Convert.ToInt32(reader["QuantityInStock"]),
                                Convert.ToString(reader["Type"])
                            );
                            orderedProducts.Add(product);
                        }
                    }

                    // Close the connection
                    sqlconnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return orderedProducts;

        }
    }
}
