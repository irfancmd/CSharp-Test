using PieShop.InventoryManagement.Domain.Contracts;
using PieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.ProductManagement
{
    public class BoxedProduct : Product, ISavable
    {
        public int AmountPerBox { get; set; }

        public BoxedProduct(int id, string name, string? description, Price price, int maxAmountInStock, int amountPerBox)
            : base(id, name, description, price, UnitType.PerBox, maxAmountInStock)
        {
            AmountPerBox = amountPerBox;
        }

        public override void UseProduct(int itemQuantity)
        {
            int smallestMultiple = 0;

            while (true)
            {
                smallestMultiple++;

                if (smallestMultiple * AmountPerBox > itemQuantity)
                {
                    break;
                }
            }

            base.UseProduct(smallestMultiple * AmountPerBox);
        }

        public override void IncreaseStock()
        {
            IncreaseStock(1);
        }

        public override void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount * AmountPerBox;

            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amount * AmountPerBox;
            }
            else
            {
                AmountInStock = maxItemsInStock;
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} item(s) ordere that couldn't be stored.");
            }

            if (AmountInStock > stockThreshold)
            {
                IsBelowStockThreshold = false;
            }
        }

        public string ConvertToStringForSaving()
        {
            return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};{1};{AmountPerBox};";
        }

        public override object Clone()
        {
            return new BoxedProduct(0, this.Name, this.Description, new Price() { ItemPrice = this.Price.ItemPrice, Currency = this.Price.Currency }, this.maxItemsInStock, this.AmountPerBox);
        }
    }
}
