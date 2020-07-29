using MySql.Data.MySqlClient;

namespace DAO.Dao
{
    internal class MysqlCommand
    {
        private string sQL_INSERT;
        private MySqlConnection cn;

        public MysqlCommand(string sQL_INSERT, MySqlConnection cn)
        {
            this.sQL_INSERT = sQL_INSERT;
            this.cn = cn;
        }
    }
}