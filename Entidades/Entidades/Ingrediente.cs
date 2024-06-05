using Entidades.Enumerables;
using Entidades.Interfaces;
using Entidades.Unidades_de_Medida;
using Entidades.Utilidades;
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
        private ITipoUnidadDeMedida _tipoDeUnidadDeMedida;
        private decimal _precio;
        private ETipoDeProducto _tipoDeProducto;
        private bool _disponibilidad;
        private EUnidadMedida _eUnidadDeMedida;
        private IProveedor _proveedor;
        private int _contadorId = 0;
        private int _id = 0;


        public Ingrediente(
            string nombre, double cantidad, EUnidadMedida eUnidadMedida, decimal precio
            , IProveedor proveedor, ETipoDeProducto tipoDeProducto)
        {
            Nombre = nombre;
            Precio = precio;
            _tipoDeUnidadDeMedida = UnidadesDeMedidaServiceFactory.CrearUnidadDeMedida(eUnidadMedida, cantidad);


            Proveedor = proveedor;
            TipoDeProducto = tipoDeProducto;

            Id = ++_contadorId;
            if (cantidad > 0)
            {
                Disponibilidad = true;
            }




        }//Tengo que hacer: como con bebida. 1ro poner en orden las Unidades de medida como hice en Bebida. 2do hacer sobrecarga de Ingredientes para que si son de la misma Id se resten entre ellos. de los platos.
        public static Ingrediente operator +(Ingrediente ingrediente1, Ingrediente ingrediente2)
        {
            if (ingrediente1.Id == ingrediente2.Id)
            {
                double nuevaCantidad = ingrediente1.Cantidad + ingrediente2.Cantidad;
                return new Ingrediente(
                    nombre: ingrediente1.Nombre,
                    cantidad: nuevaCantidad,
                    eUnidadMedida: ingrediente1.EUnidadMedida,
                    precio: ingrediente1.Precio,
                    proveedor: ingrediente1.Proveedor,
                    tipoDeProducto: ingrediente1.TipoDeProducto
                );
            }
            else
            {
                throw new InvalidOperationException("No se pueden sumar ingredientes con IDs diferentes.");
            }
        }
        public static Ingrediente operator -(Ingrediente ingrediente1, Ingrediente ingrediente2)
        {
            if (ingrediente1.Id == ingrediente2.Id)
            {
                double nuevaCantidad = ingrediente1.Cantidad - ingrediente2.Cantidad;
                return new Ingrediente(
                    nombre: ingrediente1.Nombre,
                    cantidad: nuevaCantidad,
                    eUnidadMedida: ingrediente1.EUnidadMedida,
                    precio: ingrediente1.Precio,
                    proveedor: ingrediente1.Proveedor,
                    tipoDeProducto: ingrediente1.TipoDeProducto
                );
            }
            else
            {
                throw new InvalidOperationException("No se pueden restar ingredientes con IDs diferentes.");
            }
        }

        public override decimal CalcularPrecio()
        {
            return Precio / (decimal)_tipoDeUnidadDeMedida.Cantidad;
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public double Cantidad
        {
            get { return _tipoDeUnidadDeMedida.Cantidad; }
            set { _tipoDeUnidadDeMedida.Cantidad = value; }
        }
        public ETipoDeProducto TipoDeProducto
        {
            get { return _tipoDeProducto; }
            set { _tipoDeProducto = value; }
        }
        public ITipoUnidadDeMedida TipoUnidadDeMedida
        {
            get { return _tipoDeUnidadDeMedida; }
            set { _tipoDeUnidadDeMedida = value; }
        }


        public EUnidadMedida EUnidadMedida
        {
            get { return _eUnidadDeMedida; }
            set { _eUnidadDeMedida = value; }
            
        }
        public bool Disponibilidad
        {
            get { return _disponibilidad; }
            set { _disponibilidad = value; }
        }
        public IProveedor Proveedor
        {
            get { return _proveedor; }
            set { _proveedor = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public override string ToString()
        {
            return $"Id: {_id}, Nombre: {Nombre}, Cantidad: {Cantidad},Precio {Precio}, Disponible: {Disponibilidad}, Unidad de Medida: {EUnidadMedida}, Tipo de Producto: {TipoDeProducto}, Proveedor: {Proveedor.Nombre}";
        }
    }
}
