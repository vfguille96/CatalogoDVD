using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogoDVD
{
    public class UI
    {
        static DaoDVDMySQL dao;
        static string host = "localhost";
        static string bd = "catalogo";
        static string usr = "usr_catalogo";
        static string pwd = "12345678";

        static List<Dvd> listado;

        public UI()
        {
            dao = new DaoDVDMySQL();
            PedirOpcion();
        }

        /// <summary>
        /// Opciones de menú Principal.
        /// </summary>
        static void Menu()
        {
            // Única clase que puede hablar con la consola, para no crear dependecia.

            Console.WriteLine(" CATALOGO DE DVDs - Menu de opciones");
            Console.WriteLine(" ===================================");
            Console.WriteLine();
            Console.WriteLine(" (0) Conectar con la Base de Datos");
            Console.WriteLine(" (1) Desconectar con la Base de Datos");
            Console.WriteLine(" (2) Listar DVD por código [PA]");
            Console.WriteLine(" (Q) Fin del programa");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(" Opción: ");
        }
        /// <summary>
        /// Recorrido de la llamada a un select de los registros solicitados.
        /// </summary>
        static void MostrarListado()
        {
            if (dao.Conectado())
            {
                foreach (Dvd unDvd in listado)
                {
                    unDvd.ToString();
                }
            } else
            {
                Console.WriteLine("No hay una conexión válida.");
            }
            
        }

        /// <summary>
        /// Solicitud de opción por consola.
        /// </summary>
        static void PedirOpcion()
        {
            ConsoleKeyInfo opcion;
            do
            {
                Console.WriteLine();
                Menu();
                opcion = Console.ReadKey();
                try
                {
                    switch ((char)opcion.Key)
                    {
                        case '0': // Conexión BD.
                            if (!dao.Conectado())
                            {
                                if (dao.Conectar(host, bd, usr, pwd))                               
                                    Console.WriteLine();
                                    Console.WriteLine();
                                    Console.WriteLine("***********************************************************");
                                    Console.WriteLine(" Conexión exitosa a la Base de Datos: " + bd);
                                    Console.WriteLine("***********************************************************");
                                    Console.WriteLine();
                            } else
                            {
                                Console.WriteLine("No hay una conexión establecida.");
                            }
                                break;                            
                           
                        case '1': // Desconexión BD.                           
                                Console.WriteLine();
                                if (dao.Conectado())
                                {
                                    dao.Desconectar();
                                    Console.WriteLine();
                                    Console.WriteLine("***********************************************************");
                                    Console.WriteLine(" Desconexión exitosa a la Base de Datos: " + bd);
                                    Console.WriteLine("***********************************************************");
                                    Console.WriteLine(); 
                                } else
                                {
                                    Console.WriteLine("No hay una conexión establecida.");
                                }
                                                         
                            break;

                        case '2':   // Selección de DVD a través de Procedimientos Almacenados.
                            Console.WriteLine();
                            listado = new List<Dvd>();
                            listado = dao.SeleccionarPA(null);
                            MostrarListado();
                            Console.WriteLine();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("**********************************************************************");
                    Console.WriteLine(" Ha ocurrido un error: " + e.Message);
                    Console.WriteLine("**********************************************************************");
                }

            } while (opcion.Key != ConsoleKey.Q);
        }
    }
}
