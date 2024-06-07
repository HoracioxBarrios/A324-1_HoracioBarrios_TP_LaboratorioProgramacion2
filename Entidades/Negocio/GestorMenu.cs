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
        private List<IMenu> _listaDeMenus;
        private List<IConsumible> _listaDeTodosLosPlatos;
        private List<IConsumible> _listaDeTodasLasBebidas;
        private List<IConsumible> _listaDePlatosDisponibles;
        private List<IConsumible> _listaDePlatosNoDisponibles;
        private ICocinero _cocinero;
        public GestorMenu(ICocinero cocinero)
        {
            _cocinero = cocinero;
            _listaDeMenus = new List<IMenu>();

            _listaDeTodosLosPlatos = new List<IConsumible>();
            _listaDePlatosDisponibles = new List<IConsumible>();
            _listaDePlatosNoDisponibles = new List<IConsumible>();
        }

        public void CrearMenu(string nombreMenu)
        {
            Menu menu = new Menu(nombreMenu);
            _listaDeMenus.Add(menu);
        }

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
            _listaDeTodosLosPlatos.Add(plato);
        }

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

            foreach (IConsumible bebida in listaDeBebidasConsumibles)
            {
                ((Menu)menu).Agregar(bebida);
                _listaDeTodasLasBebidas.Add(bebida);
            }
        }


        public List<IMenu> GetListaDeAllMenus()
        {
            if(_listaDeMenus.Count > 0)
            {
                return _listaDeMenus;
            }
            throw new ListaVaciaException("La Lista de menú está Vacia");
        }

        public List<IConsumible> GetListaDeAllPlatos()
        {
            if (_listaDeTodosLosPlatos.Count > 0)
            {
                return _listaDeTodosLosPlatos;
            }
            throw new ListaVaciaException("La Lista de todos los Platos está Vacia");
        }

        public List<IConsumible> GetListaDePlatosDisponibles()
        {
            if (_listaDePlatosDisponibles.Count > 0)
            {
                return _listaDePlatosDisponibles;
            }
            throw new ListaVaciaException("La Lista de los Platos Disponibles está Vacia");
        }

        public List<IConsumible> GetListaDePlatosNoDisponibles()
        {
            if (_listaDePlatosNoDisponibles.Count > 0)
            {
                return _listaDePlatosNoDisponibles;
            }
            throw new ListaVaciaException("La Lista de los Platos NO Disponibles está Vacia");
        }
    }
}
