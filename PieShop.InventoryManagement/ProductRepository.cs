﻿using PieShop.InventoryManagement.Domain.Contracts;
using PieShop.InventoryManagement.Domain.General;
using PieShop.InventoryManagement.Domain.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement
{
    internal class ProductRepository
    {
        private string directory = @"..\..\..\data\";
        private string productFileName = "products.txt";

        private void CheckForExistingProductFile()
        {
            string path = $"{productFileName}";

            Console.WriteLine(Path.GetFullPath(path));

            bool existingFileFound = File.Exists(productFileName);

            if (!existingFileFound)
            {
                if (!Directory.Exists(directory))
                {
                    Console.WriteLine("Here");
                }

                // Doing this will automatically close the file when we're done
                using FileStream fs = File.Create(path);
            }
        }

        public List<Product> LoadProductsFromFIle()
        {
            string path = $"{directory}{productFileName}";

            List<Product> products = new();

            try
            {
                CheckForExistingProductFile();

                string[] productsAsString = File.ReadAllLines(path);

                for (int i = 0; i < productsAsString.Length; i++)
                {
                    string[] productSplits = productsAsString[i].Split(';');

                    // Note that if we use "out", we don't have to declare the variable
                    bool success = int.TryParse(productSplits[0], out int productId);

                    if (!success)
                    {
                        // We can simply use the out reference here
                        productId = 0;
                    }

                    string name = productSplits[1];
                    string description = productSplits[2];

                    success = int.TryParse(productSplits[3], out int maxItemsInStock);
                    if (!success)
                    {
                        maxItemsInStock = 100; //default value
                    }

                    success = int.TryParse(productSplits[4], out int itemPrice);
                    if (!success)
                    {
                        itemPrice = 0; //default value
                    }

                    success = Enum.TryParse(productSplits[5], out Currency currency);
                    if (!success)
                    {
                        currency = Currency.UsDollar; //default value
                    }


                    success = Enum.TryParse(productSplits[6], out UnitType unitType);
                    if (!success)
                    {
                        unitType = UnitType.PerItem; //default value
                    }

                    string productType = productSplits[7];

                    Product product = null;

                    switch (productType)
                    {
                        case "1":
                            success = int.TryParse(productSplits[8], out int amountPerBox);
                            if (!success)
                            {
                                amountPerBox = 1;//default value
                            }

                            product = new BoxedProduct(productId, name, description, new Price() { ItemPrice = itemPrice, Currency = currency }, maxItemsInStock, amountPerBox);
                            break;

                        case "2":
                            product = new FreshProduct(productId, name, description, new Price() { ItemPrice = itemPrice, Currency = currency }, unitType, maxItemsInStock);
                            break;
                        case "3":
                            product = new BulkProduct(productId, name, description, new Price() { ItemPrice = itemPrice, Currency = currency }, maxItemsInStock);
                            break;
                        case "4":
                            product = new RegularProduct(productId, name, description, new Price() { ItemPrice = itemPrice, Currency = currency }, unitType, maxItemsInStock);
                            break;
                    }

                    products.Add(product);
                }

            }
            catch (IndexOutOfRangeException iex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong parsing the file, please check the data!");
                Console.WriteLine(iex.Message);
            }
            catch (FileNotFoundException fnfex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file couldn't be found!");
                Console.WriteLine(fnfex.Message);
                Console.WriteLine(fnfex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while loading the file!");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }

            return products;
        }

        public void SaveToFile(List<ISavable> savables)
        {
            StringBuilder sb = new StringBuilder();
            string path = $"{directory}{productFileName}";

            foreach (var item in savables)
            {
                sb.Append(item.ConvertToStringForSaving());
                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(path, sb.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saved items successfully");
            Console.ResetColor();
        }
    }
}
