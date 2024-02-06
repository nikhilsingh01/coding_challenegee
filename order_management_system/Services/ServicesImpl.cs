using order_management_system.Models;
using order_management_system.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order_management_system.Services
{
    internal class ServicesImpl : IServices
    {
        IOrderManagementRepository repository = new OrderManagementRepository();

        User user;

        public void cancelOrder()
        {
            Console.Write("Enter User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Order ID: ");
            int orderId = Convert.ToInt32(Console.ReadLine());

            // Call the CancelOrder method and handle any exceptions
            try
            {
                repository.cancelOrder(userId, orderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void createOrder()
        {

            if (user == null)
            {
                Console.WriteLine("Register:");
                createUser();
                return;
            }

            List<Product> productList = new List<Product>();

            Console.WriteLine("Enter product details. Enter 'exit' for product name to finish.");

            while (true)
            {
                Console.WriteLine("Enter the Product ID to add to the list (Enter 0 to exit): ");
                int input = int.Parse(Console.ReadLine());

                if (input == 0)
                {
                    Console.WriteLine("Exiting product selection...");
                    break;
                }

                Product existingProduct = repository.findProduct(input);
                if (existingProduct == null)
                {
                    Console.WriteLine($"Product with ID {input} not found in the database. Please enter a valid product ID.");
                    continue;
                }

              
                productList.Add(existingProduct);

                Console.WriteLine($"Product with ID {input} added to the list.\n");
            }

           
            if (productList.Count > 0)
            {
                bool status = repository.createOrder(user, productList);
                if (status)
                {
                    Console.WriteLine("\nList of Booked Products:");
                    foreach (Product product in productList)
                    {
                        Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.ProductName}, Price: {product.Price}, Quantity in Stock: {product.QuantityInStock}");
                    }
                }
            }
        }
            public void createProduct()
        {
            try
            {
                
                

                // Check if the login was successful and if the user is an admin
                if (user != null && user.Role == "Admin")
                {
                    // Prompt the user to enter product details
                    Console.WriteLine("Enter product details:");
                    Console.Write("Product ID: ");
                    int productId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Product Name: ");
                    string productName = Console.ReadLine();
                    Console.Write("Description: ");
                    string description = Console.ReadLine();
                    Console.Write("Price: ");
                    double price = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Quantity in Stock: ");
                    int quantityInStock = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Type: ");
                    string type = Console.ReadLine();

                    // Create a new product object with the provided details
                    Product product = new Product
                    {
                        ProductId = productId,
                        ProductName = productName,
                        Description = description,
                        Price = price,
                        QuantityInStock = quantityInStock,
                        Type = type
                    };

                    // Call the createProductFrontend method
                   
                    repository.createProduct(user, product);
                }
                else
                {
                    Console.WriteLine("Login failed. You are not authorized to create products.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void createUser()
        {
            try
            {
                // Prompt the user to enter user details
                Console.Write("Enter User ID: ");
                int userId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Username: ");
                string username = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();
                Console.Write("Enter Role (Admin/User): ");
                string role = Console.ReadLine();

                // Create a new User object with the provided details
                User newUser = new User(userId, username, password, role);

                // Call the createUserFrontend method
                
                repository.createUser(newUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void getAllProducts()
        {
            try
            {
                // Call the getAllProductsFrontend method
                
                List<Product> products = repository.getAllProducts();

                // Display product details
                Console.WriteLine("Product Details:");
                foreach (Product product in products)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}");
                    Console.WriteLine($"Product Name: {product.ProductName}");
                    Console.WriteLine($"Description: {product.Description}");
                    Console.WriteLine($"Price: {product.Price}");
                    Console.WriteLine($"Quantity in Stock: {product.QuantityInStock}");
                    Console.WriteLine($"Type: {product.Type}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        
    }

        public void getOrderbyUser()
        {
            

            List<Product> products = repository.getOrderbyUser(user);

            try
            {
                // Call the getAllProductsFrontend method

               

                // Display product details
                Console.WriteLine("Product Details:");
                foreach (Product product in products)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}");
                    Console.WriteLine($"Product Name: {product.ProductName}");
                    Console.WriteLine($"Description: {product.Description}");
                    Console.WriteLine($"Price: {product.Price}");
                    Console.WriteLine($"Quantity in Stock: {product.QuantityInStock}");
                    Console.WriteLine($"Type: {product.Type}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        public User Login()
        {
            try
            {
                Console.Write("Enter Your Name: ");
                string username = Console.ReadLine();

                Console.Write("Enter Your Password: ");
                string password = Console.ReadLine();

                User loggedinuser = repository.checkUser(username,password);

                if (loggedinuser!= null)
                {
                    Console.WriteLine("User Present");
                    Console.WriteLine($"Customer Details: {loggedinuser.Username}");
                    // Now you can use loggedInCustomer for future use
                    user = loggedinuser;
                    return loggedinuser;
                }
                else
                {
                    Console.WriteLine("Login failed. Invalid email or password.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
