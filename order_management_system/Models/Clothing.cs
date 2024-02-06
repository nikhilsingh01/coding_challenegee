using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order_management_system.Models
{
    internal class Clothing : Product
    {
        public string size {  get; set; }

        public string color { get; set; }

        public Clothing() { }

        public Clothing(int productId, string productName, string description, double price, int quantityInStock, string type, string size, string color)
        : base(productId, productName, description, price, quantityInStock, type)
        {
            size = size;
            color = color;
        }
    }
}
