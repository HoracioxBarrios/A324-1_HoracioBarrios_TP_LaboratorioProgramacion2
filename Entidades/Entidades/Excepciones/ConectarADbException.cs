using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ConectarADbException :Exception
    {

        public ConectarADbException() { }
        public ConectarADbException(string message) : base(message) { }
        public ConectarADbException(string message, Exception innerException): base(message, innerException) { }
    }
}
