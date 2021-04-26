using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace escuela.Clases
{
    public class Materia
    {
        private int idMateria;
        private string materias;

        public Materia()
        {
        }

        public Materia(int idMateria, string materias)
        {
            this.IdMateria = idMateria;
            this.Materias = materias;
        }

        public int IdMateria { get => idMateria; set => idMateria = value; }
        public string Materias { get => materias; set => materias = value; }
    }
}