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
        private decimal _precio;
        private ITipoUnidadDeMedida _iTipoUnidadDeMedida;
        private ETipoDeProducto _eTipoDeProducto;
        private bool _disponibilidad = false;
        private EUnidadMedida _eUnidadDeMedida;
        private IProveedor _proveedor;
        private int _contadorId = 0;
        private int _id = 0;

        //producto (string nombre, double cantidad,  EUnidadMedida eUnidadDeMedida, decimal precio, ITipoUnidadDeMedida iTipoUnidadDeMedida, ETipoDeProducto eTipoDeProducto,  IProveedor iProveedor)
        public Ingrediente(string nombre, double cantidad, EUnidadMedida eUnidadDeMedida, decimal precio, ETipoDeProducto tipoDeProducto, IProveedor proveedor )
        : base(nombre, cantidad, eUnidadDeMedida, precio, tipoDeProducto, proveedor)
        {
            _nombre = nombre;
            _eUnidadDeMedida = eUnidadDeMedida;
            _iTipoUnidadDeMedida = UnidadesDeMedidaServiceFactory.CrearUnidadDeMedida(eUnidadDeMedida, cantidad);
            _precio = precio;
            _proveedor = proveedor;
            _eTipoDeProducto = tipoDeProducto;
            if (Cantidad > 0) { _disponibilidad = true; }
            _id = ++_contadorId;


        }
    public static Ingrediente operator +(Ingrediente ingrediente1, Ingrediente ingrediente2)
        {
            if (ingrediente1.Id == ingrediente2.Id)
            {
                double nuevaCantidad = ingrediente1.Cantidad + ingrediente2.Cantidad;
                return new Ingrediente(
                    nombre: ingrediente1.Nombre,
                    cantidad: nuevaCantidad,
                    eUnidadDeMedida: ingrediente1.EUnidadDeMedida,
                    precio: ingrediente1.Precio,
                    proveedor: ingrediente1.Proveedor,
                    tipoDeProducto: ingrediente1.ETipoDeProducto
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
                    eUnidadDeMedida: ingrediente1.EUnidadDeMedida,
                    precio: ingrediente1.Precio,
                    proveedor: ingrediente1.Proveedor,
                    tipoDeProducto: ingrediente1.ETipoDeProducto
                );
            }
            else
            {
                throw new InvalidOperationException("No se pueden restar ingredientes con IDs diferentes.");
            }
        }

        public override decimal CalcularPrecio()
        {
            return Precio / (decimal)_iTipoUnidadDeMedida.Cantidad;
        }


        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Cantidad: {Cantidad},Precio {Precio}, Disponible: {Disponibilidad}, Unidad de Medida: {EUnidadDeMedida}, Tipo de Producto: {ETipoDeProducto}, Proveedor: {Proveedor.Nombre}";
        }
    }
}
