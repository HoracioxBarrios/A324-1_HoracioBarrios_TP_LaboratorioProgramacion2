using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IProducto
    {
        string Nombre { get; set; }
        double Cantidad { get; set; }
        decimal Precio { get; set; }
        bool Disponibilidad { get; set; }
        decimal CalcularPrecio();
        void DescontarCantidad(double cantidad);
    }
}
