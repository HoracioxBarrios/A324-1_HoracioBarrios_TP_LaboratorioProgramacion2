using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface ICliente
    {
        string Nombre { get; set; }
        string Direccion { get; set; }
        string Telefono { get; set; }
        int Id { get; set; }
        int IdDelDelivery { get; set; }
        void AgregarPedidoACliente(IPedido pedido);
        List<IPedido> ObtenerPedidosDeLCliente();
    }
}
