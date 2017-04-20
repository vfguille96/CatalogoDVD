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

        public UI()
        {
            dao = new DaoDVDMySQL();
            PedirOpcion();
        }

        static void Menu()
        {
            // Única clase que puede hablar con la consola, para no crear dependecia.

            Console.WriteLine("CATALOGO DE DVDs - Menu de opciones");
            Console.WriteLine("===================================");
            Console.WriteLine("(0) Conectar con la Base de Datos");
            Console.WriteLine("(Q) Fin del programa");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Opción? ");
        }

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
                            if (dao.Conectar(host, bd, usr, pwd))                             
                            {
                                Console.WriteLine("Conexión exitosa a la Base de Datos: " + bd);
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido un error: " + e.Message);
                }

            } while (opcion.Key != ConsoleKey.Q);
        }
    }
}
