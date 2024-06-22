using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ElPlatoNoExisteException : Exception
    {
        public ElPlatoNoExisteException() { }
        public ElPlatoNoExisteException(string message) : base(message) { }
        public ElPlatoNoExisteException(string message, Exception innerException): base(message, innerException) { }
    }
}
