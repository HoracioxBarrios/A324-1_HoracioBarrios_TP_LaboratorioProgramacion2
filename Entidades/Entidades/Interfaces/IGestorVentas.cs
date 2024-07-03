using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IGestorVentas
    {
        List<IPago> Pagos { get; set; }

        IPago ObtenerPago(int id);
    }
}
