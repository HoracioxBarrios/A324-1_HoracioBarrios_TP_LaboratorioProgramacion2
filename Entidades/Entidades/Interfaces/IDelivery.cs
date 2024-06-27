using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IDelivery
    {
        string Nombre { get; set; }
        int Id { get; set; }

        void RecibirCliente(ICliente cliente);
    }
}
