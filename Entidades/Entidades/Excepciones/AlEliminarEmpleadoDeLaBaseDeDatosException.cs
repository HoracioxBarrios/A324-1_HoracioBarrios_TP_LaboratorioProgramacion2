using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlEliminarEmpleadoDeLaBaseDeDatosException : Exception
    {
        public AlEliminarEmpleadoDeLaBaseDeDatosException() { }
        public AlEliminarEmpleadoDeLaBaseDeDatosException(string msg) { }
        public AlEliminarEmpleadoDeLaBaseDeDatosException(string msg, Exception innerException): base(msg, innerException) { }
    }
}
