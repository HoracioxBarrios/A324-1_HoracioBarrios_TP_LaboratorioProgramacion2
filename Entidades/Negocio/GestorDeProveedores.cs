using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using System;


namespace Negocio
{
    public class GestorDeProveedores
    {
        private List<IProveedor> _listProveedores;


        public GestorDeProveedores()
        {
            _listProveedores = new List<IProveedor>();
        }

        /// <summary>
        /// Metodo para crear un proveedor
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="cuit"></param>
        /// <param name="direccion"></param>
        /// <param name="tipoDeProducto"></param>
        /// <param name="mediosDePago"></param>
        /// <param name="esAcreedor"></param>
        /// <param name="diaDeEntrega"></param>
        /// <exception cref="ProveedorErrorAlCrearException"></exception>
        /// <exception cref="Exception"></exception>
        public void CrearProveedor(
                 string nombre, string cuit, string direccion, ETipoDeProducto tipoDeProducto
                ,EMediosDePago mediosDePago,EAcreedor esAcreedor, EDiaDeLaSemana diaDeEntrega)
        {
            try
            {
                IProveedor proveedor = ProveedorService.CrearProveedor(
                nombre, cuit, direccion, tipoDeProducto, mediosDePago, esAcreedor, diaDeEntrega);
                if (proveedor != null)
                {
                    _listProveedores.Add(proveedor);
                }
            }
            catch(ProveedorDatosException ex)
            {
                throw new ProveedorErrorAlCrearException($"Error al Crear el Proveedor: ", ex);
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error inesperado: {ex.Message}", ex);
            }
        }

        public List<IProveedor> GetProveedores()
        {
            return _listProveedores;
        }
    }
}
