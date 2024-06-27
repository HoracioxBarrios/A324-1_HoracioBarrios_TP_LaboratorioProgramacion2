using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IGestorDePedidos
    {
        void CrearPedido(ETipoDePedido tipoDePedido, List<IConsumible> ConsumiblesParaElPEdido);
        void AgregarConsumibleAlPedido(int id, IConsumible consumible);
        void GetPedidoPorId(int id);
        void EditarPedido(int id);
        void EditarPedido(int id, ETipoDePedido tipoDePedido);
        void EditarPedido(int id, ETipoDePedido tipoDePedido, IPedido pedido);
        void EliminarPedido(int id);

        IPedido TomarPedidoSinPrepararAunParaDelivery();

        IPedido TomarPedidoSinPrepararAunParaElLocal();
    }
}
