using MySql.Data.MySqlClient;
using System.Configuration;
using System.Web.Configuration;

namespace DAO.Dao
{
    public static class DaoConnection
    {
        private static string connectionString =
            ConfigurationManager.ConnectionStrings["strconmysql"].ConnectionString;

        public static MySqlConnection AbrirConexao()
        {
            var c = new MySqlConnection(connectionString);
            c.Open();
            return c;
        }

        public static void FecharConexao(MySqlConnection con)
        {
            con.Dispose();
        }
    }
}
