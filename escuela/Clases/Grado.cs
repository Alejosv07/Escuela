using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace escuela.Clases
{
    public class Grado
    {
        private int idGrado;
        private string nombre;
        private string seccion;

        public Grado()
        {
        }

        public Grado(int idGrado, string nombre, string seccion)
        {
            this.IdGrado = idGrado;
            this.Nombre = nombre;
            this.Seccion = seccion;
        }

        public int IdGrado { get => idGrado; set => idGrado = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Seccion { get => seccion; set => seccion = value; }
    }
}