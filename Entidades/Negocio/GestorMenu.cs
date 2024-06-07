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
    public class GestorMenu
    {
        private List<IMenu> _listaDeMenus;
        private List<IConsumible> _listaDeTodosLosPlatos;
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

        public void AgregarPlatoAMenu(string nombreMenu, string nombrePlato, List<IConsumible> listaDeIngredientes)
        {
            IMenu menu = null;
            foreach (IMenu m in _listaDeMenus)
            {
                if (m.Nombre == nombreMenu)
                {
                    menu = m;
                    break;
                }
            }
            if (menu == null)
            {
                throw new MenuNoExisteException("El menú no existe.");
            }

            IConsumible plato = _cocinero.CrearPlato(nombrePlato, listaDeIngredientes);
            ((Menu)menu).Agregar(plato); 
            _listaDeTodosLosPlatos.Add(plato); 
        }

        public List<IMenu> GetListaDeMenu()
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
