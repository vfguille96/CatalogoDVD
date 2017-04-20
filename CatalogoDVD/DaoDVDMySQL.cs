using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace CatalogoDVD
{
    public class DaoDVDMySQL
    {
        // Encapsula con la BD
        public MySqlConnection conexion;

        public bool Conectar(string srv, string db, string user, string pwd)
        {
            // server=localhost;
            // database=catalogo;
            // uid=usr_catalogo
            // pwd=12345678;
            string cadenaConexion = "server=" + srv + ";" + "database=" + db + ";" + "uid=" + user + ";" + "pwd=" + pwd + ";";


            try
            {
                conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch(ex.Number)
                {
                    case 0: throw new Exception("Error de conexión.");
                    case 1045: throw new Exception("Usuario o contraseña incorrectos.");
                    default: throw;
                }
            }
        }
    }
}
