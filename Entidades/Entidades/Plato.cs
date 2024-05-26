using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Plato
    {
        private decimal _precio;
        public string Nombre { get; set; }

        private List<IIngrediente> _ingredientes;
        private EDisponibilidad Disponibilidad { get; set; }


        public Plato(string nombre, List<IIngrediente> listaIngredientes) 
        { 
            Nombre = nombre;
            _ingredientes = listaIngredientes;
        }

        public decimal Precio
        {
            get { return _precio; }
            set
            {
                if (value > 0)
                { _precio = value; }
            }
        }

    }
}
