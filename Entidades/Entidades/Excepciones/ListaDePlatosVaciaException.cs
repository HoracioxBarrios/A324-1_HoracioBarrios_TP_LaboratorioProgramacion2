using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ListaDePlatosVaciaException : Exception
    {
        public ListaDePlatosVaciaException() { }
        public ListaDePlatosVaciaException(string message) : base(message) { }
        public ListaDePlatosVaciaException(string message, Exception innerexception): base(message, innerexception) { } 
    }
}
