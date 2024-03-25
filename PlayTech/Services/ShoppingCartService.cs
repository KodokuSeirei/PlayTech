using PlayTech.Helpers;
using PlayTech.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlayTech.Services
{
    internal class ShoppingCartService
    {
        private static ShoppingCart GetShoppingCartsFromReader(SqlDataReader reader)
        {
            var shoppingCart = new ShoppingCart
            {
                Id = SQLDataHelper.GetInt(reader, "ShoppingCartId"),
                OrderId = SQLDataHelper.GetInt(reader, "OrderId"),
                StockShelfId = SQLDataHelper.GetInt(reader, "StockShelfId"),
                StockShelfName = SQLDataHelper.GetString(reader, "StockShelfName"),
                Count = SQLDataHelper.GetInt(reader, "Count"),
                Product = new Product
                {
                    Id = SQLDataHelper.GetInt(reader, "ProductId"),
                    Name = SQLDataHelper.GetString(reader, "ProductName"),
                    CategoryId = SQLDataHelper.GetInt(reader, "CategoryId")
                }
            };
            return shoppingCart;
        }

        public static List<ShoppingCart> GetShoppingCartsByOrder(int orderId)
        {
            return SQLDataAccess.ExecuteReadList(
                "SELECT [ShoppingCartId], [OrderId], [ShoppingCart].[ProductId], [ShoppingCart].[StockShelfId], " +
                "[Count], [Product].[Name] AS [ProductName], [CategoryId], [StockShelf].[Name] AS StockShelfName " +
                "FROM [ShoppingCart] " +
                "INNER JOIN[Product] ON [ShoppingCart].[ProductId] = [Product].[ProductId]" +
                "INNER JOIN[StockShelf] ON [ShoppingCart].[StockShelfId] = [StockShelf].[StockShelfId]" +
                "WHERE [OrderId] = @OrderId",
            CommandType.Text, GetShoppingCartsFromReader,
                new SqlParameter("@OrderId", orderId));
        }

        public static List<ShoppingCart> GetShoppingCartsByOrders(params int[] orderIds)
        {
            return SQLDataAccess.ExecuteReadList(
                "SELECT [ShoppingCartId], [OrderId], [ShoppingCart].[ProductId], [ShoppingCart].[StockShelfId], " +
                "[Count], [Product].[Name] AS [ProductName], [CategoryId], [StockShelf].[Name] AS StockShelfName " +
                "FROM [ShoppingCart] " +
                "INNER JOIN[Product] ON [ShoppingCart].[ProductId] = [Product].[ProductId]" +
                "INNER JOIN[StockShelf] ON [ShoppingCart].[StockShelfId] = [StockShelf].[StockShelfId]" +
                SQLDataHelper.MultiWhereQuery("[OrderId]", orderIds) + " ORDER BY [StockShelfId], [OrderId], [ProductId]",
            CommandType.Text, GetShoppingCartsFromReader);
        }
    }
}
