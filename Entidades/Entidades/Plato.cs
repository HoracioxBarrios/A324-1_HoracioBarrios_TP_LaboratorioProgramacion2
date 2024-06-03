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

        private decimal _precio;

        private bool _dispinible;
        public string Nombre { get; set; }

        


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

        public ECategoriaConsumible Categoria { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Disponibilidad { get; set; }
    }
}
