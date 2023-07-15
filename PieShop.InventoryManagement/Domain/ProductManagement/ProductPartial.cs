using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
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
