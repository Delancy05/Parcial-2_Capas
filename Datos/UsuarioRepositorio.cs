using Entidades;
using MySql.Data.MySqlClient;

namespace Datos
{
    /// <summary>Implementación MySQL del repositorio de usuarios.</summary>
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public Usuario ObtenerPorCorreo(string correo)
        {
            using (var cn = ConexionBD.Crear())
            {
                cn.Open();
                var cmd = new MySqlCommand(
                    "SELECT id, correo, password_hash FROM usuarios WHERE correo = @correo", cn);
                cmd.Parameters.AddWithValue("@correo", correo);

                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Usuario
                    {
                        Id = rd.GetInt32("id"),
                        Correo = rd.GetString("correo"),
                        PasswordHash = rd.GetString("password_hash")
                    };
                }
            }
        }

        public bool Registrar(Usuario usuario)
        {
            using (var cn = ConexionBD.Crear())
            {
                cn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO usuarios (correo, password_hash) VALUES (@correo, @hash)", cn);
                cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@hash", usuario.PasswordHash);
                return cmd.ExecuteNonQuery() == 1;
            }
        }
    }
}
