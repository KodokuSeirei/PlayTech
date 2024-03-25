using PlayTech.Helpers;
using PlayTech.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlayTech.Services
{
    public static class OrderService
    {
        private static Order GetOrderFromReader(SqlDataReader reader)
        {
            var order = new Order
            {
                Id = SQLDataHelper.GetInt(reader, "OrderId"),
                CustomerEmail = SQLDataHelper.GetString(reader, "CustomerEmail"),
            };
            return order;
        }

        public static Order GetOrder(int id)
        {
            return SQLDataAccess.ExecuteReadOne(
                "SELECT * FROM  [Order] " +
                "WHERE [OrderId] = @Id",
            CommandType.Text, GetOrderFromReader,
                new SqlParameter("@Id", id));
        }

        public static List<Order> GetOrders(params int[] ids)
        {
            string query = "SELECT * FROM [Order]" + SQLDataHelper.MultiWhereQuery("[OrderId]", ids);

            return SQLDataAccess.ExecuteReadList(
                query,
            CommandType.Text, GetOrderFromReader);
        }
    }
}
