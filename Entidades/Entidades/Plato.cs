using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Plato : IConsumible
    {
        private List<IConsumible> _ingredientes;// Ingrediente es un iproducto
        private string _nonbre;
        private decimal _precioDeCosto = 0;
        private decimal _precioDeVenta = 0;
        private bool _disponibilidad;
        

        


        public Plato(string nombre, List<IConsumible> listaIngredientes) 
        { 
            Nombre = nombre;
            _ingredientes = listaIngredientes;
        }

     
        /// <summary>
        /// Getea el precio de Ventay setea el precio de Venta
        /// </summary>
        public decimal Precio
        {
            get { return _precioDeVenta; }
            set
            {
                if (value > 0)
                { _precioDeVenta = value; }
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

        public decimal CalcularPrecio()
        {
            decimal precio = 0;
            if(_ingredientes.Count >= 2)
            {
                foreach(IConsumible ingrediente in _ingredientes)
                {
                    IConsumible ingrediente1 = ingrediente as Ingrediente;
                    if(ingrediente1 != null)
                    {
                        precio += ingrediente1.CalcularPrecio();
                    }                 
                }
                return precio;
            }
            return precio;
        }
    }
}
