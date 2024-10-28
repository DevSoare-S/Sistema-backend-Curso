using MySql.Data.MySqlClient;
using Primeiro_Projeto.Entities;

namespace Primeiro_Projeto.Repositories
{
    public class UsuarioRepository
    {
        private string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Inserir(Usuario usuario)
        {
            var cnn = new MySqlConnection(_connectionString);

            var cmd = new MySqlCommand();

            cmd.Connection = cnn;
            cmd.CommandText = "INSERT INTO usuario(nome, sobrenome, telefone, emai, genero, senha, usuarioGuid) VALUES (@nome, @sobrenome, @telefone, @emai, @genero, @senha , @usuarioGuid)";
            cmd.Parameters.AddWithValue("nome", usuario.Nome);
            cmd.Parameters.AddWithValue("sobrenome", usuario.Sobrenome);
            cmd.Parameters.AddWithValue("telefone", usuario.Telefone);
            cmd.Parameters.AddWithValue("emai", usuario.Email);
            cmd.Parameters.AddWithValue("genero", usuario.Genero);
            cmd.Parameters.AddWithValue("senha", usuario.Senha);
            cmd.Parameters.AddWithValue("usuarioGuid", usuario.UsuarioGuid);

            cnn.Open();

            var affectedRows = cmd.ExecuteNonQuery();

            cnn.Close();

            return affectedRows;

        }

        public Usuario ObterUsuario(string email)
        {
            Usuario usuario = null;

            var cnn  = new MySqlConnection(_connectionString);

            var  cmd = new MySqlCommand();
            cmd.Connection = cnn;

            cmd.CommandText = "SELECT * FROM usuario WHERE emai = @emai";

            cmd.Parameters.AddWithValue("emai", email);

            cnn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                usuario = new Usuario();
                usuario.Id = Convert.ToInt32(reader["Id"]);
                usuario.Nome = reader["nome"].ToString();
                usuario.Sobrenome = reader["sobrenome"].ToString();
                usuario.Telefone = reader["telefone"].ToString();
                usuario.Genero = reader["genero"].ToString();
                usuario.Email = reader["emai"].ToString();
                usuario.Senha = reader["senha"].ToString();
                usuario.UsuarioGuid = new Guid(reader["usuarioGuid"].ToString());


            }

            cnn.Close();

            return usuario;


        }

        public Usuario ObterPorGuid(Guid usuarioGuid)
        {
            Usuario usuario = null;

            var cnn = new MySqlConnection(_connectionString);

            var cmd = new MySqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT * FROM usuario WHERE usuarioGuid = @usuarioGuid";

            cmd.Parameters.AddWithValue("usuarioGuid", usuarioGuid);

            cnn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                usuario = new Usuario();
                usuario.Id = Convert.ToInt32(reader["Id"]);
                usuario.Nome = reader["nome"].ToString();
                usuario.Sobrenome = reader["sobrenome"].ToString();
                usuario.Telefone = reader["telefone"].ToString();
                usuario.Genero = reader["genero"].ToString();
                usuario.Email = reader["emai"].ToString();
                usuario.Senha = reader["senha"].ToString();
                usuario.UsuarioGuid = new Guid(reader["usuarioGuid"].ToString());

            }

            cnn.Close();

            return usuario;
        }
    }
}
