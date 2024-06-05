using Entidades.CommandMenu;
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
    public class Bebida : Producto, IConsumible, ICategoriaConsumible
    {
        private string _nombre;
        private decimal _precio;
        private EUnidadMedida _eEnidadMedida;
        private IProveedor _proveedor;
        private ECategoriaConsumible _eCategoriaConsumible;
        private EClasificacionBebida _ClasificacionDeBebida;
        private ITipoUnidadDeMedida _tipoDeUnidadDeMedida;
        private ETipoDeProducto _tipoDeProducto;
        private bool _disponibilidad;
        private int _id;
        private int _contadorId = 0;


        public Bebida(
            string nombre, double cantidad, EUnidadMedida eUnidadDeMedida, decimal precio, IProveedor proveedor, 
            ECategoriaConsumible categoriaDeConsumible, EClasificacionBebida clasificacionDeBebida)
        {

            Nombre = nombre;
            _tipoDeUnidadDeMedida = UnidadesDeMedidaServiceFactory.CrearUnidadDeMedida(eUnidadDeMedida, cantidad);
            Precio = precio;
            Proveedor = proveedor;
            Categoria = categoriaDeConsumible;
            ClasificacionDeBebida = clasificacionDeBebida;
            TipoDeProducto = ETipoDeProducto.Bebida;            
            Id = ++_contadorId;
            if (cantidad > 0)
            {
                Disponibilidad = true;
            }
        }

        /// <summary>
        /// Calcula el precio Unitario de cada bebida.
        /// </summary>
        /// <returns>devuelve el precio de una bebida</returns>
        public override decimal CalcularPrecio()
        {
            return Precio / (decimal)_tipoDeUnidadDeMedida.Cantidad;
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
                throw new InvalidOperationException("No se pueden Restar bebidas con IDs diferentes.");
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

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public decimal Precio
        {
            get { return _precio; }
            private set { _precio = value; }
        }


        public double Cantidad
        {
            get { return _tipoDeUnidadDeMedida.Cantidad; }
            set { _tipoDeUnidadDeMedida.Cantidad = value; }
        }
        public EUnidadMedida EUnidadMedida
        {
            get { return _eEnidadMedida; }
            set { _eEnidadMedida = value; }
        }
        public IProveedor Proveedor
        {
            get { return _proveedor; }
            set { _proveedor = value; }
        }


        public ECategoriaConsumible Categoria
        {
            get { return _eCategoriaConsumible; }
            set { _eCategoriaConsumible = value; }
        }

        public EClasificacionBebida ClasificacionDeBebida
        {
            get { return _ClasificacionDeBebida; }
            set { _ClasificacionDeBebida = value; }
        }

        public ITipoUnidadDeMedida TipoDeUnidadDeMedida
        {
            get { return _tipoDeUnidadDeMedida; }
        }
        public bool Disponibilidad
        {
            get { return Cantidad > 0; } 
            private set { _disponibilidad = value; }
        }
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }
        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Cantidad {Cantidad}, Precio: {Precio}, Proveedor: {Proveedor}, Categoria de Consumible: {Categoria}, Clasificacion : {ClasificacionDeBebida}";
        }
    }
}
