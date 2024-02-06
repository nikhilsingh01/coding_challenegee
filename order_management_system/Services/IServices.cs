using order_management_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order_management_system.Services
{
    internal interface IServices
    {
        public User Login();

        public void createOrder();

        public void cancelOrder();

        public void createProduct();

        public void createUser();

        public void getAllProducts();

        public void getOrderbyUser();
    }
}
