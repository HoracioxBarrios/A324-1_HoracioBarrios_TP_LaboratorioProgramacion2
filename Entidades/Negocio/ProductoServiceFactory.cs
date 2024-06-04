using Entidades.Enumerables;
using Entidades;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Excepciones;

namespace Negocio
{
    public static class ProductoServiceFactory 
    {

        public static IProducto CrearProducto(
              ETipoDeProducto tipoProducto, string nombre, double cantidad, EUnidadMedida unidadDeMedida
            , decimal precio, IProveedor proveedor, ECategoriaConsumible categoria = default
            , EClasificacionBebida clasificacionDeBebida = default)
            {
                if(string.IsNullOrEmpty(nombre) || cantidad <= 0 || precio <= 0)
                {
                    throw new DatosDeProductoException("Datos del Producto Exception");
                }
                switch (tipoProducto)
                {
                    case ETipoDeProducto.Bebida:
                        return new Bebida(nombre, cantidad, unidadDeMedida, precio, proveedor, categoria, clasificacionDeBebida);
                    case ETipoDeProducto.Ingrediente:
                        return new Ingrediente(nombre, cantidad, unidadDeMedida, precio, proveedor, ETipoDeProducto.Ingrediente);
                    default:
                        throw new TipoDeProductoDesconocidoException("Tipo de producto no reconocido");                  
            
                }
            }

    }
}
