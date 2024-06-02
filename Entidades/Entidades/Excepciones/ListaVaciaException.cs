using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ListaVaciaException : Exception
    {
        public ListaVaciaException() { }
        public ListaVaciaException(string message) : base(message) { }
        public ListaVaciaException(string message, Exception innerException) :base(message, innerException) { }
    }
}
