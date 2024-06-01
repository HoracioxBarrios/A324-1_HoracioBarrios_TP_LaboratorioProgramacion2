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
    /// Clase Ingrediente que Hereda de producto. 
    /// Por herencia hereda tambien la interface IProducto(Por lo que tambien es un Iproducto). 
    /// Tambien Implementa la interface IConsumible
    /// </summary>
    public class Ingrediente : Producto, IConsumible
    {
        private static int _contadorId = 0;
        private int _id;
        private ECategoriaConsumible _eCategoriaConsumible;

        public Ingrediente(
            string nombre, double cantidad, EUnidadMedida unidadMedida, decimal precio
            , IProveedor proveedor , ETipoProductoCreable tipoDeProducto, bool disponibilidad)
        {
            Nombre = nombre;
            Cantidad = cantidad;
            UnidadDeMedida = unidadMedida;
            Proveedor = proveedor;
            TipoDeProducto = tipoDeProducto;
            Disponibilidad = disponibilidad;

            Id = ++_contadorId;
        }


        public ECategoriaConsumible Categoria 
        {
            get { return _eCategoriaConsumible; }
            set { _eCategoriaConsumible = value; }
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Cantidad: {Cantidad}, Unidad de Medida: {UnidadDeMedida}, Tipo de Producto:{TipoDeProducto}, Proveedor: {Proveedor}, Disponible: {Disponibilidad}";
        }
    }
}
