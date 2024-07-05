using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IGestorContable
    {
        void CobrarPagosDeLasVentasDelTurno(List<ICobro> pagosDeLasVentasDelTurno);
        void Pagar(decimal montoAPagar);
        decimal ObtenerMontoDisponible();

        void PagarProveedor(IProducto producto, IProveedor proveedor);
    }
}
