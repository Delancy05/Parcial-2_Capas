using MySql.Data.MySqlClient;

namespace Datos
{
    /// <summary>
    /// Conexión a MySQL con un usuario de privilegios mínimos (solo SELECT e INSERT).
    /// </summary>
    public static class ConexionBD
    {
        private const string Cadena =
            "Server=localhost;Port=3306;Database=mvp_db;Uid=app_craps;Pwd=Clave123!;";

        public static MySqlConnection Crear()
        {
            return new MySqlConnection(Cadena);
        }
    }
}
