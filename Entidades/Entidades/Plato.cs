using Entidades.Enumerables;
using Entidades.Excepciones;
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
        private List<IConsumible> _listaDeIngredientesParaEstePlato;// Ingrediente es un iproducto
        private string _nonbre;
        private decimal _precioDeCosto;
        private decimal _precioDeVenta ;
        private ECategoriaConsumible _categoriaDeConsumible;
        private DateTime _tiempoDePreparacion;
        private bool _disponibilidad = false;
        

        


        public Plato(string nombre, List<IConsumible> listaIngredientes) 
        { 
            Nombre = nombre;
            _listaDeIngredientesParaEstePlato = listaIngredientes;
            VerificarDisponibilidad();
            _precioDeCosto = 0;
            _precioDeVenta = 0;
        }



        /// <summary>
        /// Calcula el precio Base segun el precio de sus ingredientes.
        /// 
        /// </summary>
        /// <returns>devuelve el precio de COSTO del plato</returns>
        public decimal CalcularPrecio()
        {
            decimal precio = 0;

            foreach (IConsumible ingrediente in _listaDeIngredientesParaEstePlato)
            {
                precio += ingrediente.CalcularPrecio();
            }
            return precio;
        }

        /// <summary>
        /// Verifica si el plato tiene Ingredientes disponible.
        /// </summary>
        private void VerificarDisponibilidad()
        {
            bool estePlatoEstaDisponible = false;
            if (_listaDeIngredientesParaEstePlato.Count >= 2)
            {
                
                foreach (IConsumible ingrediente in _listaDeIngredientesParaEstePlato)
                {
                    Ingrediente ing = ingrediente as Ingrediente;
                    if(ing.Cantidad > 0)
                    {
                        estePlatoEstaDisponible = true;
                    }
                    estePlatoEstaDisponible = false;
                   
                }
            }
            _disponibilidad = estePlatoEstaDisponible;
        }

        public string Nombre
        {
            get { return _nonbre; }
            set { _nonbre = value; }
        }

        /// <summary>
        /// Obtiene o Establece el precio de Venta del plato
        /// </summary>
        public decimal Precio
        {
            get { return _precioDeVenta; }
            set
            {
                if (value > _precioDeCosto)
                { _precioDeVenta = value; }
            }
        }

        public ECategoriaConsumible Categoria
        {
            get { return _categoriaDeConsumible; }
            set { _categoriaDeConsumible = value; }
        }

        public bool Disponibilidad 
        {
            get {return _disponibilidad; } 
            set { _disponibilidad = value; } 
        }
        public List<IConsumible> GetIngredientesDelPlato()
        {
            if(_listaDeIngredientesParaEstePlato.Count == 0)
            {
                throw new ListaVaciaException("Error: La Lista de Ingredientes esta vacia");
            }
            return _listaDeIngredientesParaEstePlato;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nombre: {Nombre}, Precio: {Precio}, Categoria: {Categoria}");
            return sb.ToString();
        }

    }
}
