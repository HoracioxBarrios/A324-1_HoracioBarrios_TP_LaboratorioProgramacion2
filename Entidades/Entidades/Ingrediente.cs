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
        public Ingrediente(int id, string nombre, double cantidad, EUnidadMedida eUnidadDeMedida, decimal precio, ETipoDeProducto tipoDeProducto, IProveedor proveedor )
        : base(id, nombre, cantidad, eUnidadDeMedida, precio, tipoDeProducto, proveedor)
        {
            _nombre = nombre;
            _eUnidadDeMedida = eUnidadDeMedida;
            _iTipoUnidadDeMedida = UnidadesDeMedidaServiceFactory.CrearUnidadDeMedida(eUnidadDeMedida, cantidad);
            _precio = precio;
            _proveedor = proveedor;
            _eTipoDeProducto = tipoDeProducto;
            if (Cantidad > 0) { _disponibilidad = true; }
            _id = id;


        }

        public static Ingrediente operator +(Ingrediente ingrediente1, Ingrediente ingrediente2)
        {
            if (ingrediente1.Id == ingrediente2.Id)
            {
                ITipoUnidadDeMedida nuevaCantidad;

                if (ingrediente1._iTipoUnidadDeMedida is Kilo && ingrediente2._iTipoUnidadDeMedida is Gramo)
                {
                    nuevaCantidad = (Kilo)ingrediente1._iTipoUnidadDeMedida + (Gramo)ingrediente2._iTipoUnidadDeMedida;
                }
                else if (ingrediente1._iTipoUnidadDeMedida is Gramo && ingrediente2._iTipoUnidadDeMedida is Kilo)
                {
                    nuevaCantidad = (Gramo)ingrediente1._iTipoUnidadDeMedida + (Kilo)ingrediente2._iTipoUnidadDeMedida;
                }
                else if (ingrediente1._iTipoUnidadDeMedida is Kilo && ingrediente2._iTipoUnidadDeMedida is Kilo)
                {
                    nuevaCantidad = (Kilo)ingrediente1._iTipoUnidadDeMedida + (Kilo)ingrediente2._iTipoUnidadDeMedida;
                }
                else if (ingrediente1._iTipoUnidadDeMedida is Gramo && ingrediente2._iTipoUnidadDeMedida is Gramo)
                {
                    nuevaCantidad = (Gramo)ingrediente1._iTipoUnidadDeMedida + (Gramo)ingrediente2._iTipoUnidadDeMedida;
                }
                else if (ingrediente1._iTipoUnidadDeMedida is Litro && ingrediente2._iTipoUnidadDeMedida is MiliLitro)
                {
                    nuevaCantidad = (Litro)ingrediente1._iTipoUnidadDeMedida + (MiliLitro)ingrediente2._iTipoUnidadDeMedida;
                }
                else if (ingrediente1._iTipoUnidadDeMedida is MiliLitro && ingrediente2._iTipoUnidadDeMedida is Litro)
                {
                    nuevaCantidad = (MiliLitro)ingrediente1._iTipoUnidadDeMedida + (Litro)ingrediente2._iTipoUnidadDeMedida;
                }
                else
                {
                    throw new InvalidOperationException("Tipos de unidad de medida no compatibles.");
                }

                return new Ingrediente(
                    id: ingrediente1.Id,
                    nombre: ingrediente1.Nombre,
                    cantidad: nuevaCantidad.Cantidad,
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


        public static Ingrediente operator -(Ingrediente ingrediente1, Ingrediente ingrediente2)
        {
            if (ingrediente1.Id == ingrediente2.Id)
            {
                ITipoUnidadDeMedida nuevaCantidad;// Puede ser kilo, Gramo, Litro, Mililitro (Ver si falta en Unidad como está la bebida)

                if (ingrediente1._iTipoUnidadDeMedida is Kilo && ingrediente2._iTipoUnidadDeMedida is Gramo)
                {
                    nuevaCantidad = (Kilo)ingrediente1._iTipoUnidadDeMedida - (Gramo)ingrediente2._iTipoUnidadDeMedida;
                }
                else if (ingrediente1._iTipoUnidadDeMedida is Gramo && ingrediente2._iTipoUnidadDeMedida is Kilo)
                {
                    nuevaCantidad = (Gramo)ingrediente1._iTipoUnidadDeMedida - (Kilo)ingrediente2._iTipoUnidadDeMedida;
                }
                else if (ingrediente1._iTipoUnidadDeMedida is Kilo && ingrediente2._iTipoUnidadDeMedida is Kilo)
                {
                    nuevaCantidad = (Kilo)ingrediente1._iTipoUnidadDeMedida - (Kilo)ingrediente2._iTipoUnidadDeMedida;
                }
                else if (ingrediente1._iTipoUnidadDeMedida is Gramo && ingrediente2._iTipoUnidadDeMedida is Gramo)
                {
                    nuevaCantidad = (Gramo)ingrediente1._iTipoUnidadDeMedida - (Gramo)ingrediente2._iTipoUnidadDeMedida;
                }
                else if (ingrediente1._iTipoUnidadDeMedida is Litro && ingrediente2._iTipoUnidadDeMedida is MiliLitro)
                {
                    nuevaCantidad = (Litro)ingrediente1._iTipoUnidadDeMedida - (MiliLitro)ingrediente2._iTipoUnidadDeMedida;
                }
                else if (ingrediente1._iTipoUnidadDeMedida is MiliLitro && ingrediente2._iTipoUnidadDeMedida is Litro)
                {
                    nuevaCantidad = (MiliLitro)ingrediente1._iTipoUnidadDeMedida - (Litro)ingrediente2._iTipoUnidadDeMedida;
                }
                else
                {
                    throw new InvalidOperationException("Tipos de unidad de medida no compatibles.");
                }

                return new Ingrediente(
                    id: ingrediente1.Id,
                    nombre: ingrediente1.Nombre,
                    cantidad: nuevaCantidad.Cantidad,
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
