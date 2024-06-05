using Entidades.Enumerables;
using Entidades.Interfaces;
using Negocio;
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
        private string _nombre;
        private decimal _precio;
        private IUnidadDeMedida _tipoDeUnidadDeMedida;
        private IProveedor _proveedor;
        private ETipoDeProducto _tipoDeProducto;
        private int _id;


        public Ingrediente(
            string nombre, double cantidad, EUnidadMedida eUnidadMedida, decimal precio
            , IProveedor proveedor, ETipoDeProducto tipoDeProducto)
        {
            Nombre = nombre;
            Precio = precio;
            _tipoDeUnidadDeMedida = UnidadesDeMedidaServiceFactory.CrearUnidadDeMedida(eUnidadMedida, cantidad);
            Proveedor = proveedor;
            TipoDeProducto = tipoDeProducto;
            //if (cantidad > 0)
            //{
            //    Disponibilidad = true;
            //}




        }//Tengo que hacer: como con bebida. 1ro poner en orden las Unidades de medida como hice en Bebida. 2do hacer sobrecarga de Ingredientes para que si son de la misma Id se resten entre ellos. de los platos.

        public static Ingrediente operator +(Ingrediente ingrediente, Bebida ingrediente2)
        {
            if (ingrediente.Id == ingrediente2.Id)
            {
                double nuevaCantidad = ingrediente.Cantidad + ingrediente2.Cantidad;
                return new Ingrediente(
                    nombre: ingrediente.Nombre,
                    cantidad: nuevaCantidad,
                    eUnidadMedida: ingrediente.EUnidadDeMedida,
                    precio: ingrediente.Precio,
                    proveedor: ingrediente.Proveedor
                    TipoDeProducto = ingrediente.TipoDeProducto

                );
            }
            else
            {
                throw new InvalidOperationException("No se pueden Restar bebidas con IDs diferentes.");
            }
        }



        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public override decimal CalcularPrecio()
        {
            return Precio / (decimal)_tipoDeUnidadDeMedida.Cantidad;
        }

        

        new public double Cantidad
        {
            get { return _tipoDeUnidadDeMedida.Cantidad; }
            set { _tipoDeUnidadDeMedida.Cantidad = value; }
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Cantidad: {Cantidad},Precio {Precio}, Disponible: {Disponibilidad}, Unidad de Medida: {UnidadDeMedida}, Tipo de Producto: {TipoDeProducto}, Proveedor: {Proveedor.Nombre}";
        }
    }
}
