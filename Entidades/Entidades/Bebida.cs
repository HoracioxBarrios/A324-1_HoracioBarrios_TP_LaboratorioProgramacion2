using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;

namespace Entidades
{
    /// <summary>
    /// Class Bebida
    /// </summary>
    public class Bebida : Producto, IProducto, IConsumible
    {
        private static int _contadorId = 0;
        private readonly int _id;
        private ECategoriaConsumible _eCategoriaConsumible;
        private EClasificacionBebida _ClasificacionDeBebida;
        private ECategoriaDEProducto _tipoDeProducto;


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

            _id = ++_contadorId;
            TipoDeProducto = ECategoriaDEProducto.Bebida;
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

        public int ID { get { return _id; } }   
        public override string ToString()
        {
            return $"ID: {ID}, Nombre: {Nombre}, Cantidad {Cantidad}, Precio: {Precio}, Proveedor: {Proveedor}, Categoria de Consumible: {Categoria}, Con Alcohol? : {ClasificacionDeBebida}";
        }
    }
}
