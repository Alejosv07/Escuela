using escuela.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace escuela.Interfaces
{
    interface IProfesores : ICrudGenerico<Profesores>
    {
        Profesores listaId(int id);
        Profesores listaIdxGrado(int idGrado);
        Profesores listaLogin(string email, string contrasena);
        Profesores listaLogin(string email);
    }
}
