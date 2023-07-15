using PieShop.InventoryManagement.Domain.General;
using PieShop.InventoryManagement.Domain.ProductManagement;

namespace PieShop.InventoryManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintWelcome();

            Utilities.InitializeStock();

            Utilities.ShowMainMenu();

            Console.WriteLine("Application shutting down...");

            Console.ReadLine();
        }

        private static void PrintWelcome()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome To Pie Shop!");
            Console.ResetColor();

            Console.WriteLine("Press Enter to log in.");
            // Accepting Enter
            Console.ReadLine();

            Console.Clear();
        }
    }
}