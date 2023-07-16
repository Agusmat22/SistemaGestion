using Logica;
using MySql.Data.MySqlClient;

namespace Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string botonPresionado;

            while (true)
            {
                Console.WriteLine(Sistema.MenuInicio());
                botonPresionado = Console.ReadLine();
                Console.Clear();

                if (botonPresionado == "1") // ALUMNOS
                {
                    while (true)
                    {
                        Console.WriteLine(Sistema.MenuAlumno());
                        botonPresionado = Console.ReadLine();
                        Console.Clear();
                         
                        if (botonPresionado == "1")  // REGISTRO DE ALUMNOS
                        {
                            Sistema.RegistrarAlumno();
                        }
                        else if (botonPresionado == "2") // ELIMINAR ALUMNOS (CORREGIR)
                        {
                            string datoIngresado = Sistema.PedirDato("Ingrese el numero de ID");

                            ConexionBD.EliminarAlumno(datoIngresado);
                        }
                        else if (botonPresionado == "3") //BUSCAR ALUMNOS
                        {
                            string alumnoBuscado = Sistema.PedirDato("Ingrese el numero de legajo: ");

                            Persona alumno = ConexionBD.buscarRegistroBaseDatos(alumnoBuscado);

                            if(alumno.GetNombre() != "")
                            {

                                Sistema.MensajeColor(alumno.Mostrar());
                            }
                            

                        }
                        else if (botonPresionado == "4") //MOSTRAR TODOS LOS ALUMNOS
                        {
                            Sistema.MostrarAlumnos();
                        }
                        else if (botonPresionado == "5") //SALIR
                        {
                            break;
                        }
                        else
                        {
                            Sistema.MensajeColor("Error ingrese nuevamente un numero del 1-5", "red");
                        }
                    }
                }
                else if (botonPresionado == "2")
                {
                    break;
                }
                else
                {
                    Sistema.MensajeColor("ERROR Ingrese un numero del 1-2");
                }

            }

        }
    }
}
