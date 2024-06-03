using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ErrorAlConvertirException : Exception
    {

        public ErrorAlConvertirException() { }
        public ErrorAlConvertirException(string message) :base(message){ }
        public ErrorAlConvertirException (string message, Exception innerException) :base(message, innerException){ }
    }
}
