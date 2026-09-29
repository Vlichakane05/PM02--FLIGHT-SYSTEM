using Microsoft.Data.SqlClient;

namespace PM02__FLIGHT_SYSTEM.Data
{
    public class DatabaseConnection
    {
        private readonly string connectionString =
            "Server=localhost;Database=PM02- Airport system;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}