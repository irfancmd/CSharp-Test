using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.OrderManagement
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime OrderFulfillmentDate { get; private set; }
        public ICollection<OrderItem> OrderItems { get; }
        public bool IsFulfilled { get; set; } = false;

        public Order()
        {
            Id = new Random().Next(9999999);

            int numberOfSeconds = new Random().Next(100);
            OrderFulfillmentDate = DateTime.Now.AddSeconds(numberOfSeconds);
            
            OrderItems = new List<OrderItem>();
        }
    }
}
