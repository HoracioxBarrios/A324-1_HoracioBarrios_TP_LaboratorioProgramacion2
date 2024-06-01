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
        private static int _contadorId = 0;
        private int _id;
        private ECategoriaConsumible _eCategoriaConsumible;
        private EClasificacionBebida _ClasificacionDeBebida;
        private ETipoProductoCreable _tipoDeProducto;


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

            Id = ++_contadorId;
            TipoDeProducto = ETipoProductoCreable.Bebida;
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

        public int ID { 
            get { return _id; }
            private set { _id = value; }
        }   
        public override string ToString()
        {
            return $"ID: {ID}, Nombre: {Nombre}, Cantidad {Cantidad}, Precio: {Precio}, Proveedor: {Proveedor}, Categoria de Consumible: {Categoria}, Con Alcohol? : {ClasificacionDeBebida}";
        }
    }
}
