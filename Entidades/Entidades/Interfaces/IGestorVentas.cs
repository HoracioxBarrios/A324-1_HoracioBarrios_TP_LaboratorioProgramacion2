using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IGestorVentas
    {

        void RegistrarPago(IPago pago);
        IPago ObtenerPago(int id);
        List<IPago> ObtenerPagos();
        decimal ObtenerMontoDeLosPagosDeLosConsumosTotales();
        decimal ObtenerMontoDeLosPagosDeDeliverys();
        decimal ObtenerMontoDeLosPagosDeMeseros();
    }
}
