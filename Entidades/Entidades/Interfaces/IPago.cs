using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IPago
    {
        int Id { get; set; }
        int IdMesaOCliente { get; set; }
        decimal Monto { get; set; }
        DateTime Fecha { get; set; }
        int IdDelCobrador { get; set; }
        ERol RolDelCobrador { get; set; }
        ETipoDePago TipoPago { get; set; }
    }
}
