using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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

        public string Nombre { get; set; }
        public double Cantidad { get; set; }
        public decimal Precio { get; set; }
        public ITipoUnidadDeMedida TipoDeUnidadDeMedida { get; set; }
        public ETipoDeProducto TipoDeProducto { get; set; }
        public bool Disponibilidad { get; set; }
        public EUnidadMedida EUnidadDeMedida { get; set; }
        public IProveedor Proveedor { get; set; }
        public int Id { get; set; }

        private int _contadorId = 0;




        protected Producto()
        {
            Id = ++_contadorId;
        }

        public abstract decimal CalcularPrecio();



    }
}
