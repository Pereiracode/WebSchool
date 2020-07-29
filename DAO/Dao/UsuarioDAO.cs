using Model.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DAO.Dao
{
    public class UsuarioDAO
    {
        private const string SQL_SELECT_TODOS = "SELECT (login, senha, email) FROM usuarios";
        private const string SQL_SELECT_ID = "SELECT (login, senha, email) FROM usuarios WHERE login=?";
        private const string SQL_UPDATE = "UPDATE usuarios SET senha=?, email=? WHERE login=?";
        private const string SQL_DELETE = "DELETE FROM usuarios WHERE login=?";
        private const string SQL_INSERT = 
            "INSERT INTO usuarios(login, senha, email) VALUES(?, ?, ?)";

        public void Incluir(Usuario usuario)
        {
            MySqlConnection cn = null;
            try
            {
                cn = DaoConnection.AbrirConexao();
                var comando = new MySqlCommand(SQL_INSERT, cn);
                comando.Parameters.AddWithValue("@login", usuario.Login);
                comando.Parameters.AddWithValue("@senha", usuario.Senha);
                comando.Parameters.AddWithValue("@email", usuario.Email);

                comando.ExecuteNonQuery();
            }
            finally
            {
                DaoConnection.FecharConexao(cn);
            }
        }

        public void Editar(Usuario usuario)
        {
            MySqlConnection cn = null;
            try
            {
                cn = DaoConnection.AbrirConexao();
                var comando = new MySqlCommand(SQL_UPDATE, cn);
                comando.Parameters.AddWithValue("@senha", MySqlDbType.VarChar).Value = usuario.Senha;
                comando.Parameters.AddWithValue("@email", MySqlDbType.VarChar).Value = usuario.Email;
                comando.Parameters.AddWithValue("@login", MySqlDbType.VarChar).Value = usuario.Login;
                comando.ExecuteNonQuery();
            }
            finally
            {
                DaoConnection.FecharConexao(cn);
            }
        }

        public void Excluir(string login)
        {
            var cn = DaoConnection.AbrirConexao();
            try
            {
                var comando = new MySqlCommand(SQL_DELETE, cn);
                comando.Parameters.AddWithValue("@login", MySqlDbType.VarChar).Value = login;
                comando.ExecuteNonQuery();
            }
            finally
            {
                DaoConnection.FecharConexao(cn);
            }
        }

        public IList<Usuario> Listar()
        {
            var usuarios = new List<Usuario>();
            MySqlConnection cn = null;
            try
            {
                cn = DaoConnection.AbrirConexao();
                var comando = new MySqlCommand(SQL_SELECT_TODOS, cn);
                var result = comando.ExecuteReader();

                while (result.Read())
                {
                    var login = result["login"] as string;
                    var senha = result["senha"] as string;
                    var email = result["email"] as string;

                    usuarios.Add(new Usuario() { Email = email, Login = login, Senha = senha});
                }
            }
            finally
            {
                DaoConnection.FecharConexao(cn);
            }
            return usuarios;
        }

        public Usuario BuscarPorLogin(string login)
        {
            MySqlConnection cn = null;
            try
            {
                cn = DaoConnection.AbrirConexao();
                var comando = new MySqlCommand(SQL_SELECT_ID, cn);
                comando.Parameters.AddWithValue("@login", MySqlDbType.VarChar).Value = login;

                var result = comando.ExecuteReader();

                if (result != null)
                {
                    result.Read();
                    var logindb = result["login"] as string;
                    var senha = result["senha"] as string;
                    var email = result["email"] as string;
                    return new Usuario() { Login = logindb, Senha = senha, Email = email};
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                DaoConnection.FecharConexao(cn);
            }
        }
    }
}
