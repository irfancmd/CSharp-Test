using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.Contracts
{
    public interface ISavable
    {
        string ConvertToStringForSaving();
    }
}
