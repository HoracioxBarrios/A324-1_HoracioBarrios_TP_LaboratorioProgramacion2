using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlEliminarDeFormaLogicaEnDBException : Exception
    {

        public AlEliminarDeFormaLogicaEnDBException() { }

        public AlEliminarDeFormaLogicaEnDBException(string message) : base(message) { }

        public AlEliminarDeFormaLogicaEnDBException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
