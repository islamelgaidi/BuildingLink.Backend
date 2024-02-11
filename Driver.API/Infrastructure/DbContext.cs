using Driver.API.Domain.Interfaces;
using System.Data;
using Microsoft.Data.Sqlite;
namespace Driver.API.Infrastructure
{
    public class DbContext : IDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DbContext> _logger;
        public DbContext(IConfiguration configuration, ILogger<DbContext> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public int Execute(string sql)
        {
            int affectedRows = -1;
            try
            {
                using(SqliteConnection connection=new SqliteConnection(_configuration.GetConnectionString("DriverConnetionString")))
                {
                    connection.Open();
                    SqliteCommand cmd=new SqliteCommand(sql, connection);
                    affectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception  ex)
            {
                _logger.LogError(ex, $"Execute {sql}");
            }
            return affectedRows;
        }

        public int Execute(string sql, Dictionary<string, object> Parameters)
        {
            int affectedRows = -1;
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_configuration.GetConnectionString("DriverConnetionString")))
                {
                    connection.Open();
                    SqliteCommand cmd = new SqliteCommand(sql, connection);
                    foreach (KeyValuePair<string, object> param in Parameters)
                    {
                        if (param.Value != null)
                            cmd.Parameters.AddWithValue($"{param.Key}", param.Value);
                        else
                            cmd.Parameters.AddWithValue($"{param.Key}", DBNull.Value);
                    }
                    affectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Execute {sql} wiht paramters");
            }
            return affectedRows;
        }

        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_configuration.GetConnectionString("DriverConnetionString")))
                {
                    connection.Open();
                    SqliteCommand cmd = new SqliteCommand(sql, connection);
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GetDataTable {sql}");
            }
            return dt;
        }

        public DataTable GetDataTable(string sql, Dictionary<string, object> Parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqliteConnection connection = new SqliteConnection(_configuration.GetConnectionString("DriverConnetionString")))
                {
                    connection.Open();
                    SqliteCommand cmd = new SqliteCommand(sql, connection);
                    foreach (KeyValuePair<string, object> param in Parameters)
                    {
                        if (param.Value != null)
                            cmd.Parameters.AddWithValue($"{param.Key}", param.Value);
                        else
                            cmd.Parameters.AddWithValue($"{param.Key}", DBNull.Value);
                    }
                    //
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GetDataTable {sql} wiht paramters");
            }
            return dt;
        }
    }
}
