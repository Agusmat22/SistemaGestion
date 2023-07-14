using MySql.Data.MySqlClient;
using System.ComponentModel.Design;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using System.Text.RegularExpressions;

namespace Logica
{
    public static class Sistema
    {
        static List<Persona>  listaAlumnos;  // creo una lista de Personas
        static Sistema()
        {
            listaAlumnos = new List<Persona>(); // guardo los alumnos cuando se llama al metodo

        }

        public static string PedirNombre(string mensaje)
        {
            string nombreIngresado = Sistema.PedirDato($"{mensaje}: ");

            while (!Sistema.ValidarStringAlfabetico(nombreIngresado))
            {
                nombreIngresado = Sistema.PedirDato($"ERROR {mensaje}: ");

            }

            return nombreIngresado;
        }

        public static int PedirNumeros(string mensaje,int cantidadCaracteres)
        {
            string numeroIngresado = Sistema.PedirDato($"{mensaje}: ");
            int numeroCasteado;

            while (!int.TryParse(numeroIngresado, out numeroCasteado) || numeroCasteado > cantidadCaracteres || numeroCasteado < -1)
            {
                numeroIngresado = Sistema.PedirDato($"ERROR {mensaje}: ");

            }

            return numeroCasteado;
        }

        public static bool ValidarStringAlfabetico(string dato)
        {
            if (Regex.IsMatch(dato, "^[a-zA-Z]+$") && dato.Length < 13)
            {
                return true;

            }
            return false;
        }

        public static bool ValidarStringNumerico(string dato)
        {
            if (Regex.IsMatch(dato, "^[0-9]+$"))
            {
                return true;

            }
            return false;
        }

        public static string PedirDato(string mensaje)
        {
            Console.WriteLine(mensaje);
            string dato = Console.ReadLine();

            return dato;
        }

        public static string MenuInicio()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Bienvenido al sistema de gestion APB");
            sb.AppendLine("\n1)Alumnos");
            sb.AppendLine("2)Salir");

            return sb.ToString();

        }

        public static string MenuAlumno()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("1)Agregar alumno");
            sb.AppendLine("2)Eliminar alumno");
            sb.AppendLine("3)Buscar alumno");
            sb.AppendLine("4)Listar alumnos");
            sb.AppendLine("5)Volver atras");

            return sb.ToString();
        }

        public static string MenuAgregarNota()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Agregar Nota");
            sb.AppendLine("\n1)Parcial 1");
            sb.AppendLine("2)Parcial 2");

            return sb.ToString();

        }
        public static string MenuAlumnoBuscar()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Buscar Alumno");
            sb.AppendLine("\n1)Nombre y apellido");
            sb.AppendLine("2)Numero de legajo");
            sb.AppendLine("4)Volver atras");

            return sb.ToString();
        }

        public static void MensajeColor(string mensaje,string color = "verde")
        {
            if (color == "verde")
            {
                Console.ForegroundColor = ConsoleColor.Green;

            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine(mensaje);
            Console.ResetColor();


        }

        public static void RegistrarAlumno()
        {
            string nombre = Sistema.PedirNombre("Ingrese el nombre: ");
            string apellido = Sistema.PedirNombre("Ingrese el apellido: ");
            int edad = Sistema.PedirNumeros("Ingrese su edad", 99999);
            string legajo = Sistema.PedirDato("Ingrese el numero de legajo: ");


            ConexionBD.InsertarDatos(nombre, apellido, edad, legajo); //inserto los datos en la base de datos
        }  

        public static void ObtenerAlumnos() //consulto todos los registro en la base de datos y los guardo en una lista
        {
            Persona alumno;

            MySqlConnection conexionBD = ConexionBD.conexion();

            try
            {
                conexionBD.Open();

                string sqlConsulta = "SELECT Id, NOMBRE, APELLIDO, EDAD, LEGAJO FROM ALUMNO";

                MySqlCommand command = new MySqlCommand(sqlConsulta, conexionBD);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nombre = reader.GetString("NOMBRE");
                    string apellido = reader.GetString("APELLIDO");
                    int edad = reader.GetInt32("EDAD");
                    string legajo = reader.GetString("LEGAJO");

                    alumno = new Persona(nombre, apellido, edad, legajo);

                    listaAlumnos.Add(alumno);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los alumnos: " + ex.Message);
            }
            finally
            {
                if (conexionBD.State == ConnectionState.Open)
                {
                    conexionBD.Close();
                }
            }
        }

        public static void MostrarAlumnos() //recorro la lista y los imprimo
        {
            ObtenerAlumnos();

            foreach (var alumno in listaAlumnos)
            {
                MensajeColor(alumno.Mostrar());
                
            }


        }

    }
}
