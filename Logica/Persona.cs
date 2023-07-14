

using System.Text;

namespace Logica
{
    public class Persona
    {
        private string nombre;
        private string apellido;
        private int edad;
        private string numeroLegajo;
        private int notaPrimerParcial;
        private int notaSegundoParcial;


        public Persona(string nombre, string apellido, int edad,string numeroLegajo)
        {
            this.nombre = nombre;
            this.apellido = apellido; 
            this.numeroLegajo = numeroLegajo;
            this.edad = edad;
            notaPrimerParcial = 0;
            notaSegundoParcial = 0;
        }


        public string Mostrar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nombre: {GetNombre()}");
            sb.AppendLine($"Apellido: {GetApellido()}");
            sb.AppendLine($"Edad: {GetEdad()}");
            sb.AppendLine($"Legajo: {GetNumeroLegajo()}");

            return sb.ToString();
        }

        public decimal CalcularPromedio()
        {
            decimal promedio = 0;

            if (GetNotaPrimerParcial() != 0 && GetNotaSegundoParcial() != 0) 
            {
            
                promedio = ( GetNotaPrimerParcial() + GetNotaSegundoParcial() )/ 2;  
            
            }
            
            return promedio;    
        }

        public string GetNombre() 
        {
            return nombre;
        }

        public void SetNombre(string valor)
        {
            nombre = valor;

        }

        public string GetApellido()
        {
            return apellido;
        }

        public void SetApellido(string valor)
        {
            apellido = valor;

        }

        public string GetNumeroLegajo()
        {
            return numeroLegajo;
        }

        public int GetEdad()
        {
            return edad;
        }

        public void SetEdad(int valor)
        {
            edad = valor;
        }

        public int GetNotaPrimerParcial()
        {
            return notaPrimerParcial;
        }

        public void SetNotaPrimerParcial(int nota)
        {
            notaPrimerParcial = nota;

        }

        public int GetNotaSegundoParcial()
        {
            return notaSegundoParcial;
        }
        public void SetNotaSegundoParcial(int nota)
        {
            notaSegundoParcial = nota;
        }



    }
}