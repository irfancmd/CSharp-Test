using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.ProductManagement
{
    public abstract partial class Product
    {
        protected string CreateSimpleProductRepresentation()
        {
            return $"Product {id}: {name}";
        }

        protected void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
