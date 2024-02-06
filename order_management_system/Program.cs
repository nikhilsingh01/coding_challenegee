using order_management_system.Models;
using order_management_system.Services;

namespace order_management_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IServices service = new ServicesImpl();
            User loggedInUser = null;

            Console.WriteLine("Welcome to Ecommerce Application");

            while (true)
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");

                int userInput = int.Parse(Console.ReadLine());

                switch (userInput)
                {
                    case 1:
                        loggedInUser = service.Login();
                        if (loggedInUser != null)
                        {
                            Console.WriteLine($"Login successful. Welcome, {loggedInUser.Username}!");

                            // Nested loop for the main menu
                            while (true)
                            {
                                Console.WriteLine("\nMain Menu");
                                Console.WriteLine("1. Create Order");
                                Console.WriteLine("2. Cancel Order");
                                Console.WriteLine("3. Create Product");
                                Console.WriteLine("4. View All Products");
                                Console.WriteLine("5. View Orders by User");
                                Console.WriteLine("6. Logout");

                                int userinput = int.Parse(Console.ReadLine());

                                switch (userinput)
                                {
                                    case 1:
                                        service.createOrder();
                                        break;
                                    case 2:
                                        service.cancelOrder();
                                        break;
                                    case 3:
                                        service.createProduct();
                                        break;
                                    case 4:
                                        service.getAllProducts();
                                        break;
                                    case 5:
                                        service.getOrderbyUser();
                                        break;
                                    case 6:
                                        Console.WriteLine("Logging out. Goodbye!");
                                        return;
                                    default:
                                        Console.WriteLine("Invalid choice. Please try again.");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Login failed. Invalid email or password.");
                        }
                        break;

                    case 2:
                        service.createUser();
                        break;

                    case 3:
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                        break;
                }
            }


        }
    }
}
