using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using Entidades.Unidades_de_Medida;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Entidades
{
    /// <summary>
    /// Class Bebida (Hereda de Producto, a su vez por gerarquia de herencia hereda la interface IProducto, e Implementa IConsumible)
    /// </summary>
    public class Bebida : Producto, IConsumible
    {   
        private ECategoriaConsumible _eCategoriaConsumible;
        private EClasificacionBebida _ClasificacionDeBebida;
        private ETipoDeProducto _tipoDeProducto;
        private IUnidadDeMedida _tipoDeUnidadDeMedida;
        private int _id;
        
        public Bebida(
            string nombre, double cantidad, EUnidadMedida eUnidadDeMedida, decimal precio, IProveedor proveedor, 
            ECategoriaConsumible categoriaDeConsumible, EClasificacionBebida clasificacionDeBebida)
        {
            Nombre = nombre;
            Precio = precio;
            Proveedor = proveedor;
            Categoria = categoriaDeConsumible;
            ClasificacionDeBebida = clasificacionDeBebida;
            TipoDeProducto = ETipoDeProducto.Bebida;

            Cantidad = cantidad;
            _tipoDeUnidadDeMedida = UnidadesDeMedidaServiceFactory.CrearUnidadDeMedida(eUnidadDeMedida, cantidad);

        }

        /// <summary>
        /// Calcula base a su cantidad el precio total del producto.
        /// Este método multiplica el precio unitario por la cantidad disponible.
        /// Ejemplo: Si la cantidad es 10 y el precio unitario es 100, el precio total del stock será 10 × 100 = 1000.
        /// </summary>
        /// <returns>El precio total del stock.</returns>
        public override decimal CalcularPrecio()
        {
            return Precio * (decimal)_tipoDeUnidadDeMedida.Cantidad;
        }

        public static Bebida operator +(Bebida bebida1, Bebida bebida2)
        {
            if (bebida1.Id == bebida2.Id)
            {
                double nuevaCantidad = bebida1.Cantidad + bebida2.Cantidad;
                return new Bebida(
                    nombre: bebida1.Nombre,
                    cantidad: nuevaCantidad,
                    eUnidadDeMedida: bebida1.EUnidadDeMedida,
                    precio: bebida1.Precio,
                    proveedor: bebida1.Proveedor,
                    categoriaDeConsumible: bebida1.Categoria,
                    clasificacionDeBebida: bebida1.ClasificacionDeBebida
                );
            }
            else
            {
                throw new InvalidOperationException("No se pueden sumar bebidas con IDs diferentes.");
            }
        }

        public static Bebida operator -(Bebida bebida1, Bebida bebida2)
        {
            if (bebida1.Id == bebida2.Id)
            {
                double nuevaCantidad = bebida1.Cantidad - bebida2.Cantidad;
                return new Bebida(
                    nombre: bebida1.Nombre,
                    cantidad: nuevaCantidad,
                    eUnidadDeMedida: bebida1.EUnidadDeMedida,
                    precio: bebida1.Precio,
                    proveedor: bebida1.Proveedor,
                    categoriaDeConsumible: bebida1.Categoria,
                    clasificacionDeBebida: bebida1.ClasificacionDeBebida
                );
            }
            else
            {
                throw new InvalidOperationException("No se pueden Restar bebidas con IDs diferentes.");
            }
        }



        public ECategoriaConsumible Categoria 
        { 
            get { return _eCategoriaConsumible; }
            set {  _eCategoriaConsumible = value;}
        }

        public EClasificacionBebida ClasificacionDeBebida
        {
            get { return _ClasificacionDeBebida; }
            set { _ClasificacionDeBebida = value; }
        }

        new public double Cantidad
        {
            get { return _tipoDeUnidadDeMedida.Cantidad; }
            set { _tipoDeUnidadDeMedida.Cantidad = value; }
        }
        public int Id
        {
            get { return _id; }
            private set { _id = value;}
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Cantidad {Cantidad}, Precio: {Precio}, Proveedor: {Proveedor}, Categoria de Consumible: {Categoria}, Clasificacion : {ClasificacionDeBebida}";
        }
    }
}
