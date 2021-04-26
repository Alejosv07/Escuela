using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace escuela.Clases
{
    interface ICrudGenerico<T>
    {
        void Agregar(T t);
        void Modificar(T t);
        void Eliminar(T t);
        List<T> Listar();
    }
}
