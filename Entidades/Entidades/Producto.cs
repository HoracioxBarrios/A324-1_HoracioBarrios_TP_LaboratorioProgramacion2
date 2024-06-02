using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Clase Abstracta Producto :
    /// Implementa la interface IProducto, Las clases derivadas seran tambien un IProducto
    /// </summary>
    public abstract class Producto : IProducto, IProductoCreable
    {
        private static int _contadorId = 0;
        public string Nombre { get; set; }
        public double Cantidad { get; set; }
        public decimal Precio { get; set; }
        public ETipoProductoCreable TipoDeProducto { get; set; }
        public bool Disponibilidad { get; set; }
        public EUnidadMedida UnidadDeMedida { get; set; }
        public IProveedor Proveedor { get; set; }
        public int Id { get; set; }




        protected Producto()
        {
            Id = ++_contadorId;
        }

        public abstract decimal CalcularPrecio();
        public abstract void DescontarCantidad(double cantidad);

    }
}
