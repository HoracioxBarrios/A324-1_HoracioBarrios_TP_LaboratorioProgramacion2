using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{   /// <summary>
    /// Clase que representa un Menú - se va a ofrecer si tiene al menos un consumible disponible dentro
    /// </summary>
    public class Menu :IMenu
    {
        private List<IConsumible> _listaDeConsumiblesBebidasYPlatos;// Bebidas y Platos
        private string _nombre;
        private bool _disponibilidad;
         

        public Menu(string nombre)
        {
            _nombre = nombre;
            _listaDeConsumiblesBebidasYPlatos = new List<IConsumible>();
            _disponibilidad = false;
        }


        public void Agregar(IConsumible consumible)
        {
            _listaDeConsumiblesBebidasYPlatos.Add(consumible);
            _disponibilidad = true;
        }
        public void Quitar(IConsumible consumible)
        {
            _listaDeConsumiblesBebidasYPlatos.Remove(consumible);
            if(_listaDeConsumiblesBebidasYPlatos.Count == 0)
            {
                _disponibilidad = false;
            }
        }


        public List<IConsumible> GetBebidasInMenu()
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
        public List<IConsumible> GetPlatosInMenu()
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
            get { return _nombre; }
            set { _nombre = value; }
        }

        public bool Disponibilidad
        {
            get { return _disponibilidad; }
            set
            {
                if (_listaDeConsumiblesBebidasYPlatos.Count > 0)
                {
                    _disponibilidad = true;
                }
            }
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
