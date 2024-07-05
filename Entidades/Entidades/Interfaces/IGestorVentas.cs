using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IGestorVentas
    {

        void RegistrarCobro(ICobro pago);
        ICobro ObtenerPago(int id);
        List<ICobro> ObtenerPagos();
        decimal ObtenerMontoDeLosPagosDeLosConsumosTotales();
        decimal ObtenerMontoDeLosPagosDeDeliverys();
        decimal ObtenerMontoDeLosPagosDeMeseros();

        void CerrarTurno();
    }
}
