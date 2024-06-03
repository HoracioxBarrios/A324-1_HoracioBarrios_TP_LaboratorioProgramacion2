using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ErrorAlRestarException : Exception
    {

        public ErrorAlRestarException() { }
        public ErrorAlRestarException(string message) :base(message){ }
        public ErrorAlRestarException(string message, Exception innerException) :base(message, innerException) { }
    }
}
