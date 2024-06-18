using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IModificadorDePedidos
    {
        void EditarPedido(int id);
        void EditarPedido(int id, ETipoDePedido tipoDePedido);
        void EditarPedido(int id, ETipoDePedido tipoDePedido, IPedido pedido);
        void EliminarPedido(int id);
    }
}
