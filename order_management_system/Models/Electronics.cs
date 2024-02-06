﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order_management_system.Models
{
    internal class Electronics : Product
    {
        public string Brand { get; set; }

        public int WarrantyPeriod { get; set; }

        public Electronics() { }

        public Electronics(int productId, string productName, string description, double price, int quantityInStock, string type, string brand, int warrantyPeriod)
        : base(productId, productName, description, price, quantityInStock, type)
        {
            Brand = brand;
            WarrantyPeriod = warrantyPeriod;
        }
    }
}
