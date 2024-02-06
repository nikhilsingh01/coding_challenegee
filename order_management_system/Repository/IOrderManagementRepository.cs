using order_management_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order_management_system.Repository
{
    internal interface IOrderManagementRepository
    {
        public bool createOrder(User user, List<Product> products);

        public User checkUser(string username,string password);

        public Product findProduct(int productid);

        public bool cancelOrder(int userid, int orderid);

        public bool createProduct(User user,Product product);

        public bool createUser(User user);

        public List<Product> getAllProducts();

        public List<Product> getOrderbyUser(User user);
    }
}
