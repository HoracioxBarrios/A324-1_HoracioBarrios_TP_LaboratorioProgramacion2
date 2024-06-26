using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IMesa
    {
        int CantidadComensales { get; set; }
        EStateMesa Estado { get; set; }
        int IdDelMesero {  get; set; }
        int Id { get; set; }
        bool EstaMesaEstaAsignadaAMesero();
        void AgregarPedidoAMesa(IPedido pedido);
        void AgregarPedidosAMesa(List<IPedido> pedidos);
        List<IPedido> ObtenerPedidosDeLaMesa();

        void Cerrar();

    }
}
