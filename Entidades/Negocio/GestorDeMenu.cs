using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GestorDeMenu : IGestorMenu
    {
        
        private List<IConsumible> _listaDeConsumiblesDisponiblesGeneral; // Bebidas y Comidas Sin Importar Disponibilidad.
        private List<IConsumible> _listaDeTodosLosPlatosDisponibles;
        private List<IConsumible> _listaDeTodasLasBebidasDisponibles;
        private List<IConsumible> _listaDeTodosLosPlatosNoDisponibles;
        private List<IConsumible> _listaDeTodasLasBebidasNoDisponibles;
        private List<IMenu> _listaDeMenus;//Se va a ofrecer solo si esta disponible
        private ICocinero _cocinero;
        


        private IGestorProductos _gestorproductos;
        private List<IConsumible> _ingredientesSelecionados;

        public GestorDeMenu()
        {
            _listaDeMenus = new List<IMenu>();
            _listaDeConsumiblesDisponiblesGeneral = new List<IConsumible>();
            _listaDeTodosLosPlatosDisponibles = new List<IConsumible>();
            _listaDeTodasLasBebidasDisponibles = new List<IConsumible>();
            _listaDeTodosLosPlatosNoDisponibles = new List<IConsumible>();
            _listaDeTodasLasBebidasNoDisponibles = new List<IConsumible>();

            _ingredientesSelecionados = new List<IConsumible>();
        }
        public GestorDeMenu(ICocinero cocinero) :this()
        {
            _cocinero = cocinero;
        }

        public GestorDeMenu(ICocinero cocinero, IGestorProductos gestorDeproductosStock): this(cocinero)
        {
            _gestorproductos = gestorDeproductosStock;
        }



        /// <summary>
        /// Crea un Menu - Puede ser por ejemplo: DESAYUNO, ALMUERZO , MERIENDA, CENA, ESPECIAL, EMPRESARIAL, etc
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
        /// Edita un Consumible BEBIDA O PLATO del Menu
        /// </summary>
        /// <param name="nombreMenu"></param>
        /// <param name="consumibleEsxistente"></param>
        /// <param name="consumibleQueReemplaza"></param>
        public void EditarMenu(string nombreMenu, IConsumible consumibleEsxistente, IConsumible consumibleQueReemplaza)
        {
            for (int i = 0; i < _listaDeMenus.Count; i++)
            {
                if (_listaDeMenus[i].Nombre == nombreMenu)
                {
                    List<IConsumible> listConsumibles = _listaDeMenus[i].ObtenerAllItemsDelMenu();
                    for(int j  = 0; j < listConsumibles.Count; j++)
                    {
                        if (listConsumibles[j] == consumibleEsxistente)
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



        /// <summary>
        /// En Base a los IProductos o IConsumibles (Ingredientes) que estan disponibles en STOCK, Premite seleccionar los necesarios para el Cocinero pueda crear sus platos
        /// </summary>
        /// <param name="listaDeConsumiblesEnStock"></param>
        /// <param name="nombreDelIngrediente"></param>
        /// <param name="cantidadNecesaria"></param>
        /// <param name="unidadDeMedida"></param>
        public void SeleccionarIngredienteParaElPlato(List<IConsumible> listaDeConsumiblesEnStock, string nombreDelIngrediente, double cantidadNecesaria, EUnidadDeMedida unidadDeMedida)
        {
            IConsumible ingrediente = IngredienteService.ObtenerIngredienteParaPlato(listaDeConsumiblesEnStock, nombreDelIngrediente, cantidadNecesaria, unidadDeMedida);
            if (ingrediente != null)
            {
                _ingredientesSelecionados.Add(ingrediente);
            }
        }



        public void AgregarPlatoAMenu(string nombreDelMenu, string nombrePlato)
        {
            if (_ingredientesSelecionados == null || _ingredientesSelecionados.Count < 2)
            {
                throw new ListaVaciaException("Debe seleccionar al menos 2 ingredientes para crear el plato.");
            }

            IMenu menu = _listaDeMenus.FirstOrDefault(m => m.Nombre == nombreDelMenu);
            if (menu == null)
            {
                throw new MenuNoExisteException("El menú no existe.");
            }


            List<IConsumible> ingredientes = _ingredientesSelecionados; // NO LE PASO LA MISMA LISTA PORQUE DESPUES LA VOY A BORRAA Y ME DA ERROR QUE NUNCA TIENE 2 O MAS INGREDIENTES EN EL TEST
            IConsumible plato = _cocinero.CrearPlato(nombrePlato, new List<IConsumible>(ingredientes));
            ((Menu)menu).Agregar(plato); 
            _listaDeConsumiblesDisponiblesGeneral.Add(plato);            
            _ingredientesSelecionados.Clear();// LimpiaMOS la lista de ingredientes utilizados
        }



        /// <summary>
        /// Agrega una Bebida a un Menu Existente
        /// </summary>
        /// <param name="nombreDelMenu"></param>
        /// <param name="listaDeBebidas"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="MenuNoExisteException"></exception>
        public void AgregarBebidasAMenu(string nombreDelMenu, List<IConsumible> listaDeBebidas)
        {
            IMenu menu = null;
            foreach (IMenu m in _listaDeMenus)
            {
                if (m.Nombre == nombreDelMenu)
                {
                    menu = m;
                    break;
                }
            }
            if (menu == null)
            {
                throw new MenuNoExisteException("El menú no existe.");
            }
            else
            {
                foreach (IConsumible bebida in listaDeBebidas)
                {
                    if(bebida.Disponibilidad == true && bebida is Bebida)
                    {
                        ((Menu)menu).Agregar(bebida);

                        _listaDeConsumiblesDisponiblesGeneral.Add(bebida);
                    }

                }
            }

        }


        public List<IMenu> GetListaDeMenusQueSeOfrecen()
        {
            if(_listaDeMenus.Count > 0)
            {
                return _listaDeMenus;
            }
            throw new ListaVaciaException("La Lista de menú está Vacia");
        }

        public List<IConsumible> GetListaDeTodosLosPlatosDisponibles()
        {
            if (_listaDeConsumiblesDisponiblesGeneral.Count > 0)
            {
                foreach (IConsumible consumible in _listaDeConsumiblesDisponiblesGeneral)
                {
                    if (consumible.Disponibilidad == true && consumible is Plato)
                    {
                        _listaDeTodosLosPlatosDisponibles.Add(consumible);
                    }
                }
            }
            throw new ListaVaciaException("La Lista de todos los Platos está Vacia");
        }


        public List<IConsumible> GetListaDeTodasLasBebidasDisponibles()
        { 
            if(_listaDeConsumiblesDisponiblesGeneral.Count > 0)
            {
                foreach(IConsumible consumible in _listaDeConsumiblesDisponiblesGeneral)
                {
                    if(consumible.Disponibilidad == true && consumible is Bebida)
                    {
                        _listaDeTodasLasBebidasDisponibles.Add(consumible);
                    }
                }
            }
            throw new ListaVaciaException("La lista de bebidas esta Vacia");
        }

        public List<IConsumible> GetListaDeTodosLosPlatosNoDisponibles()
        {
            if (_listaDeConsumiblesDisponiblesGeneral.Count > 0)
            {
                foreach (IConsumible consumible in _listaDeConsumiblesDisponiblesGeneral)
                {
                    if (consumible.Disponibilidad == false && consumible is Plato)
                    {
                        _listaDeTodosLosPlatosNoDisponibles.Add(consumible);
                    }
                }
            }
            throw new ListaVaciaException("La Lista de todos los Platos está Vacia");
        }
        public List<IConsumible> GetListaDeTodasLasBebidasNoDisponibles()
        {
            if (_listaDeConsumiblesDisponiblesGeneral.Count > 0)
            {
                foreach (IConsumible consumible in _listaDeConsumiblesDisponiblesGeneral)
                {
                    if (consumible.Disponibilidad == false && consumible is Bebida)
                    {
                        _listaDeTodasLasBebidasDisponibles.Add(consumible);
                    }
                }
            }
            throw new ListaVaciaException("La lista de bebidas esta Vacia");
        }
    }
}
