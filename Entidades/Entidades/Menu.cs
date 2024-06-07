using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Menu :IMenu
    {
        private List<IConsumible> _listaDeConsumiblesBebidasYPlatos;// Bebidas y Platos
        private string _nombreDelMenu;
        
         

        public Menu(string nombre)
        {
            _nombreDelMenu = nombre;
            _listaDeConsumiblesBebidasYPlatos = new List<IConsumible>();
        }


        public void Agregar(IConsumible consumible)
        {
            _listaDeConsumiblesBebidasYPlatos.Add(consumible);
        }
        public void Quitar(IConsumible consumible)
        {
            _listaDeConsumiblesBebidasYPlatos.Remove(consumible);
        }


        public IConsumible ObtenerBebidas()
        {
            List<IConsumible> nuevaListaDeBebidas = new List<IConsumible>();
            foreach(IConsumible consumible in _listaDeConsumiblesBebidasYPlatos)
            {
                if(consumible is Bebida)
                {
                    nuevaListaDeBebidas.Add(consumible);
                }
            }
            return (IConsumible)nuevaListaDeBebidas;
        }
        public IConsumible ObtenerPlatos()
        {
            List<IConsumible> nuevaListaDePlatos = new List<IConsumible>();
            foreach (IConsumible consumible in _listaDeConsumiblesBebidasYPlatos)
            {
                if (consumible is Plato)
                {
                    nuevaListaDePlatos.Add(consumible);
                }
            }
            return (IConsumible)nuevaListaDePlatos;
        }
        public string Nombre
        {
            get { return _nombreDelMenu; }
            set { _nombreDelMenu = value; }
        }

    }
}
