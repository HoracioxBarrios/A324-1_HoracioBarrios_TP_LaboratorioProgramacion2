using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    /// <summary>
    /// Clase Gestor Menú.
    /// </summary>
    /// <remarks>
    /// Espera un ICocinero para la creación, edición y eliminación de platos. //
    /// Espera el Gestor de Productos (que tiene el stock de productos dentro).
    /// </remarks>
    public class GestorDeMenu : IGestorMenu
    {
        private List<IMenu> _listaDeMenus;//Se va a ofrecer solo si esta disponible
        private List<IConsumible> _ListaGeneralDeConsumiblesLocal; // Bebidas y Comidas Sin Importar Disponibilidad.
        private ICocinero _cocinero;
        private IGestorProductos _gestorProductosStock;



        private GestorDeMenu()
        {
            _listaDeMenus = new List<IMenu>();
            _ListaGeneralDeConsumiblesLocal = new List<IConsumible>();

        }
        private GestorDeMenu(ICocinero cocinero) :this()
        {
            _cocinero = cocinero;
        }

        public GestorDeMenu(ICocinero cocinero, IGestorProductos gestorDeproductosStock): this(cocinero)
        {
            _gestorProductosStock = gestorDeproductosStock;

            //Sucribimos al evento para Actualizar  Iconsumibles como Bebidas o Ingredientes
            _gestorProductosStock.EventStockDeProductosActualizados += ActualizarConsumiblesDesdeStock;

        }




        public void CrearMenu(string nombreMenu)
        {
            IMenu menu = new Menu(nombreMenu);
            _listaDeMenus.Add(menu);
        }

        public IMenu ObtenerMenuPorNombre(string nombreDelMenu)
        {
            IMenu menu = _listaDeMenus.FirstOrDefault(m => m.Nombre == nombreDelMenu);// verifica si existe si es true lo retorna
            if (menu == null)
            {
                throw new MenuNoExisteException("El menú no existe.");
            }
            return menu;
        }

        public List<IMenu> ObtenerTodosLosMenus()
        {
            if (_listaDeMenus.Count > 0)
            {
                return _listaDeMenus;
            }
            throw new ListaVaciaException("La Lista de menú está Vacia");
        }

        public void EditarMenu(string nombreMenu, string nuevoNombre)
        {
            for(int i = 0; i < _listaDeMenus.Count; i++)
            {
                if (_listaDeMenus[i].Nombre == nombreMenu)
                {
                    _listaDeMenus[i].Nombre = nuevoNombre;
                }
            }
        }

        public void EditarMenu(string nombreMenu, IConsumible consumibleExistente, IConsumible consumibleQueReemplaza)
        {
            for (int i = 0; i < _listaDeMenus.Count; i++)
            {
                if (_listaDeMenus[i].Nombre == nombreMenu)
                {
                    List<IConsumible> listConsumibles = _listaDeMenus[i].ObtenerTodosLosConsumiblesEnMenu();
                    for(int j  = 0; j < listConsumibles.Count; j++)
                    {
                        if (listConsumibles[j] == consumibleExistente)
                        {
                            listConsumibles[j] = consumibleQueReemplaza;
                        }
                    }
                }
            }
        }

        public void EliminarMenu(string nombreMenu)
        {
            for(int i = 0; i < _listaDeMenus.Count; i++)
            {
                if (_listaDeMenus[i].Nombre == nombreMenu)
                {
                    _listaDeMenus.Remove(_listaDeMenus[i]);
                }
            }
        }






        public IConsumible CrearPlato(string nombreDelPlato, int tiempoDePreparacion, EUnidadDeTiempo unidadDeTiempo)
        {
            List<IConsumible> ingredientesSeleccionados = _cocinero.ObtenerListaDeIngredientesSeleccionadosParaPlato();
            if (ingredientesSeleccionados == null || ingredientesSeleccionados.Count == 0)
            {
                throw new ListaVaciaException("La lista de ingredientes seleccionados por el cocinero está vacía.");
            }

            IConsumible plato = _cocinero.CrearPlato(nombreDelPlato, ingredientesSeleccionados, tiempoDePreparacion, unidadDeTiempo);


            bool disponibilidadPlato = ((Plato)plato).VerificarStockIngredientes(_gestorProductosStock.ObtenerTodosLosProductosIngrediente());//Verificamos si la disponibilidad
            ((Plato)plato).Disponibilidad = disponibilidadPlato;

            if (plato == null)
            {
                throw new AlCrearPlatoException("El plato no se creó correctamente.");
            }

            _ListaGeneralDeConsumiblesLocal.Add(plato);
            return plato;
        }

        public IConsumible ObtenerConsumibleBebidaOPlato(string nombreConsumible)
        {

            IConsumible consumibleEncontrado = _ListaGeneralDeConsumiblesLocal.FirstOrDefault(consumible => consumible.Nombre == nombreConsumible);
            if (consumibleEncontrado == null)
            {
                throw new ConsumibleNoExisteEnListaDeMenuException("El plato o la bebida no existen en la lista.");
            }

            return consumibleEncontrado;
        }


        public void EditarPlato(string nombrePlato, List<IConsumible> ingredientesActualizacion)
        {
            for (int i = 0; i < _ListaGeneralDeConsumiblesLocal.Count; i++)
            {
                IConsumible consumible = _ListaGeneralDeConsumiblesLocal[i];
                if (consumible is Plato && consumible.Nombre == nombrePlato)
                {
                    _ListaGeneralDeConsumiblesLocal[i] = _cocinero.EditarPlato(consumible, ingredientesActualizacion);
                    break;
                }
            }
        }


        public void EliminarPlato(string nombrePlato)
        {
            for (int i = _ListaGeneralDeConsumiblesLocal.Count - 1; i >= 0; i--)
            {
                IConsumible consumible = _ListaGeneralDeConsumiblesLocal[i];
                if (consumible is Plato && consumible.Nombre == nombrePlato)
                {
                    _ListaGeneralDeConsumiblesLocal.RemoveAt(i);
                    break;
                }
            }
        }








        /// <summary>
        /// Crea el plato y lo agrega al menús / lista general de consumibles.
        /// </summary>
        /// <param name="nombreDelMenu"></param>
        /// <param name="nombrePlato"></param>
        /// <exception cref="ListaVaciaException"></exception>
        public void AgregarPlatoAMenu(string nombreDelMenu, IConsumible plato)
        {
            if (string.IsNullOrEmpty(nombreDelMenu))
            {
                throw new NombreDelMenuException("El nombre del menu no es valido");
            }
            if(plato == null)
            {
                throw new ElPlatoNoExisteException("Error al agregar Plato al Menú El plato no existe");
            }


            IMenu menu = ObtenerMenuPorNombre(nombreDelMenu);
            if(menu == null)
            {
                throw new MenuNoExisteException("El Menu no existe en la Lista de menus");
                
            }
            menu.Agregar(plato);

        }


        public void AgregarPlatosAlMenu(string nombreDelMenu, List<IConsumible> listaDePlatos)
        {
            if (string.IsNullOrEmpty(nombreDelMenu))
            {
                throw new NombreDelMenuException("El nombre del menu no es valido");
            }
            if (listaDePlatos == null || listaDePlatos.Count == 0)
            {
                throw new ListaDePlatosVaciaException("Error, La lista de platos esta Vacia");
            }
            
            IMenu menu = ObtenerMenuPorNombre(nombreDelMenu);
            foreach (Plato plato in listaDePlatos)
            {
                menu.Agregar(plato);
            }
        }


        //OK
        public void AgregarBebidaAlMenu(string nombreDelMenu, IConsumible consumible)
        {
            if (string.IsNullOrEmpty(nombreDelMenu))
            {
                throw new NombreDelMenuException("El nombre del menu no es valido");
            }
            if(consumible == null)
            {
                throw new ConsumibleEsNullException("El consumible es NUll");
            }

            if (!(consumible is Bebida bebida && bebida.Disponibilidad))
            {
                throw new ConsumibleInvalidoException("El consumible No es una bebida o No está disponible.");

            }
            IMenu menu = ObtenerMenuPorNombre(nombreDelMenu);
            menu.Agregar(consumible);
            _ListaGeneralDeConsumiblesLocal.Add(consumible);
        }



        //ok
        public void AgregarBebidasAMenu(string nombreDelMenu, List<IConsumible> listaDeBebidas)
        {
            if (string.IsNullOrEmpty(nombreDelMenu))
            {
                throw new NombreDelMenuException("El nombre del menú no es válido");
            }

            if (listaDeBebidas == null || listaDeBebidas.Count == 0)
            {
                throw new ListaVaciaException("La lista de bebidas está vacía");
            }


            IMenu menu = ObtenerMenuPorNombre(nombreDelMenu);
            foreach (IConsumible consumible in listaDeBebidas)
            {
                if (consumible is Bebida bebida && bebida.Disponibilidad)
                {
                    menu.Agregar(consumible);
                    _ListaGeneralDeConsumiblesLocal.Add(consumible);
                }
            }
        }








        public List<IConsumible> ObtenerPlatosDisponibles()
        {
            List<IConsumible> platosDisponibles = new List<IConsumible>();

            foreach (IConsumible consumible in _ListaGeneralDeConsumiblesLocal)
            {
                // Verificar si el consumible es un Plato y está disponible para crear
                if (consumible is Plato plato && plato.Disponibilidad)
                {
                    try
                    {
                        // Verificar si el plato tiene ingredientes suficientes en stock
                        List<IConsumible> ingredientesEnStock = _gestorProductosStock.ObtenerTodosLosProductosIngrediente();
                        if (plato.VerificarStockIngredientes(ingredientesEnStock))
                        {
                            platosDisponibles.Add(plato);
                        }
                    }
                    catch (ListaVaciaException ex)
                    {
                        // Manejar la excepción de lista vacía de ingredientes en stock
                        Console.WriteLine($"Advertencia: {plato.Nombre} no está disponible debido a la falta de ingredientes en stock.");
                    }
                    catch (Exception ex)
                    {
                        // Manejar otras excepciones
                        Console.WriteLine($"Error al verificar disponibilidad de {plato.Nombre}: {ex.Message}");
                    }
                }
            }

            if (platosDisponibles.Count == 0)
            {
                throw new ListaVaciaException("No hay platos disponibles para crear debido a la escasez de ingredientes en stock.");
            }

            return platosDisponibles;
        }



        public List<IConsumible> ObtenerBebidasDisponibles()
        {
            List<IConsumible> bebidasDisponibles = new List<IConsumible>();
            if (_ListaGeneralDeConsumiblesLocal.Count > 0)
            {
                foreach(IConsumible consumible in _ListaGeneralDeConsumiblesLocal)
                {
                    if(consumible.Disponibilidad == true && consumible is Bebida)
                    {
                        bebidasDisponibles.Add(consumible);
                    }
                }
                return bebidasDisponibles;
            }
            throw new ListaVaciaException("La lista de bebidas esta Vacia");
        }

        public List<IConsumible> ObtenerPlatosNoDisponibles()
        {
            List<IConsumible> platosNoDisponibles = new List<IConsumible>();

            foreach (IConsumible consumible in _ListaGeneralDeConsumiblesLocal)
            {
                // Verificar si el consumible es un Plato y no está disponible por falta de ingredientes
                if (consumible is Plato plato && !plato.Disponibilidad)
                {
                    platosNoDisponibles.Add(plato);
                }
            }

            if (platosNoDisponibles.Count == 0)
            {
                throw new ListaVaciaException("No hay platos no disponibles debido a la insuficiencia de ingredientes.");
            }

            return platosNoDisponibles;
        }

        public List<IConsumible> ObtenerBebidasNoDisponibles()
        {
            List<IConsumible> bebidasNoDisponibles = new List<IConsumible>();
            if (_ListaGeneralDeConsumiblesLocal.Count > 0)
            {
                foreach (IConsumible consumible in _ListaGeneralDeConsumiblesLocal)
                {
                    if (consumible.Disponibilidad == false && consumible is Bebida)
                    {
                        bebidasNoDisponibles.Add(consumible);
                    }
                }
                return bebidasNoDisponibles;
            }
            throw new ListaVaciaException("La lista de bebidas esta Vacia");
        }



        public List<Plato> OrdenarPlatosPorIngrediente(string nombreIngrediente)
        {
            List<Plato> platos = _ListaGeneralDeConsumiblesLocal.OfType<Plato>().ToList();

            platos.Sort((plato1, plato2) =>
            {
                Ingrediente ingrediente1 = plato1.ObtenerIngrediente(nombreIngrediente);
                Ingrediente ingrediente2 = plato2.ObtenerIngrediente(nombreIngrediente);

                // Manejar casos donde alguno de los ingredientes es null
                if (ingrediente1 == null && ingrediente2 == null) return 0;
                if (ingrediente1 == null) return -1;
                if (ingrediente2 == null) return 1;

                // Comparar de mayor a menor cantidad de ingrediente
                if (ingrediente1 > ingrediente2) return -1;
                if (ingrediente1 < ingrediente2) return 1;
                return 0;
            });

            return platos;
        }

















        /// <summary>
        /// Establece el precio de Venta del producto
        /// </summary>
        /// En este Lugar (Gestor Menu estableceremos el precio a los productos)
        /// <param name="establecedorDePrecios"></param>
        /// <param name="nombreDelProducto"></param>
        /// <param name="precioDeVentaDelProducto"></param>
        public void EstablecerPrecioAProducto(IEstablecedorDePrecios establecedorDePrecios, string nombreDelProducto, decimal precioDeVentaDelProducto)
        {
            foreach (IConsumible producto in _ListaGeneralDeConsumiblesLocal)
            {
                if (producto.Nombre == nombreDelProducto)
                {
                    establecedorDePrecios.EstablecerPrecioAProducto(producto, precioDeVentaDelProducto);
                    break;
                }
            }

        }


        public void SelecionarIngredienteParaUnPlato(string nombreDelIngrediente, double cantidadNecesaria, EUnidadDeMedida unidadDeMedida)
        {
            List<IConsumible> ingredientesEnStock = _gestorProductosStock.ObtenerTodosLosProductosIngrediente();
            if (ingredientesEnStock == null || ingredientesEnStock.Count == 0)
            {
                throw new ListaVaciaException("La lista de Ingredientes en Stock esta Vacia");
            }
            _cocinero.SeleccionarIngredienteParaElPlato(ingredientesEnStock, nombreDelIngrediente, cantidadNecesaria, unidadDeMedida);
        }

        private void ActualizarConsumibleEnListaLocal()
        {
            Thread ActualizarConsumilesThread = new Thread(() =>
            {
                lock (_ListaGeneralDeConsumiblesLocal)
                {
                    List<IProducto> productos = _gestorProductosStock.ObtenerTodosLosProductos();
                    HashSet<string> nombreDeBebidasEnlaListaLocal = new HashSet<string>(_ListaGeneralDeConsumiblesLocal.OfType<Bebida>().Select(b => b.Nombre));

                    foreach (IProducto producto in productos)
                    {
                        if (!(nombreDeBebidasEnlaListaLocal.Contains(producto.Nombre)))
                        {
                            _ListaGeneralDeConsumiblesLocal.Add((IConsumible)producto);
                            nombreDeBebidasEnlaListaLocal.Add(producto.Nombre);
                        }
                    }
                }
            });

            ActualizarConsumilesThread.Start();// inicio del Hilo
        }


        private void ActualizarConsumiblesDesdeStock()
        {
            // Actualizar consumibles en la lista local de bebidas
            List<IConsumible> bebidasEnStock = _gestorProductosStock.OtenerTodosLosProductosBebidas();
            lock (_ListaGeneralDeConsumiblesLocal)
            {
                foreach (Bebida bebida in bebidasEnStock)
                {
                    IConsumible bebidaExistente = _ListaGeneralDeConsumiblesLocal.OfType<Bebida>().FirstOrDefault(b => b.Id == bebida.Id);
                    if (bebidaExistente != null)
                    {
                        bebidaExistente.Cantidad = bebida.Cantidad;
                    }
                }
            }
        }

    }
}
