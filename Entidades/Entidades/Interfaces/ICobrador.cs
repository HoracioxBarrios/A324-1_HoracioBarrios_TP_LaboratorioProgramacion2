using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface ICobrador
    {
        decimal MontoAcumulado { get; set; };
        void Cobrar(decimal monto);
    }
}
