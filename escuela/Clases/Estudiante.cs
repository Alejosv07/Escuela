using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace escuela
{
    public class Estudiante
    {
        private int idAlumno;
        private string nombre;
        private string apellido;
        private string carnet;
        private int grado;
        private string responsableNombre;
        private string responsableApellido;
        private string email;
        private string usuario;
        private string contra;

        public Estudiante()
        {
        }

        public Estudiante(int idAlumno, string nombre, string apellido, string carnet, int grado, string responsableNombre, string responsableApellido, string email, string usuario, string contra)
        {
            this.IdAlumno = idAlumno;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Carnet = carnet;
            this.Grado = grado;
            this.ResponsableNombre = responsableNombre;
            this.ResponsableApellido = responsableApellido;
            this.Email = email;
            this.Usuario = usuario;
            this.Contra = contra;
        }

        public int IdAlumno { get => idAlumno; set => idAlumno = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Carnet { get => carnet; set => carnet = value; }
        public int Grado { get => grado; set => grado = value; }
        public string ResponsableNombre { get => responsableNombre; set => responsableNombre = value; }
        public string ResponsableApellido { get => responsableApellido; set => responsableApellido = value; }
        public string Email { get => email; set => email = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Contra { get => contra; set => contra = value; }
    }
}