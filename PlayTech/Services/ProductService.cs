using PlayTech.Helpers;
using PlayTech.Models;
using System.Data;
using System.Data.SqlClient;

namespace PlayTech.Services
{
    internal class ProductService
    {
        private static Product GetProductFromReader(SqlDataReader reader)
        {
            var product = new Product
            {
                Id = SQLDataHelper.GetInt(reader, "ProductrrId"),
                Name = SQLDataHelper.GetString(reader, "Name"),
                CategoryId = SQLDataHelper.GetInt(reader, "CategoryId"),
            };
            return product;
        }

        public static Product GetProduct(int id)
        {
            return SQLDataAccess.ExecuteReadOne(
                "SELECT * FROM [Product] " +
                "WHERE [Id] = @Id",
            CommandType.Text, GetProductFromReader,
                new SqlParameter("@Id", id));
        }
    }
}
