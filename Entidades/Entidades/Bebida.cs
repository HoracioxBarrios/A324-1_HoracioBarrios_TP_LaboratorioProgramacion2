using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;

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


        
        public Bebida(
            string nombre, double cantidad, EUnidadMedida unidadDeMedida, decimal precio, IProveedor proveedor, 
            ECategoriaConsumible categoriaDeConsumible, EClasificacionBebida clasificacionDeBebida)
        {
            Nombre = nombre;
            Cantidad = cantidad;
            UnidadDeMedida = unidadDeMedida;
            Precio = precio;
            Proveedor = proveedor;
            Categoria = categoriaDeConsumible;
            ClasificacionDeBebida = clasificacionDeBebida;

            TipoDeProducto = ETipoDeProducto.Bebida;
            
        }

        /// <summary>
        /// Calcula el Precio del Producto
        /// Ejemplo: Precio total del stock = Cantidad × Precio unitario=10×100=1000
        /// </summary>
        /// <returns></returns>
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
            set {  _eCategoriaConsumible = value;}
        }

        public EClasificacionBebida ClasificacionDeBebida
        {
            get { return _ClasificacionDeBebida; }
            set { _ClasificacionDeBebida = value; }
        }



        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Cantidad {Cantidad}, Precio: {Precio}, Proveedor: {Proveedor}, Categoria de Consumible: {Categoria}, Clasificacion : {ClasificacionDeBebida}";
        }
    }
}
