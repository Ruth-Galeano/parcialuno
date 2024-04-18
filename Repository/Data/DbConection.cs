using Npgsql;
using System.Data;

namespace Repository.Data
{
    public class DbConection
    {
        private string connectionString;

        public DbConection(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public IDbConnection dbConnection()
        {

            try
            {
                IDbConnection conexion = new NpgsqlConnection(connectionString);
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                throw new NpgsqlException(ex.Message);
            }
        }

    }
}
