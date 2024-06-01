using Entidades.Enumerables;
using Entidades;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoFactoryService 
    {
        public static IProducto CrearProducto(
              ETipoProductoCreable tipoProducto, string nombre, double cantidad, EUnidadMedida unidadDeMedida
            , decimal precio, IProveedor proveedor, ECategoriaConsumible categoria = default
            , EClasificacionBebida clasificacionDeBebida = default, bool disponibilidad = true)
        {
            switch (tipoProducto)
            {
                case ETipoProductoCreable.Bebida:
                    return new Bebida(nombre, cantidad, unidadDeMedida, precio, proveedor, categoria, clasificacionDeBebida);
                case ETipoProductoCreable.Ingrediente:
                    return new Ingrediente(nombre, cantidad, unidadDeMedida, precio, proveedor, ETipoProductoCreable.Ingrediente, disponibilidad);
                default:
                    throw new ArgumentException("Tipo de producto no reconocido");                  
            
            }
        }

    }
}
