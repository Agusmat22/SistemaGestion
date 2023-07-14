using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace Logica
{
    public static class ConexionBD
    {
        public static MySqlConnection conexion()
        {
            string servidor = "localhost";
            string bd = "abm_csharp";
            string usuario = "root";
            string pass = "root2023";

            string cadenaConexion = "Database=" + bd + "; Data Source=" + 
                servidor + "; User Id=" + usuario + "; Password=" + pass + "";

            try
            {
                MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                //Console.WriteLine("Se conecto correctamente");
                return conexion;

            }
            catch (MySqlException ex) //ex es la var que contiene el error producido
            {

                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            
        }

        public static void InsertarDatos(string name, string surname, int edad, string legajo2)
        {
            //TODO ESTO SIRVE PARA GUARDAR DATOS EN LA BASE DE DATOS DE MYSQL
            

            string sql = $"INSERT INTO alumno (NOMBRE,APELLIDO,EDAD,LEGAJO) " +
                $"VALUES ('"+name+ "','"+surname+"','"+edad+ "','"+legajo2+"')"; 
                    
            MySqlConnection conexionBD2 = ConexionBD.conexion();
            conexionBD2.Open(); // abro la database

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD2);
                comando.ExecuteNonQuery(); // ejecuta el comando y inserta los valores
                Sistema.MensajeColor("Registro exitoso");

            }
            catch (MySqlException ex)
            {

                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conexionBD2.Close();  //cierro la conexion a la database

            }

        }


        public static Persona buscarRegistroBaseDatos(string codigoBusqueda)
        {
            //ESTO SIRVE PARA CONSULTA EN LA BASE DE DATOS

            Persona alumno;

            int id;
            string nombre2 = "";
            string apellido2 = "";
            int edad2 = 0;
            string legajo2 = "";

            //aca busca por ID y el @es para despues colocar una variable con el valor        (BUSCO POR NUMERO DE LEGAJO CON WHERE)
            string sqlBusqueda = "SELECT Id, NOMBRE, APELLIDO, EDAD, LEGAJO FROM ALUMNO WHERE LEGAJO = @codigoBusqueda";  

            MySqlConnection conexionBD2 = ConexionBD.conexion(); //conectamos con la base de datos con la funcion que creamos
            conexionBD2.Open(); // abro la database

            try
            {
                MySqlCommand command = new MySqlCommand(sqlBusqueda, conexionBD2); //creo el comando de control y le paso la busqueda que deseamos y la conexion
                command.Parameters.AddWithValue("@codigoBusqueda", codigoBusqueda); //por parametro le asignamos el valor que buscamos dentro del id

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // El código ingresado coincide con el ID en la tabla
                    // Puedes obtener los valores de las columnas correspondientes
                    id = reader.GetInt32("Id");
                    nombre2 = reader.GetString("NOMBRE");
                    apellido2 = reader.GetString("APELLIDO");
                    edad2 = reader.GetInt32("EDAD");
                    legajo2 = reader.GetString("LEGAJO");

                    // Realiza las acciones necesarias con los valores obtenidos
                }
                else
                {
                    // El código ingresado no coincide con ningún ID en la tabla
                    Sistema.MensajeColor($"No se ha encontrado ninguna coincidencia con {codigoBusqueda}", "red");

                }

                reader.Close();
                command.Dispose();
                conexionBD2.Close();

                alumno = new Persona(nombre2, apellido2, edad2, legajo2);

                return alumno;
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine("Ocurrió un error: " + ex.Message);
            }

            return null;

        }

        public static void EliminarAlumno(string codigoEliminacion)
        {
            MySqlConnection conexionBD2 = ConexionBD.conexion();
            conexionBD2.Open();

            string sqlEliminar = "DELETE FROM ALUMNO WHERE Id = @id";

            try
            {
                MySqlCommand command = new MySqlCommand(sqlEliminar, conexionBD2);
                command.Parameters.AddWithValue("@id", codigoEliminacion);
                command.ExecuteNonQuery();

                Console.WriteLine("El alumno ha sido eliminado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar el alumno: " + ex.Message);
            }
            finally
            {
                conexionBD2.Close();
            }
        }

    }
}
