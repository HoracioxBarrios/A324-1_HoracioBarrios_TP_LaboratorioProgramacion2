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


        }



        /// <summary>
        /// Crea un Menu y agrega a una lista interna en el gestor menu- Puede ser por ejemplo: DESAYUNO, ALMUERZO , MERIENDA, CENA, ESPECIAL, EMPRESARIAL, etc
        /// </summary>
        /// <param name="nombreMenu"></param>
        public void CrearMenu(string nombreMenu)
        {
            IMenu menu = new Menu(nombreMenu);
            _listaDeMenus.Add(menu);
        }


        /// <summary>
        /// Edita el nombre del Menu
        /// </summary>
        /// <param name="nombreMenu"></param>
        /// <param name="nuevoNombre"></param>
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


        /// <summary>
        /// Edita Reemplazando un Consumible BEBIDA O PLATO del Menu por otro.
        /// </summary>
        /// <param name="nombreMenu"></param>
        /// <param name="consumibleExistente"></param>
        /// <param name="consumibleQueReemplaza"></param>
        public void EditarMenu(string nombreMenu, IConsumible consumibleExistente, IConsumible consumibleQueReemplaza)
        {
            for (int i = 0; i < _listaDeMenus.Count; i++)
            {
                if (_listaDeMenus[i].Nombre == nombreMenu)
                {
                    List<IConsumible> listConsumibles = _listaDeMenus[i].ObtenerAllItemsDelMenu();
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





        public void SelecionarIngredienteParaUnPlato(string nombreDelIngrediente, double cantidadNecesaria, EUnidadDeMedida unidadDeMedida )
        {
            List<IConsumible> ingredientesEnStock = _gestorProductosStock.ReadAllProductosIngredientes();
            if (ingredientesEnStock == null || ingredientesEnStock.Count == 0)
            {
                throw new ListaVaciaException("La lista de Ingredientes en Stock esta Vacia");
            }
            _cocinero.SeleccionarIngredienteParaElPlato(ingredientesEnStock, nombreDelIngrediente, cantidadNecesaria, unidadDeMedida);
        }

        public IConsumible CrearPlato(string nombreDelPlato, int tiempoDePreparacion, EUnidadDeTiempo unidadDeTiempo)
        {
            List<IConsumible> ingredientesSeleccionados = _cocinero.GetListaDeIngredientesSeleccionados();
            if(ingredientesSeleccionados == null || ingredientesSeleccionados.Count == 0)
            {
                throw new ListaVaciaException("Error la Lista de ingredientes seleccioandos por el Cocinero esta Vacia");
            }
            IConsumible plato = _cocinero.CrearPlato(nombreDelPlato, ingredientesSeleccionados, tiempoDePreparacion, unidadDeTiempo);
            if (plato == null) 
            {
                throw new AlCrearPlatoException("El Plato no se Creó, es NULL");
            }
            _ListaGeneralDeConsumiblesLocal.Add(plato);
            return plato;
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


            IMenu menu = GetMenuPorNombre(nombreDelMenu);
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
            
            IMenu menu = GetMenuPorNombre(nombreDelMenu);
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
            IMenu menu = GetMenuPorNombre(nombreDelMenu);
            menu.Agregar(consumible);
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


            IMenu menu = GetMenuPorNombre(nombreDelMenu);
            foreach (IConsumible consumible in listaDeBebidas)
            {
                if (consumible is Bebida bebida && bebida.Disponibilidad)
                {
                    menu.Agregar(consumible);
                }
            }
        }



        public List<IMenu> GetAllMenus()
        {
            if(_listaDeMenus.Count > 0)
            {
                return _listaDeMenus;
            }
            throw new ListaVaciaException("La Lista de menú está Vacia");
        }

        public IMenu GetMenuPorNombre(string nombreDelMenu)
        {
            IMenu menu = _listaDeMenus.FirstOrDefault(m => m.Nombre == nombreDelMenu);// verifica si existe si es true lo retorna
            if (menu == null)
            {
                throw new MenuNoExisteException("El menú no existe.");
            }
            return menu;
        }




        public List<IConsumible> GetPlatosDisponibles()
        {
            List<IConsumible> platosDisponibles = new List<IConsumible>();
            if (_ListaGeneralDeConsumiblesLocal.Count > 0)
            {
                foreach (IConsumible consumible in _ListaGeneralDeConsumiblesLocal)
                {
                    if (consumible.Disponibilidad == true && consumible is Plato)
                    {
                        platosDisponibles.Add(consumible);
                    }
                }
                return platosDisponibles;
            }
            throw new ListaVaciaException("La Lista de todos los Platos está Vacia");
        }


        public List<IConsumible> GetBebidasDisponibles()
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

        public List<IConsumible> GetPlatosNoDisponibles()
        {
            List<IConsumible> platosNoDisponibles = new List<IConsumible>();
            if (_ListaGeneralDeConsumiblesLocal.Count > 0)
            {
                foreach (IConsumible consumible in _ListaGeneralDeConsumiblesLocal)
                {
                    if (consumible.Disponibilidad == false && consumible is Plato)
                    {
                           platosNoDisponibles.Add(consumible);
                    }
                }
                return platosNoDisponibles;
            }
            throw new ListaVaciaException("La Lista de todos los Platos está Vacia");
        }
        public List<IConsumible> GetBebidasNoDisponibles()
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

        private void ActualizarConsumibleEnListaLocal()
        {
            Thread ActualizarConsumilesThread = new Thread(() =>
            {
                lock (_ListaGeneralDeConsumiblesLocal)
                {
                    List<IProducto> productos = _gestorProductosStock.ReadAllProductos();
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
        
    }
}
