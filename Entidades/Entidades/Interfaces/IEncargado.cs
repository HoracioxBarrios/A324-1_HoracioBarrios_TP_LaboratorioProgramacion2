using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IEncargado
    {
        void AsignarMesaAMesero(IMesa mesa, IMesero mesero);
        void AsignarClienteADelivery(ICliente cliente, IDelivery delivery);
    }
}
