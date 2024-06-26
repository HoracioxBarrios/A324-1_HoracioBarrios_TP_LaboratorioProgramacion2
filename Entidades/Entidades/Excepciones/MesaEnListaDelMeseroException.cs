using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class MesaEnListaDelMeseroException :Exception
    {
        public MesaEnListaDelMeseroException() { }
        public MesaEnListaDelMeseroException(string message) : base(message) { }

        public MesaEnListaDelMeseroException(string message, Exception innerException) : base(message, innerException) { }
    }
}
