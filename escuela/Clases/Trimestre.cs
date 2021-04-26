using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace escuela.Clases
{
    public class Trimestre
    {
        private int idTrimestre;
        private int numeroTrimestre;

        public Trimestre()
        {
        }

        public Trimestre(int idTrimestre, int numeroTrimestre)
        {
            this.IdTrimestre = idTrimestre;
            this.NumeroTrimestre = numeroTrimestre;
        }

        public int IdTrimestre { get => idTrimestre; set => idTrimestre = value; }
        public int NumeroTrimestre { get => numeroTrimestre; set => numeroTrimestre = value; }
    }
}