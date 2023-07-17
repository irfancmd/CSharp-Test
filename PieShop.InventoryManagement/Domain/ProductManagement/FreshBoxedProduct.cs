using PieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.ProductManagement
{
    // Making this class sealed will prevent users from inheriting this class
    public sealed class FreshBoxedProduct : BoxedProduct
    {
        public FreshBoxedProduct(int id, string name, string? description, Price price, int maxAmountInStock, int amountPerBox) 
            : base(id, name, description, price, maxAmountInStock, amountPerBox)
        {
        }
    }
}
