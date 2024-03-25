using PlayTech.Helpers;
using PlayTech.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlayTech.Services
{
    public static class StockShelfService
    {
        private static StockShelf GetStockShelfFromReader(SqlDataReader reader)
        {
            var stockShelf = new StockShelf
            {
                Id = SQLDataHelper.GetInt(reader, "StockShelfId"),
                Name = SQLDataHelper.GetString(reader, "Name"),
            };
            return stockShelf;
        }

        public static List<StockShelf> GetStockShelfsByProduct(int productId)
        {
            return SQLDataAccess.ExecuteReadList(
                "SELECT * FROM [StockShelf] " +
                "JOIN [ProductStockShelfBinding] ON [StockShelf].[StockShelfId] = [ProductStockShelfBinding].[StockShelfId]" +
                "WHERE [ProductId] = @Id",
            CommandType.Text, GetStockShelfFromReader,
                new SqlParameter("@Id", productId));
        }
    }
}
