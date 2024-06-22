using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ConsumibleNoExisteEnListaDeMenuException : Exception
    {
        public ConsumibleNoExisteEnListaDeMenuException() { }

        public ConsumibleNoExisteEnListaDeMenuException(string message) : base(message) { }

        public ConsumibleNoExisteEnListaDeMenuException(string message, Exception innerException) : base(message, innerException) { }
    }
}
