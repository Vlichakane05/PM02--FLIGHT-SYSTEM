using Microsoft.Data.SqlClient;

namespace PM02__FLIGHT_SYSTEM.Data
{
    public class DatabaseConnection
    {
        private readonly string connectionString =
            "Data Source=localhost;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=\"SQL Server Management Studio\";Command Timeout=0";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}