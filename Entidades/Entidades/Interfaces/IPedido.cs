using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IPedido
    {
        ETipoDePedido TipoDePedido { get; set;}
        int Id { get; set; }
        bool ListoParaEntregar { get; }
        bool Entregado { get; set; }
        void AgregarConsumible(IConsumible consumible);
        void EditarConsumible(IConsumible consumibleCorregido);
        void EditarConsumibles(List<IConsumible> nuevaListaDeConsumiblesCorregidos);
        void EliminarConsumible(IConsumible consumible);
        List<IConsumible> GetBebidas();
        List<IConsumible> GetPlatos();
        decimal CalcularPrecio();
        bool VerificarSiEsEntregable();

        event PedidoListoParaEntregarEventHandler PedidoListoParaEntregar;


    }
}
