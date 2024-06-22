using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class NombreDelMenuException :Exception
    {
        public NombreDelMenuException() { }

        public NombreDelMenuException(string message) : base(message) { }

        public NombreDelMenuException(string mesagge, Exception innerException) : base(mesagge, innerException) { }

    }
}
