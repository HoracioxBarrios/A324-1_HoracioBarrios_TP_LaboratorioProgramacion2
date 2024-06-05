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

        private string _servicioDeGuardado = "";
        public GestorDeProductos() //Puede esperar un servicio a DB o a Archivo? _servicioDeGuardado
        {
            _listProductosStock = new List<IProducto>();
        }

        public IProducto CrearProducto(
              ETipoDeProducto tipoProducto, string nombre, double cantidad, EUnidadMedida unidadDeMedida
            , decimal precio, IProveedor proveedor, ECategoriaConsumible categoria = default
            , EClasificacionBebida clasificacionDeBebida = default)
        {
            try
            {
                IProducto producto = ProductoServiceFactory.CrearProducto(tipoProducto, nombre, cantidad, unidadDeMedida, precio, proveedor, categoria, clasificacionDeBebida);
                _listProductosStock.Add(producto);
                return producto;

            }
            catch (DatosDeProductoException ex)
            {
                throw new AlCrearProductoException("Error al crear el Producto ", ex);
            }
            catch(TipoDeProductoDesconocidoException ex)
            {
                throw new AlCrearProductoException("Error al crear el Producto ", ex);
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



        public static void ActualizarListaOriginal(List<IProducto> listaDeProductosIngredientesStock, List<IProducto> productosActualizados)
        {
            for (int i = 0; i < listaDeProductosIngredientesStock.Count; i++)
            {
                listaDeProductosIngredientesStock[i] = productosActualizados[i];
            }
        }

        public static List<IProducto> DescontarProductos(List<IProducto> listaDeProductosIngredientesStock, List<IProducto> listaDeIngredienteEnElPlato)
        {
            List<IProducto> productosActualizados = new List<IProducto>();

            foreach (IProducto producto in listaDeProductosIngredientesStock)
            {
                if (producto is Ingrediente ingrediente)
                {
                    bool encontrado = false;
                    foreach (IProducto productoADescontar in listaDeIngredienteEnElPlato)
                    {
                        if (productoADescontar is Ingrediente ingredienteADescontar)
                        {
                            if (ingrediente.Id == ingredienteADescontar.Id)
                            {
                                Ingrediente nuevoIngrediente = ingrediente - (Ingrediente)productoADescontar;
                                productosActualizados.Add(nuevoIngrediente);
                                encontrado = true;
                                break; // Salir del bucle si se encontro el producto a descontar
                            }
                        }
                    }
                    if (!encontrado)
                    {
                        productosActualizados.Add(ingrediente);
                    }
                }
            }

            return productosActualizados;
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
