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
                    case 0: throw new Exception("Error de conexión." + ex.ErrorCode);
                    case 1045: throw new Exception("Usuario o contraseña incorrectos.");
                    default: throw;
                }
            }
        }

        public void Desconectar()
        {
            try
            {
                conexion.Close();
            }
            catch(MySqlException)
            {
                throw;  // Captura una posible excepción de MySQL y la relanza.
            }
        }

        public bool Conectado()
        {
            if (conexion != null)
            {
                return conexion.State == System.Data.ConnectionState.Open;
            } else
            {
                return false;
            }
        }

        public List<Dvd> SeleccionarPA(string codigo)
        {
            List<Dvd> resultado = new List<Dvd>();
            int resul;

            MySqlCommand cmd = new MySqlCommand("selectDVD", conexion); // Instrucciones MySQL
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Armado de los parámetros del procedimiento almacenado
            cmd.Parameters.AddWithValue("@codi", codigo); // Método para añadir un parámetro al comando, aconsejado por MariaDB.
            cmd.Parameters.AddWithValue("@titu", null);
            cmd.Parameters.AddWithValue("@arti", null);
            cmd.Parameters.AddWithValue("@elPais", null);
            cmd.Parameters.AddWithValue("@comp", null);
            cmd.Parameters.AddWithValue("@prec", null);
            cmd.Parameters.AddWithValue("@elAnio", null);

            cmd.Parameters.AddWithValue("@resul", MySqlDbType.Int16);
            cmd.Parameters["@resul"].Direction = System.Data.ParameterDirection.Output;

            MySqlDataReader lector = cmd.ExecuteReader();
            resul = (int)cmd.Parameters["@resul"].Value;    // Por definir qué hacer en caso de resul != 0.

            while (lector.Read())
            {
                Dvd undvd = new Dvd();
                undvd.Codigo = int.Parse(lector["codigo"].ToString());
                undvd.Titulo = lector["titulo"].ToString();
                undvd.Artista = lector["artista"].ToString();
                undvd.Pais = lector["pais"].ToString();
                undvd.Compania = lector["compania"].ToString();
                undvd.Precio = double.Parse(lector["precio"].ToString());
                undvd.Anio = lector["anio"].ToString();

                resultado.Add(undvd);
            }

            lector.Close();
            return resultado;
        }

    }
}
