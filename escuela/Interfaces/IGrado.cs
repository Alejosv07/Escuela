﻿using escuela.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace escuela.Interfaces
{
    interface IGrado : ICrudGenerico<Grado>
    {
        Grado gradoId(int id);
    }
}
