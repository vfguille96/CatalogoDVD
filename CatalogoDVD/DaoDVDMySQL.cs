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

        public Conectar(string srv, string db, string user, string pwd)
        {
            // server=localhost;
            // database=catalogo;
            // uid=usr_catalogo
            // pwd=12345678;
            string cadenaConexion = "server=" + srv + 
        }
    }
}
