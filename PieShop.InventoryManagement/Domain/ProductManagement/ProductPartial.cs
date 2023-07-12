using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        private void UpdateLowStock()
        {
            if (AmountInStock < 10)
            {
                IsBelowStockThreshold = true;
            }
            else
            {
                IsBelowStockThreshold = false;
            }
        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product {id}: {name}";
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
