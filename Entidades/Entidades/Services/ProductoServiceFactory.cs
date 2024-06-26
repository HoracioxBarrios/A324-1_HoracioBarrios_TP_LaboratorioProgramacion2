﻿using Entidades.Enumerables;
using Entidades;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Excepciones;

namespace Entidades.Services
{
    public static class ProductoServiceFactory
    {
        static ProductoServiceFactory() { }


        //HAY QUE LEER ANTES DE CREAR EL PRODUCTO SI YA HAY UNO CREADO Y PASARLE LA ID AL PRODUCTO.( para crear Iproductos para el stock y para crear Iproductos(ingredientes para el plato)))
        public static IProducto CrearProducto(
              ETipoDeProducto tipoProducto, int id, string nombre, double cantidad, EUnidadDeMedida unidadDeMedida
            , decimal precio, IProveedor proveedor, ECategoriaConsumible categoria = default
            , EClasificacionBebida clasificacionDeBebida = default)
        {
            if (string.IsNullOrEmpty(nombre) || cantidad <= 0 || precio <= 0)
            {
                throw new DatosDeProductoException("Datos del Producto Exception");
            }
            switch (tipoProducto)
            {
                case ETipoDeProducto.Bebida:
                    return new Bebida(id, nombre, cantidad, unidadDeMedida, precio, proveedor, categoria, clasificacionDeBebida);
                case ETipoDeProducto.Ingrediente:
                    return new Ingrediente(id, nombre, cantidad, unidadDeMedida, precio, tipoProducto, proveedor);
                default:
                    throw new TipoDeProductoDesconocidoException("Tipo de producto no reconocido");

            }
        }

    }
}
