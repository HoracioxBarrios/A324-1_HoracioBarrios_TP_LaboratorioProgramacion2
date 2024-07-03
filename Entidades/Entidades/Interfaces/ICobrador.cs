using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface ICobrador
    {
        decimal MontoAcumulado { get; set; }
        IPago Cobrar(int idMesaOCliente, ETipoDePago tipoPago);
    }
}
