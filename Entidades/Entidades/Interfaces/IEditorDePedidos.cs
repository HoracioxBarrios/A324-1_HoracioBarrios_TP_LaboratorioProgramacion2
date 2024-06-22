using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IEditorDePedidos
    {
        bool EditarPedido(int id, Queue<IPedido> pedidos, List<IConsumible> consumiblesCorregidos);
    }
}
