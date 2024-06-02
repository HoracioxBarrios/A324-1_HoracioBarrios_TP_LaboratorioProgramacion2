using System;
using System.Collections.Generic;
using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;

namespace Negocio
{
    public class GestorDeProductos
    {
        List<IProducto> _listProductosStock;
        private decimal _precioTotalStock = 0;
        public GestorDeProductos()
        {
            _listProductosStock = new List<IProducto>();
        }

        public IProducto CrearProducto(
              ETipoProductoCreable tipoProducto, string nombre, double cantidad, EUnidadMedida unidadDeMedida
            , decimal precio, IProveedor proveedor, ECategoriaConsumible categoria = default
            , EClasificacionBebida clasificacionDeBebida = default)
        {
            try
            {
                IProducto producto = ProductoServiceFactory.CrearProducto(tipoProducto, nombre, cantidad, unidadDeMedida, precio, proveedor, categoria, clasificacionDeBebida);
                _listProductosStock.Add(producto);
                return producto;

            }
            catch (ErrorDatosDeProductoException ex)
            {
                throw new ErrorAlCrearProductoException("Error al crear el Producto ", ex);
            }
            catch(ErrorTipoDeProductoDesconocidoException ex)
            {
                throw new ErrorAlCrearProductoException("Error al crear el Producto ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Inesperado ", ex);
            }

        }
        public decimal CalcularPrecio()
        {
            if(_listProductosStock.Count > 0)
            {
                foreach(IProducto producto in _listProductosStock)
                {
                    _precioTotalStock += producto.CalcularPrecio();
                }
            }
            return _precioTotalStock;
        }

        public List<IProducto> GetProductos()
        {
            if(_listProductosStock.Count > 0)
            {
                return _listProductosStock;
            }
            throw new ListaVaciaException("La lista esta vacia");
        }

        public IProducto GetProducto(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new DatoIncorrectoException("Dato Incorrecto: Nombre");
            }
            foreach(IProducto producto in _listProductosStock)
            {
                if(string.Equals(producto.Nombre, nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return producto;
                }
            }
            return null;
        }
        public IProducto GetProducto(int id)
        {
            if (id < 0)
            {
                throw new DatoIncorrectoException("Dato Incorrecto: ID no valida");
            }
            foreach(Producto producto in GetProductos()) 
            { 
                if(producto.Id == id)
                {
                    return producto;
                }
            }
            return null;
        }

    }
}
