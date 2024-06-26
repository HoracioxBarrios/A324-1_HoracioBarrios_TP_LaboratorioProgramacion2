using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface ICobrador
    {
        decimal MontoAcumulado { get; }
        void Cobrar(int idMesaOCliente);// con la id puede verificar los pedidos del cliente y cobrarlos
    }
}
