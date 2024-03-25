using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PlayTech.Services

{
    public class SQLDataAccess : IDisposable
    {
        public SqlCommand cmd;
        public SqlConnection cn;
        private string connectionString = "Data Source=.\\MSSQLSERVER2022;Initial Catalog=PlayTech;Integrated Security=True";

        public SQLDataAccess()
        {
            cn = new SqlConnection(connectionString);
            cmd = new SqlCommand { Connection = cn };
        }

        private void Inititialize(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            cmd.Parameters.Clear();

            if (parameters == null || parameters.Any(param => param != null && param.Value == null))
                Console.WriteLine("Запрос к БД не содержит параметров");
            else
                cmd.Parameters.AddRange(parameters.Where(param => param != null).ToArray());
        }

        public void cnOpen()
        {
            try
            {
                cn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void cnClose()
        {
            if ((cn != null) && (cn.State != ConnectionState.Closed))
            {
                cn.Close();
            }
        }

        public static TResult ExecuteReadOne<TResult>(string commandText, CommandType commandType, Func<SqlDataReader, TResult> function, params SqlParameter[] parameters)
        {
            try
            {
                using (var db = new SQLDataAccess())
                {
                    db.Inititialize(commandText, commandType, parameters);

                    db.cnOpen();
                    using (var reader = db.cmd.ExecuteReader())
                    {
                        return reader.Read() ? function(reader) : default(TResult);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(TResult);
            }
        }

        public static List<TResult> ExecuteReadList<TResult>(string commandText, CommandType commandType, Func<SqlDataReader, TResult> function, params SqlParameter[] parameters)
        {
            var result = new List<TResult>();
            using (var db = new SQLDataAccess())
            {
                db.Inititialize(commandText, commandType, parameters);

                db.cnOpen();
                using (var reader = db.cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(function(reader));
                    }
                }
            }
            return result;
        }


        public void Dispose()
        {
            if (cn.State != ConnectionState.Closed)
                cn.Close();

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (cn != null)
            {
                cn.Dispose();
                cn = null;
            }
        }
    }
}
