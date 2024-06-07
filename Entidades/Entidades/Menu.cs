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


        public List<IConsumible> ObtenerBebidasInMenu()
        {
            List<IConsumible> nuevaListaDeBebidas = new List<IConsumible>();
            foreach(IConsumible consumible in _listaDeConsumiblesBebidasYPlatos)
            {
                if(consumible is Bebida)
                {
                    nuevaListaDeBebidas.Add(consumible);
                }
            }
            return nuevaListaDeBebidas;
        }
        public List<IConsumible> ObtenerPlatosInMenu()
        {
            List<IConsumible> nuevaListaDePlatos = new List<IConsumible>();
            foreach (IConsumible consumible in _listaDeConsumiblesBebidasYPlatos)
            {
                if (consumible is Plato)
                {
                    nuevaListaDePlatos.Add(consumible);
                }
            }
            return nuevaListaDePlatos;
        }
        public List<IConsumible> ObtenerAllItemsDelMenu()
        {
            return _listaDeConsumiblesBebidasYPlatos;
        }
        public string Nombre
        {
            get { return _nombreDelMenu; }
            set { _nombreDelMenu = value; }
        }
        public override string ToString() 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nombre del Menú: {Nombre}");
            sb.AppendLine($"------- Los Platos del Menú: --------");
            foreach(IConsumible consumible in _listaDeConsumiblesBebidasYPlatos)
            {
                if(consumible is Plato)
                {
                    sb.AppendLine($"{consumible}");
                }
            }
            sb.AppendLine("--------- Las Bebidas del Menú: ");
            foreach (IConsumible consumible in _listaDeConsumiblesBebidasYPlatos)
            {
                if ( consumible is Bebida)
                {
                    sb.AppendLine($"{consumible}");
                }
            }

            return sb.ToString();
        }
    }
}
