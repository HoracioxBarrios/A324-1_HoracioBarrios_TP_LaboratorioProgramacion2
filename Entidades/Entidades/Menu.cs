using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Menu :IConsumible
    {
        private string _nombreDelMenu;
        private decimal _precio;
        private bool _disponibilidad;
        private List<IConsumible> _listaPlatos;
         
        public Menu(){}



        public void Agregar(IConsumible plato) 
        { 
            
        }
        public void Quitar() { }

        public decimal CalcularPrecio()
        {
            throw new NotImplementedException();
        }

        public string Nombre { get; set ; }
        public decimal Precio { get ; set ; }
        public bool Disponibilidad { get ; set ; }
    }
}
