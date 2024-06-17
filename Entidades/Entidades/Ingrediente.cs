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
        private decimal _precioUnitario;
        private ITipoUnidadDeMedida _iTipoUnidadDeMedida;
        private ETipoDeProducto _eTipoDeProducto;
        private bool _disponibilidad = false;
        private EUnidadDeMedida _eUnidadDeMedida;
        private IProveedor _proveedor;
        private int _contadorId = 0;
        private int _id = 0;

        //producto (string nombre, double cantidad,  EUnidadMedida eUnidadDeMedida, decimal precio, ITipoUnidadDeMedida iTipoUnidadDeMedida, ETipoDeProducto eTipoDeProducto,  IProveedor iProveedor)
        public Ingrediente(int id, string nombre, double cantidad, EUnidadDeMedida eUnidadDeMedida, decimal precioporCantidad, ETipoDeProducto tipoDeProducto, IProveedor proveedor )
        : base(id, nombre, cantidad, eUnidadDeMedida, precioporCantidad, tipoDeProducto, proveedor)
        {
            _nombre = nombre;
            _eUnidadDeMedida = eUnidadDeMedida;
            _iTipoUnidadDeMedida = UnidadesDeMedidaServiceFactory.CrearUnidadDeMedida(eUnidadDeMedida, cantidad);
            _precioUnitario = (precioporCantidad / (decimal)cantidad );
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
                decimal nuevoPrecio = ingrediente1._precioUnitario * (decimal)nuevaCantidad.Cantidad;
                return new Ingrediente(
                    id: ingrediente1.Id,
                    nombre: ingrediente1.Nombre,
                    cantidad: nuevaCantidad.Cantidad,
                    eUnidadDeMedida: ingrediente1.EUnidadDeMedida,
                    precioporCantidad: nuevoPrecio,
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

                decimal nuevoPrecio = ingrediente1._precioUnitario * (decimal)nuevaCantidad.Cantidad;
                return new Ingrediente(
                    id: ingrediente1.Id,
                    nombre: ingrediente1.Nombre,
                    cantidad: nuevaCantidad.Cantidad,
                    eUnidadDeMedida: ingrediente1.EUnidadDeMedida,
                    precioporCantidad: nuevoPrecio,
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
            return _precioUnitario * (decimal)_iTipoUnidadDeMedida.Cantidad;
        }



        /// <summary>
        /// Crea una copia con la cantidad y tipo de unidad de medida que necesita el plato
        /// </summary>
        /// <param name="cantidadNecesaria"></param>
        /// <param name="nuevaUnidadDeMedida"></param>
        /// <returns></returns>
        public Ingrediente CrearCopiaConCantidadNueva(double cantidadNecesaria, EUnidadDeMedida nuevaUnidadDeMedida)
        {
            
            ITipoUnidadDeMedida nuevaITipoUnidadDeMedida = UnidadesDeMedidaServiceFactory.CrearUnidadDeMedida(nuevaUnidadDeMedida, cantidadNecesaria);

            
            return new Ingrediente(
                id: this.Id,
                nombre: this.Nombre,
                cantidad: cantidadNecesaria,
                eUnidadDeMedida: nuevaUnidadDeMedida,
                precioporCantidad: this._precioUnitario * (decimal)cantidadNecesaria,
                tipoDeProducto: this.ETipoDeProducto,
                proveedor: this.Proveedor
            )
            {
                _iTipoUnidadDeMedida = nuevaITipoUnidadDeMedida, // Asigna la nueva unidad de medida
                _precioUnitario = this._precioUnitario // Mantiene el precio unitario original
            };
        }


        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Cantidad: {Cantidad},Su Precio {CalcularPrecio()}, Disponible: {Disponibilidad}, Unidad de Medida: {EUnidadDeMedida}, Tipo de Producto: {ETipoDeProducto}, Proveedor: {Proveedor.Nombre}";
        }
    }
}
