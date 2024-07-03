using System;
using System.Collections.Generic;
using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using Entidades.Services;

namespace Negocio
{
    public class GestorDeProductos :IGestorProductos
    {
        private List<IProducto> _listaDeProductosEnStock; // Puede ser una Bebida o Ingredientes
        private decimal _precioTotalStock = 0;
        private int _contadorId = 1;

        public event StockDeProductosActualizoDelegate EventStockDeProductosActualizados;








        public GestorDeProductos() //Puede esperar un servicio a DB.
        {
            _listaDeProductosEnStock = new List<IProducto>();
            CorroborarUltimaIdDeProducto();
        }




        public IProducto CrearProducto(
              ETipoDeProducto tipoProducto, string nombre, double cantidad, EUnidadDeMedida unidadDeMedida,
              decimal precio, IProveedor proveedor, ECategoriaConsumible categoria = default,
              EClasificacionBebida clasificacionDeBebida = default)
        {
            try
            {
                int idParaAsignar = VerificarExistenciaDelProducto(nombre, tipoProducto);
                IProducto producto = ProductoServiceFactory.CrearProducto(tipoProducto, idParaAsignar, nombre, cantidad, unidadDeMedida, precio, proveedor, categoria, clasificacionDeBebida);

                return producto;
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


        public IProducto ObtenerProducto(string nombre)
        {

            if (string.IsNullOrEmpty(nombre))
            {
                throw new DatoIncorrectoException("Dato Incorrecto: Nombre");
            }
            foreach (IProducto producto in _listaDeProductosEnStock)
            {
                if (string.Equals(producto.Nombre, nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return producto;
                }
            }
            return null;
        }

        public IProducto ObtenerProducto(int id)
        {
            if (id < 0)
            {
                throw new DatoIncorrectoException("Dato Incorrecto: ID no valida");
            }
            foreach (Producto producto in ObtenerTodosLosProductos())
            {
                if (producto.Id == id)
                {
                    return producto;
                }
            }
            return null;
        }



        public List<IConsumible> ObtenerTodosLosProductosIngrediente()
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

        public List<IConsumible> OtenerTodosLosProductosBebidas()
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

        public List<IProducto> ObtenerTodosLosProductos()
        {
            if (_listaDeProductosEnStock.Count > 0)
            {
                return _listaDeProductosEnStock;
            }
            throw new ListaVaciaException("La lista de productos en stock esta vacia");
        }


        public void EditarProducto(string nombre, string correccionDelNombre)
        {
            bool seModificoLaLista = false;
            foreach (IProducto producto in _listaDeProductosEnStock)
            {
                if (producto.Nombre == nombre)
                {
                    producto.Nombre = correccionDelNombre;
                    seModificoLaLista = true;
                    break;
                }
            }

            if (seModificoLaLista)
            {
                OnStockProductosActualizado();
            }
        }

        public void EliminarProducto(string nombre)
        {
            bool SeModificoProductoEnLista = false;
            for (int i = _listaDeProductosEnStock.Count - 1; i >= 0; i--) // uso reverse para que no rompa cuando modificamos la lista(iteramos desde el ultimo elemento hacia el primero)  evitamos el Evitar InvalidOperationException de este modo
            {
                if (_listaDeProductosEnStock[i].Nombre == nombre)
                {
                    _listaDeProductosEnStock.RemoveAt(i);
                    SeModificoProductoEnLista = true;
                    break;
                }
            }

            if (SeModificoProductoEnLista)
            {
                OnStockProductosActualizado();
            }
        }










        /// <summary>
        /// Agrega un producto a stock por ejemplo (Puede ser un Ingrediente o una Bebida)
        /// </summary>
        /// <param name="producto"></param>
        /// <exception cref="AlAgregarProductoAStockException"></exception>
        public void AgregarProductoAStock(IProducto producto)
        {
            bool seModificoLaLista = false;
            if(producto == null)
            {
                throw new AlAgregarProductoAStockException("El producto es null, no se pudo agregar a la lista");
            }
            lock (_listaDeProductosEnStock)
            {
                _listaDeProductosEnStock.Add(producto);
                seModificoLaLista = true;
            }


            if (seModificoLaLista)
            {
                OnStockProductosActualizado();
            }
        }


        public bool DescontarProductosDeStock(List<IConsumible> consumiblesADescontarDeStock)//LE PUEDE LLEGAR PLATOS O BEBIDAS
        {
            bool seModificoProductoEnLista = false;

            if (consumiblesADescontarDeStock == null || consumiblesADescontarDeStock.Count == 0)
                return false;

            lock (_listaDeProductosEnStock) // Aplicamos lock para garantizar la consistencia de _listaDeProductosEnStock durante la modificación, en contextos con eventos y múltiples hilos (threads)
            {
                foreach (IConsumible consumible in consumiblesADescontarDeStock)
                {
                    for (int i = 0; i < _listaDeProductosEnStock.Count; i++)
                    {
                        Producto ingredienteEnStock = (Producto)_listaDeProductosEnStock[i];
                        if (consumible is Plato plato)
                        {
                            List<IConsumible> ingredientesDelPlato = plato.ObtenerIngredientes();
                            foreach (Ingrediente ingredienteADescontar in ingredientesDelPlato)
                            {
                                if (ingredienteADescontar.Nombre == _listaDeProductosEnStock[i].Nombre)
                                {
                                    Ingrediente nuevoIngrediente = (Ingrediente)ingredienteEnStock - ingredienteADescontar;
                                    _listaDeProductosEnStock[i] = nuevoIngrediente;
                                    seModificoProductoEnLista = true;
                                    break;
                                }
                            }
                        }
                        else if (consumible is Bebida bebidaADescontar && _listaDeProductosEnStock[i] is Bebida bebidaEnStock && bebidaEnStock.Id == bebidaADescontar.Id)
                        {
                            Bebida nuevaBebida = bebidaEnStock - bebidaADescontar;
                            _listaDeProductosEnStock[i] = nuevaBebida;
                            seModificoProductoEnLista = true;
                            break;
                        }
                    }
                }
            }

            if (seModificoProductoEnLista)
            {
                OnStockProductosActualizado(); // Disparamos evento si se han realizado modificaciones
            }

            return seModificoProductoEnLista;
        }




        public List<IProducto> ConsultaStockVigente(IEncargado encargado)
        {
            return encargado.ConsultaStockVigente(_listaDeProductosEnStock);
        }

        public List<IProducto> ConsultaDeStockPorAgotarse(IEncargado encargado)
        {
            return encargado.ConsultaDeStockPorAgotarse(_listaDeProductosEnStock);
        }


        public void BloquearParaLaVenta(IEncargado encargado, IProducto producto)
        {
            encargado.bloquearParaLaVenta(producto);
        }





        private int VerificarExistenciaDelProducto(string nombre, ETipoDeProducto tipoDeProducto)
        {
            if (_listaDeProductosEnStock.Count > 0)
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
        /// Calcula el precio de todos los productos del Stock
        /// </summary>
        /// <returns></returns>
        public decimal CalcularPrecio()
        {
            if (_listaDeProductosEnStock.Count > 0)
            {
                foreach (IProducto producto in _listaDeProductosEnStock)
                {
                    _precioTotalStock += producto.CalcularPrecioDeCosto();
                }
            }
            return _precioTotalStock;
        }

        /// <summary>
        /// Dispara el evento cuando se actualiza la Lista del stock de productos
        /// </summary>
        protected void OnStockProductosActualizado()
        {
            EventStockDeProductosActualizados?.Invoke();
        }
    }
}
