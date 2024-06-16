using System;
using System.Collections.Generic;
using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;

namespace Negocio
{
    public class GestorDeProductos :IGestorProductos
    {
        private List<IProducto> _listaDeProductosEnStock;
        private decimal _precioTotalStock = 0;

        private int _contadorId = 1;

    


        public GestorDeProductos() //Puede esperar un servicio a DB.
        {
            _listaDeProductosEnStock = new List<IProducto>();
            CorroborarUltimaIdDeProducto();
        }





        /// <summary>
        /// Verifica la existencia del producto en la lista de productos.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="tipoDeProducto"></param>
        /// <returns>si existe ya el producto devuelve su id,sino al corroborar la ultima id (lo actualiza para tenerla disponible) </returns>
        private int VerificarExistenciaDelProducto(string nombre, ETipoDeProducto tipoDeProducto)
        {
            if(_listaDeProductosEnStock.Count > 0)
            {
                foreach (IProducto producto in _listaDeProductosEnStock)
                {
                    if (producto.Nombre == nombre && producto.ETipoDeProducto == tipoDeProducto)
                    {
                        return producto.Id;
                    }
                }
            }

            CorroborarUltimaIdDeProducto();
            return _contadorId;
        }



        /// <summary>
        /// Corrobora la ultima Id de los productos en la lista, actualiza la: _id (dejando la siguiente id disponible)
        /// <return>Void</return>
        /// </summary>
        private void CorroborarUltimaIdDeProducto()
        {
            if (_listaDeProductosEnStock.Count > 0)
            {
                int maxId = 0;
                foreach (var producto in _listaDeProductosEnStock)
                {
                    if (producto.Id > maxId)
                    {
                        maxId = producto.Id;
                    }
                }
                _contadorId = maxId + 1;
            }
        }

        /// <summary>
        /// Crea un producto Agregandolo a la Lista de stock.
        /// <para>
        /// Para Crear un Ingrediente: Recibe (ETipoDeProducto tipoDeProducto, string  nombreDeProducto, double cantidad, EUnidadDeMedida unidadDeMedida, decimal  precio, IProveedor proveedor).    
        /// </para>
        /// <para>
        /// ParaCrear Una Bebida: Recibe (ETipoDeProducto tipoDeProducto, string  nombreDeProducto, double cantidad, EUnidadDeMedida unidadDeMedida, decimal precio, IPproveedor proveedor, ECategoriaDeConsumible categoriaDeConsumible [default], EClasificacionBebida clasificacionDeBebida)[default];
        /// </para>
        /// </summary>
        /// <param name="tipoProducto">ETipoDeProducto : tipo de producto: ejemplo Verduleria, Carniceria,Almacen, Bebida, Ingrediente </param>
        /// <param name="nombre"> String : Tipo de Producto</param>
        /// <param name="cantidad">Cantidad total</param>
        /// <param name="unidadDeMedida">EUnidad de medida : Kilo, Gramo, Litro, MiliLitro, Unidad </param>
        /// <param name="precio">Precio Total (detalle: Dentro de cada Producto se Calcula el precio Unitario en base a la cantidad tambien)</param>
        /// <param name="proveedor">IProveedor : proveedor</param>
        /// <param name="categoria">ECategoriaDeConsumible : Comidad , Bebida . ( este Parametro es Obcional solo para BEBIDA)</param>
        /// <param name="clasificacionDeBebida">EClasificacion Bebida : Con_Alcohol, Sin_Alcohol.  ( este Parametro es Obcional solo para BEBIDA)</param>
        /// 
        /// 
        ///
        /// 
        /// <exception cref="AlCrearProductoException"></exception>
        /// <exception cref="Exception"></exception>
        public void CrearProductoParaListaDeStock(
              ETipoDeProducto tipoProducto, string nombre, double cantidad, EUnidadDeMedida unidadDeMedida
            , decimal precio, IProveedor proveedor, ECategoriaConsumible categoria = default
            , EClasificacionBebida clasificacionDeBebida = default)
        {
            try
            {
                int idParaAsignar = VerificarExistenciaDelProducto(nombre, tipoProducto);
                IProducto producto = ProductoServiceFactory.CrearProducto(tipoProducto, idParaAsignar, nombre, cantidad, unidadDeMedida, precio, proveedor, categoria, clasificacionDeBebida);
                _listaDeProductosEnStock.Add(producto);

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

   
        public IConsumible CrearProducto(
      ETipoDeProducto tipoProducto, string nombre, double cantidad, EUnidadDeMedida unidadDeMedida,
      decimal precio, IProveedor proveedor, ECategoriaConsumible categoria = default,
      EClasificacionBebida clasificacionDeBebida = default)
        {
            try
            {
                int idParaAsignar = VerificarExistenciaDelProducto(nombre, tipoProducto);
                IProducto producto = ProductoServiceFactory.CrearProducto(tipoProducto, idParaAsignar, nombre, cantidad, unidadDeMedida, precio, proveedor, categoria, clasificacionDeBebida);
                return (IConsumible)producto;
            }
            catch (DatosDeProductoException ex)
            {
                throw new AlCrearProductoException("Error al crear el Producto ", ex);
            }
            catch (TipoDeProductoDesconocidoException ex)
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
            if (_listaDeProductosEnStock.Count > 0)
            {
                foreach (IProducto producto in _listaDeProductosEnStock)
                {
                    _precioTotalStock += producto.CalcularPrecio();
                }
            }
            return _precioTotalStock;
        }


        public bool DescontarProductosDeStock(List<IConsumible> listaDeIngredienteEnElPlato)
        {
            List<IProducto> listaDeProductosEnStock = GetAllProductos();
            bool seActualizo = false;

            foreach (IProducto productoADescontar in listaDeIngredienteEnElPlato)
            {
                if (productoADescontar is Ingrediente ingredienteADescontar)
                {
                    for (int i = 0; i < listaDeProductosEnStock.Count; i++)
                    {
                        if (listaDeProductosEnStock[i] is Ingrediente ingredienteEnStock && ingredienteEnStock.Id == ingredienteADescontar.Id)
                        {
                            Ingrediente nuevoIngrediente = ingredienteEnStock - ingredienteADescontar;
                            listaDeProductosEnStock[i] = nuevoIngrediente;//modificamos por referencia
                            seActualizo = true;
                            break;
                        }
                    }
                }
            }

            return seActualizo;
        }

        private void ActualizarListaOriginalDeproductos(List<IProducto> listaDeProductosIngredientesStock, List<IProducto> productosActualizados)
        {
            for (int i = 0; i < listaDeProductosIngredientesStock.Count; i++)
            {
                listaDeProductosIngredientesStock[i] = productosActualizados[i];
            }
        }


        /// <summary>
        /// GetAllProductosIngredientes (  De una Lista Original de IProductos, separa los ingredientes y lo retorna en una lista de IConsumibles)
        /// </summary>
        /// <returns>Devuelve una lista de IConsumibles (Ingredientes) en base a una lista original de IProductos</returns>
        public List<IConsumible> GetAllProductosIngrediente()
        {
            List<IConsumible> nuevaListDeIngrediente = new List<IConsumible>();

            foreach (IProducto producto in _listaDeProductosEnStock)
            {
                Producto producto1 = producto as Producto;
                if (producto1.ETipoDeProducto == ETipoDeProducto.Ingrediente)
                {
                    nuevaListDeIngrediente.Add(producto1 as IConsumible);

                }
            }
            return nuevaListDeIngrediente;
        }

        /// <summary>
        /// GetAllProductosBebidas (  De una Lista Original de IProductos, separa las bebidas y lo retorna en una lista de IConsumibles)
        /// </summary>
        /// <returns>Devuelve una lista de IConsumibles (bebidas) en base a una lista original de IProductos</returns>
        public List<IConsumible> GetAllProductosBebidas()
        {
            List<IConsumible> nuevaListDeBebidas = new List<IConsumible>();

            foreach (IProducto producto in _listaDeProductosEnStock)
            {
                Producto producto1 = producto as Producto;
                if (producto1.ETipoDeProducto == ETipoDeProducto.Bebida)
                {
                    nuevaListDeBebidas.Add(producto1 as IConsumible);

                }
            }
            return nuevaListDeBebidas;
        }




        public List<IProducto> GetAllProductos()
        {
            List<IProducto> lista = new List<IProducto>();
            if(_listaDeProductosEnStock.Count > 0)
            {
                return _listaDeProductosEnStock;
            }
            return lista;
        }
        

        public IProducto GetProducto(string nombre)
        {

            if (string.IsNullOrEmpty(nombre))
            {
                throw new DatoIncorrectoException("Dato Incorrecto: Nombre");
            }
            foreach(IProducto producto in _listaDeProductosEnStock)
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
            foreach(Producto producto in GetAllProductos()) 
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
