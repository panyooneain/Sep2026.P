
using Microsoft.Data.SqlClient;

namespace Sep2026.PanYoonEain_ComputerShop
{
    internal class DbConnection
    {
        private static readonly string connectionString =
            "Data Source=.;Initial Catalog=Sep2026.ComputerShopDb;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
