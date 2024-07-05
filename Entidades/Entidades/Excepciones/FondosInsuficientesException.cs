using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class FondosInsuficientesException : Exception
    {

        public FondosInsuficientesException() { }

        public FondosInsuficientesException(string message) : base(message) { }
        
        public FondosInsuficientesException(string message , Exception innerException): base(message, innerException) { }
    }
}
