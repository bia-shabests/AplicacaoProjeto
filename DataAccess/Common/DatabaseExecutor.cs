using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccess.Common
{
    public static class DatabaseExecutor
    {
        public static void Execute(string connectionString, string procedure, DynamicParameters parametros = null, int timeout = 30,
             CommandType commandType = CommandType.StoredProcedure)
        {
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    dbConnection.Open();
                    dbConnection.Execute(procedure, parametros, commandType: commandType, commandTimeout: timeout);
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }
        public static async Task ExecuteAsync(string connectionString, string procedure, DynamicParameters parametros = null, int timeout = 30,
            CommandType commandType = CommandType.StoredProcedure)
        {
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    await dbConnection.OpenAsync();
                    await dbConnection.ExecuteAsync(procedure, parametros, commandType: commandType, commandTimeout: timeout);
                }
                finally
                {
                    await dbConnection.CloseAsync();
                }
            }
        }

        public static List<T> Query<T>(string connectionString, string procedure, DynamicParameters parametros = null, int timeout = 30,
            CommandType commandType = CommandType.StoredProcedure)
        {
            List<T> resultado;
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    dbConnection.Open();
                    resultado = dbConnection.Query<T>(procedure, parametros, commandType: commandType, commandTimeout: timeout).AsList();
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return resultado;
        }
        public static async Task<List<T>> QueryAsync<T>(string connectionString, string procedure, DynamicParameters parametros = null, int timeout = 30,
            CommandType commandType = CommandType.StoredProcedure)
        {
            IEnumerable<T> resultado;
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    await dbConnection.OpenAsync();
                    resultado = await dbConnection.QueryAsync<T>(procedure, parametros, commandType: commandType, commandTimeout: timeout);
                }
                finally
                {
                    await dbConnection.CloseAsync();
                }
            }
            return resultado.ToList();
        }

        public static T QueryFirstOrDefault<T>(string connectionString, string procedure, DynamicParameters parametros = null, int timeout = 30,
            CommandType commandType = CommandType.StoredProcedure)
        {
            T resultado;
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    dbConnection.Open();
                    resultado = dbConnection.QueryFirstOrDefault<T>(procedure, parametros, commandType: commandType, commandTimeout: timeout);
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return resultado;
        }
        public static async Task<T> QueryFirstOrDefaultAsync<T>(string connectionString, string procedure, DynamicParameters parametros = null, int timeout = 30,
            CommandType commandType = CommandType.StoredProcedure)
        {
            T resultado;
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    await dbConnection.OpenAsync();
                    resultado = await dbConnection.QueryFirstOrDefaultAsync<T>(procedure, parametros, commandType: commandType, commandTimeout: timeout);
                }
                finally
                {
                    await dbConnection.CloseAsync();
                }
            }
            return resultado;
        }

        public static T QueryFirst<T>(string connectionString, string procedure, DynamicParameters parametros = null, int timeout = 30,
            CommandType commandType = CommandType.StoredProcedure)
        {
            T resultado;
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    dbConnection.Open();
                    resultado = dbConnection.QueryFirst<T>(procedure, parametros, commandType: commandType, commandTimeout: timeout);
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return resultado;
        }
    }
}
