using PlayTech.Models;
using PlayTech.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayTech
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nДля получения данных о заказах введите комманду \"go run main.go\" с номерами нужных заказов (прим. \"go run main.go 10,11,14,15\")\n");
                string command = Console.ReadLine();
                if (command.Contains("go run main.go"))
                {
                    int[] orderIds = new int[0];
                    command = command.Replace("go run main.go ", "");
                    try
                    {
                        orderIds = command.Split(',').Select(numStr => int.Parse(numStr)).ToArray();
                    }
                    catch
                    {
                        Console.WriteLine("Команда введена неверно, прим. go run main.go 10,11,14,15\n");
                    }
                    if (orderIds.Length > 0)
                        GetOrdersInfoConsole(orderIds);
                }
            }
        }

        public static void GetOrdersInfoConsole(int[] orderIds)
        {
            List<ShoppingCart> shoppingCarts = ShoppingCartService.GetShoppingCartsByOrders(orderIds);
            int stockShelfId = -1;

            foreach (var shoppingCart in shoppingCarts)
            {
                //Разделение стеллажей
                if (shoppingCart.StockShelfId != stockShelfId)
                {
                    Console.WriteLine("\n===Стеллаж " + shoppingCart.StockShelfName);
                }
                stockShelfId = shoppingCart.StockShelfId;

                Console.WriteLine(shoppingCart.Product.Name + " (id=" + shoppingCart.Product.Id + ")");
                Console.WriteLine("заказ " + shoppingCart.OrderId + ", " + shoppingCart.Count + " шт");

                //Отображение дополнительных стеллажей
                if (shoppingCart.Product.StockShelfs.Count > 1)
                {
                    string additionalShelfs = String.Empty;
                    for (int i = 0; i < shoppingCart.Product.StockShelfs.Count; i++)
                    {
                        if (shoppingCart.Product.StockShelfs[i].Name != shoppingCart.StockShelfName)
                        {
                            if (additionalShelfs != String.Empty)
                                additionalShelfs += ",";
                            else
                                additionalShelfs += "доп стеллаж: ";

                            additionalShelfs += shoppingCart.Product.StockShelfs[i].Name;
                        }
                    }
                    Console.WriteLine(additionalShelfs);
                }
                Console.WriteLine();
            }
        }
    }
}
