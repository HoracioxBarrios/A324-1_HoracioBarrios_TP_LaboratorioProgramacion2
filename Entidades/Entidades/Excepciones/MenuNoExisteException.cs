using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class MenuNoExisteException : Exception
    {
        public MenuNoExisteException() { }
        public MenuNoExisteException(string message) : base(message) { }

        public MenuNoExisteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
