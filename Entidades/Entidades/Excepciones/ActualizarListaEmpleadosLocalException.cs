using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ActualizarListaEmpleadosLocalException :Exception
    {
        public ActualizarListaEmpleadosLocalException() { }

        public ActualizarListaEmpleadosLocalException(string msg) : base(msg) { }

        public ActualizarListaEmpleadosLocalException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
