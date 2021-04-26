using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace escuela.Clases
{
    public class Profesores
    {
        private int idProfesores;
        private string nombre;
        private string apellido;
        private int idGrado;
        private string email;
        private string usuario;
        private string contra;
        private int nivel;

        public Profesores()
        {
        }

        public Profesores(int idProfesores, string nombre, string apellido, int idGrado, string email, string usuario, string contra, int nivel)
        {
            this.IdProfesores = idProfesores;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.IdGrado = idGrado;
            this.Email = email;
            this.Usuario = usuario;
            this.Contra = contra;
            this.Nivel = nivel;
        }

        public int IdProfesores { get => idProfesores; set => idProfesores = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public int IdGrado { get => idGrado; set => idGrado = value; }
        public string Email { get => email; set => email = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Contra { get => contra; set => contra = value; }
        public int Nivel { get => nivel; set => nivel = value; }
    }
}