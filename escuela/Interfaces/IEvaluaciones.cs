using escuela.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace escuela.Interfaces
{
    interface IEvaluaciones : ICrudGenerico<Evaluaciones>
    {
        List<Evaluaciones> ListaEvaluacionesId(Evaluaciones t,int trimestre);
        List<Evaluaciones> ListaEvaluacionesIdFinales(Evaluaciones t);

        SqlCommand cargarTabla(Evaluaciones t, int trimestre);
    }
}
