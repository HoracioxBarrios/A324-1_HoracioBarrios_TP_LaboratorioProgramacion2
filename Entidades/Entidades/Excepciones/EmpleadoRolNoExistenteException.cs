using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class EmpleadoRolNoExistenteException : Exception
    {

        public EmpleadoRolNoExistenteException()  : base () { }

        public EmpleadoRolNoExistenteException(string message) : base(message) 
        { 
        
        }

        public EmpleadoRolNoExistenteException(string message,  Exception innerException) : base(message, innerException) 
        {
        
        }
    }
}
