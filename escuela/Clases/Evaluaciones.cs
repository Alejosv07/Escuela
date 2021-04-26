using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace escuela.Clases
{
    public class Evaluaciones
    {
        private int idEvaluaciones;
        private double evaluacion1;
        private double evaluacion2;
        private double evaluacion3;
        private double recuperacion;
        private int idTrimestre;
        private int idMateria;
        private int idAlumno;
        private int idProfesores;

        public Evaluaciones()
        {
        }

        public Evaluaciones(int idEvaluaciones, double evaluacion1, double evaluacion2, double evaluacion3, double recuperacion, int idTrimestre, int idMateria, int idAlumno, int idProfesores)
        {
            this.IdEvaluaciones = idEvaluaciones;
            this.Evaluacion1 = evaluacion1;
            this.Evaluacion2 = evaluacion2;
            this.Evaluacion3 = evaluacion3;
            this.Recuperacion = recuperacion;
            this.IdTrimestre = idTrimestre;
            this.IdMateria = idMateria;
            this.IdAlumno = idAlumno;
            this.IdProfesores = idProfesores;
        }

        public int IdEvaluaciones { get => idEvaluaciones; set => idEvaluaciones = value; }
        public double Evaluacion1 { get => evaluacion1; set => evaluacion1 = value; }
        public double Evaluacion2 { get => evaluacion2; set => evaluacion2 = value; }
        public double Evaluacion3 { get => evaluacion3; set => evaluacion3 = value; }
        public double Recuperacion { get => recuperacion; set => recuperacion = value; }
        public int IdTrimestre { get => idTrimestre; set => idTrimestre = value; }
        public int IdMateria { get => idMateria; set => idMateria = value; }
        public int IdAlumno { get => idAlumno; set => idAlumno = value; }
        public int IdProfesores { get => idProfesores; set => idProfesores = value; }
    }
}