using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order_management_system.Models
{
    internal class Orders
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
      

        // Constructors
        public Orders()
        {
            
        }

        public Orders(int orderId, int userId, int productId)
        {
            OrderId = orderId;
            UserId = userId;
            ProductId = productId;
            
        }
    }
}
