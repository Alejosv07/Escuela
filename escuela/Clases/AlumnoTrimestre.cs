using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace escuela.Clases
{
    public class AlumnoTrimestre
    {
        private int idAlumnoTrimestre;
        private int idAlumno;
        private int idTrimestre;

        public AlumnoTrimestre()
        {
        }

        public AlumnoTrimestre(int idAlumnoTrimestre, int idAlumno, int idTrimestre)
        {
            this.IdAlumnoTrimestre = idAlumnoTrimestre;
            this.IdAlumno = idAlumno;
            this.IdTrimestre = idTrimestre;
        }

        public int IdAlumnoTrimestre { get => idAlumnoTrimestre; set => idAlumnoTrimestre = value; }
        public int IdAlumno { get => idAlumno; set => idAlumno = value; }
        public int IdTrimestre { get => idTrimestre; set => idTrimestre = value; }
    }
}