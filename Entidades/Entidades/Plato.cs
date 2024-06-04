using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Plato : IConsumible
    {
        private List<IConsumible> _ingredientes;// Ingrediente es un iproducto
        private string _nonbre;
        private decimal _precio;
        private bool _disponibilidad;
        

        


        public Plato(string nombre, List<IConsumible> listaIngredientes) 
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

        
        public bool Disponibilidad 
        {
            get {return _disponibilidad; } set { _disponibilidad = value; } 
        }
        public ECategoriaConsumible Categoria { get; set; }
        public string Nombre 
        { 
            get { return _nonbre; }
            set { _nonbre = value; }
        }
    }
}
