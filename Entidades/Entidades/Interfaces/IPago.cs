using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IPago
    {
        public string NombreCobrador { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }

        bool PagoPendienteAProveedor { get; }
        void EstablecerPagoPendienteAProveedorAcreedor();
        void EstablecerPagoNoPendienteAProveedor();
    }
}
