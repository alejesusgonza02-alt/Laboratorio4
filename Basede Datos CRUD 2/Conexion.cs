using System.Data.SQLite;

namespace Ejemplo_en_clase_1
{
    internal class Conexion
    {
        private static string cadena = "Data Source=Productos.db;Version=3;";

        public static SQLiteConnection ObtenerConexion()
        {
            return new SQLiteConnection(cadena);
        }

        public static void CrearBaseDatos()
        {
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();

                string sql = @"
                    CREATE TABLE IF NOT EXISTS Productos
                    (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Producto TEXT NOT NULL,
                        Precio REAL NOT NULL,
                        Cantidad INTEGER NOT NULL,
                        Imagen TEXT
                    );";

                using (var comando = new SQLiteCommand(sql, conexion))
                {
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}