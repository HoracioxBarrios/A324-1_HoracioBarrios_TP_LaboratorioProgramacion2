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
    public class Bebida : Producto, IConsumible, IConsumibleCategorizable
    {
        private string _nombre;
        private decimal _precioUnitario;
        private ITipoUnidadDeMedida _iTipoUnidadDeMedida;//Guarda la cantidad y por medio del Get : Cantidad tenemos acceso al dato
        private ETipoDeProducto _eTipoDeProducto;
        private bool _disponibilidad;
        private EUnidadMedida _eUnidadDeMedidad;
        private IProveedor _proveedor;
        private int _id;

        private ECategoriaConsumible _eCategoriaConsumible;
        private EClasificacionBebida _eClasificacionDeBebida;



        public Bebida(
            int id, string nombre, double cantidad, EUnidadMedida eUnidadDeMedida, decimal precioporCantidad, IProveedor proveedor, 
            ECategoriaConsumible categoriaDeConsumible, EClasificacionBebida clasificacionDeBebida) : base(
                id, nombre, cantidad, eUnidadDeMedida, precioporCantidad, ETipoDeProducto.Bebida, proveedor)
        {

            _nombre = nombre;
            _iTipoUnidadDeMedida = UnidadesDeMedidaServiceFactory.CrearUnidadDeMedida(eUnidadDeMedida, cantidad);
            _precioUnitario = (precioporCantidad / (decimal)cantidad);
            _proveedor = proveedor;
            _eCategoriaConsumible = categoriaDeConsumible;
            _eClasificacionDeBebida = clasificacionDeBebida;
            _eTipoDeProducto = ETipoDeProducto.Bebida;            
            _id = id;
            if (cantidad > 0){Disponibilidad = true;}
        }

        /// <summary>
        /// Calcula el precio de las bebidas.
        /// </summary>
        /// <returns>devuelve el precio de una bebida</returns>
        public override decimal CalcularPrecio()
        {
            return _precioUnitario * (decimal)_iTipoUnidadDeMedida.Cantidad;
        }


        public static Bebida operator +(Bebida bebida1, Bebida bebida2)
        {
            if (bebida1.Id == bebida2.Id)
            {
                double nuevaCantidad = bebida1.Cantidad + bebida2.Cantidad;
                decimal nuevoPrecio = bebida1._precioUnitario * (decimal)nuevaCantidad;
                return new Bebida(
                    id: bebida1.Id,
                    nombre: bebida1.Nombre,
                    cantidad: nuevaCantidad,
                    eUnidadDeMedida: bebida1.EUnidadDeMedida,
                    precioporCantidad: nuevoPrecio,
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
                decimal nuevoPrecio = bebida1._precioUnitario * (decimal)nuevaCantidad;
                return new Bebida(
                    id: bebida1.Id,
                    nombre: bebida1.Nombre,
                    cantidad: nuevaCantidad,
                    eUnidadDeMedida: bebida1.EUnidadDeMedida,
                    precioporCantidad: nuevoPrecio,
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


        /// <summary>
        /// Precio Unitario
        /// </summary>
        public decimal Precio
        {
            get { return _precioUnitario; }
            private set { _precioUnitario = value; }
        }


        public double Cantidad
        {
            get { return _iTipoUnidadDeMedida.Cantidad; }
            set { _iTipoUnidadDeMedida.Cantidad = value; }
        }
        public EUnidadMedida EUnidadMedida
        {
            get { return _eUnidadDeMedidad; }
            set { _eUnidadDeMedidad = value; }
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
            get { return _eClasificacionDeBebida; }
            set { _eClasificacionDeBebida = value; }
        }

        public ITipoUnidadDeMedida TipoDeUnidadDeMedida
        {
            get { return _iTipoUnidadDeMedida; }
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

        public ECategoriaConsumible ECategoriaDeConsumible 
        { 
            get { return _eCategoriaConsumible; }
            set { _eCategoriaConsumible= value; }
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Cantidad {Cantidad},Su Precio: {CalcularPrecio()}, Proveedor: {Proveedor}, Categoria de Consumible: {Categoria}, Clasificacion : {ClasificacionDeBebida}";
        }
    }
}
