using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;

namespace Negocio
{
    /// <summary>
    /// Class Proveedor Service - Provee Servicio de Manejo de Proveedores
    /// </summary>
    public static class ProveedorService
    {
      
        /// <summary>
        /// Methodo que Crea un Proveedor
        /// </summary>
        /// <param name="nombre">Nombre</param>
        /// <param name="cuit">Cuit</param>
        /// <param name="direccion">Direccion</param>
        /// <param name="tipoDeproducto"> -Enum- Tipo de Producto</param>
        /// <param name="medioDePago">-Enum- Medio de Pago </param>
        /// <param name="diaDeEntrega">-Enum -Dia de Entrega</param>
        /// <returns>Devuelve un IProveedor si los datos son validos, de no serlo Lanza una excepcion</returns>
        /// <exception cref="ProveedorDatosException"></exception>
        public static IProveedor CrearProveedor(string nombre, string cuit, string direccion, ETipoDeProducto tipoDeproducto, EMediosDePago medioDePago, EAcreedor esAcreedor, EDiaDeLaSemana diaDeEntrega)
        {

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(cuit) || string.IsNullOrEmpty(direccion))
            {
                throw new ProveedorDatosException("Los Datos Del Proveedor no son Validos");
            }
            else
            {
                return new Proveedor(nombre, cuit, direccion, tipoDeproducto, medioDePago, esAcreedor, diaDeEntrega);
            }
        }


    }
}
