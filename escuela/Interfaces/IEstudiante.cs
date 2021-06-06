using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace escuela.Clases
{
    interface IEstudiante : ICrudGenerico<Estudiante>
    {
        Estudiante listaId(int id);
        Estudiante listaLogin(string email,string contrasena);
        Estudiante listaLogin(string email);

        List<Estudiante> ListarGrado(int idGrado);
    }
}
