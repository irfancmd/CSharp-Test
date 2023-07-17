using PieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.ProductManagement
{
    // Extension methods can be used to extend both regular and sealed classes.
    // Note that extension methods should be inside a static class. The method themselves have to be static, however, in rumetime these
    // methods will act like instance methods
    public static class ProductExtensions
    {
        static double dollarToEuro = 0.92;
        static double euroToDollar = 1.11;

        static double poundToEuro = 1.14;
        static double euroToPound = 0.88;

        static double dollarToPound = 0.81;
        static double poundToDollar = 1.14;

        // The first parameter is mandatory for all extension methods
        public static double ConvertProductPrice(this Product product, Currency targetCurrency)
        {
            Currency sourceCurrency = product.Price.Currency;
            double originalPrice = product.Price.ItemPrice;
            double convertedPrice = 0.0;

            if (sourceCurrency == Currency.UsDollar && targetCurrency == Currency.Euro)
            {
                convertedPrice = originalPrice * dollarToEuro;
            }
            else if (sourceCurrency == Currency.Euro && targetCurrency == Currency.UsDollar)
            {
                convertedPrice = originalPrice * euroToDollar;
            }
            else if (sourceCurrency == Currency.Pound && targetCurrency == Currency.Euro)
            {
                convertedPrice = originalPrice * poundToEuro;
            }
            else if (sourceCurrency == Currency.Euro && targetCurrency == Currency.Pound)
            {
                convertedPrice = originalPrice * euroToPound;
            }
            else if (sourceCurrency == Currency.UsDollar && targetCurrency == Currency.Pound)
            {
                convertedPrice = originalPrice * dollarToPound;
            }
            else if (sourceCurrency == Currency.Pound && targetCurrency == Currency.UsDollar)
            {
                convertedPrice = originalPrice * poundToDollar;
            }
            else
            {
                convertedPrice = originalPrice;
            }

            return convertedPrice;
        }
    }
}
