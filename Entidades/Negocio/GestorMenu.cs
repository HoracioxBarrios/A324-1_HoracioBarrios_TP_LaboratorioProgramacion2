using Entidades;
using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GestorMenu : IGestorMenu
    {
        private List<IMenu> _listaDeMenus;//Se va a ofrecer solo si esta disponible
        private List<IConsumible> _listaDeConsumiblesDisponiblesGeneral; // Bebidas y Comidas Sin Importar Disponibilidad.
        private List<IConsumible> _listaDeTodosLosPlatosDisponibles;
        private List<IConsumible> _listaDeTodasLasBebidasDisponibles;
        private List<IConsumible> _listaDeTodosLosPlatosNoDisponibles;
        private List<IConsumible> _listaDeTodasLasBebidasNoDisponibles;

        private ICocinero _cocinero;
        private IGestorProductos _gestorproductos;



        public GestorMenu(ICocinero cocinero)
        {
            
            _listaDeMenus = new List<IMenu>();
            _listaDeConsumiblesDisponiblesGeneral = new List<IConsumible>();
            _listaDeTodosLosPlatosDisponibles = new List<IConsumible>();
            _listaDeTodasLasBebidasDisponibles = new List<IConsumible>();
            _listaDeTodosLosPlatosNoDisponibles = new List<IConsumible>();
            _listaDeTodasLasBebidasNoDisponibles = new List<IConsumible>();
            _cocinero = cocinero;
    

        }
        public GestorMenu(ICocinero cocinero, IGestorProductos gestorDeproductosStock): this(cocinero)
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

        public void Eliminarmenu(string nombreMenu)
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
        /// Agrega un Plato a un Menu existente
        /// </summary>
        /// <param name="nombreDelMenu"></param>
        /// <param name="nombrePlato"></param>
        /// <param name="listaDeIngredientes"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="MenuNoExisteException"></exception>
        public void AgregarPlatoAMenu(string nombreDelMenu, string nombrePlato, List<IProducto> listaDeIngredientes)
        {
            // Convertir cada IProducto en IConsumible
            List<IConsumible> listaDeIngredientesConsumibles = new List<IConsumible>();
            foreach (var producto in listaDeIngredientes)
            {
                if (producto is IConsumible consumible)
                {
                    listaDeIngredientesConsumibles.Add(consumible);
                }
                else
                {
                    throw new InvalidCastException("Un producto en la lista no puede ser convertido a IConsumible.");
                }
            }

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

            IConsumible plato = _cocinero.CrearPlato(nombrePlato, listaDeIngredientesConsumibles);
            ((Menu)menu).Agregar(plato);
            _listaDeConsumiblesDisponiblesGeneral.Add(plato);
        }


        /// <summary>
        /// Agrega una Bebida a un Menu Existente
        /// </summary>
        /// <param name="nombreDelMenu"></param>
        /// <param name="listaDeBebidas"></param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="MenuNoExisteException"></exception>
        public void AgregarBebidasAMenu(string nombreDelMenu, List<IProducto> listaDeBebidas)
        {
            // Convertir cada IProducto en IConsumible
            List<IConsumible> listaDeBebidasConsumibles = new List<IConsumible>();
            foreach (var producto in listaDeBebidas)
            {
                if (producto is IConsumible consumible)
                {
                    listaDeBebidasConsumibles.Add(consumible);
                }
                else
                {
                    throw new InvalidCastException("Un producto en la lista no puede ser convertido a IConsumible.");
                }
            }

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
                foreach (IConsumible bebida in listaDeBebidasConsumibles)
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
