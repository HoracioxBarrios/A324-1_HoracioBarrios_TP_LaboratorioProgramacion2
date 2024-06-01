using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Clase Publica Abstracta:  Producto
    /// </summary>
    public abstract class Producto : IProducto
    {

        public string Nombre { get; set; }
        public double Cantidad { get; set; }
       
        public Proveedor Proveedor { get; set; }
        public int Id { get; set; }
        public decimal Precio { get; set; }

        public EDisponibilidad EDisponibilidad { get; set; }
        public EUnidadMedida EUnidadMedida { get ; set ; }
    }
}
