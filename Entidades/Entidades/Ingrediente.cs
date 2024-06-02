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
        private ECategoriaConsumible _eCategoriaConsumible;

        public Ingrediente(
            string nombre, double cantidad, EUnidadMedida unidadMedida, decimal precio
            , IProveedor proveedor , ETipoProductoCreable tipoDeProducto)
        {
            Nombre = nombre;
            Cantidad = cantidad;
            UnidadDeMedida = unidadMedida;
            Precio = precio;
            Proveedor = proveedor;
            TipoDeProducto = tipoDeProducto;
            if(cantidad > 0)
            {
                Disponibilidad = true;
            }

        }


        public override decimal CalcularPrecio()
        {
            return Precio;
        }
        public override void DescontarCantidad(double cantidad)
        {
            throw new NotImplementedException();
        }

        public ECategoriaConsumible Categoria 
        {
            get { return _eCategoriaConsumible; }
            set { _eCategoriaConsumible = value; }
        }





        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Cantidad: {Cantidad},Precio {Precio}, Disponible: {Disponibilidad}, Unidad de Medida: {UnidadDeMedida}, Tipo de Producto: {TipoDeProducto}, Proveedor: {Proveedor.Nombre}";
        }
    }
}
