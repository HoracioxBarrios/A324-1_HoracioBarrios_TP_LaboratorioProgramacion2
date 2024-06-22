using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IEliminadorDePedidos
    {
        bool EliminarPedido(int id, Queue<IPedido> pedidos);
    }
}
