using PieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;
        private int maxItemsInStock = 0;
        private Price Price;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value[..250] : value;
                }
            }
        }

        public UnitType UnitType { get; set; }

        public int AmountInStock { get; private set; }

        public bool IsBelowStockThreshold { get; private set; }

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock) : this(id, name)
        {
            Description = description;
            UnitType = unitType;
            Price = price;
            maxItemsInStock = maxAmountInStock;

            UpdateLowStock();
        }

        public void UseProduct(int itemQuantity)
        {
            if (itemQuantity <= AmountInStock)
            {
                AmountInStock -= itemQuantity;

                UpdateLowStock();

                Log($"Updated stock count for {name}: {AmountInStock}");
            }
            else
            {
                Log($"Not enough {CreateSimpleProductRepresentation()} in stock. Stock: {AmountInStock}, Requested quantity: {itemQuantity}");
            }
        }

        public void IncreaseStock()
        {
            IncreaseStock(1);
        }

        public void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount;

            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                AmountInStock = maxItemsInStock;

                Log($"Stock overflow occurred for {CreateSimpleProductRepresentation()}. {newStock - AmountInStock} item(s) couldn't be stored");
            }

            UpdateLowStock();
        }

        public void DecreaseStock(int quantity, string reason)
        {
            if (quantity <= AmountInStock)
            {
                AmountInStock -= quantity;
            }
            else
            {
                AmountInStock = 0;
            }

            UpdateLowStock();

            Log(reason);
        }

        public string DisplayDetailsFull()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"ID: {Id}");
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine($"Price: {Price}");
            sb.AppendLine($"Amount In Stock: {AmountInStock}");

            if (IsBelowStockThreshold)
            {
                sb.AppendLine($"WARNING: Stock Low!");
            }

            return sb.ToString();
        }
    }
}
