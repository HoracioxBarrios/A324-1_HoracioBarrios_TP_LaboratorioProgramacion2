using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{

    public delegate void PlatoListoDelegate(Plato plato);

    public class Plato : ICocinable, IConsumible, IVendible
    {
        private string _nombre;
        private List<IConsumible> _ingredientesSeleccionadosParaEstePlato;
        private decimal _precioDeCosto;
        private decimal _precioDeVenta ;
        private ECategoriaConsumible _categoriaDeConsumible;
        private bool _disponibilidad;
        private double _cantidad;
        private bool _listoParaEntregar;

        public event PlatoListoDelegate EventPlatoListo;


        private TimeSpan _tiempoDePreparacion;
        private Stopwatch _cronometro; 
        private DateTime _horaInicioCocinado;
        private DateTime _horaFinCocinado;









        private Plato()
        {
            _precioDeVenta = 0;
            _disponibilidad = false;
            _ingredientesSeleccionadosParaEstePlato = new List<IConsumible>();
            _listoParaEntregar = false;
        }

        private Plato(string nombre, List<IConsumible> listaIngredientesSeleccionados) :this()
        {
            Nombre = nombre;
            _ingredientesSeleccionadosParaEstePlato = listaIngredientesSeleccionados ?? new List<IConsumible>(); //usamos operador (operador de coalescencia nula )
            _precioDeCosto = CalcularPrecioDeCosto();
        }

        public Plato(string nombre, List<IConsumible> listaIngredientesSeleccionados, int tiempoPreparacion, EUnidadDeTiempo unidadDeTiempo) :this(nombre, listaIngredientesSeleccionados)
        {
             if(unidadDeTiempo == EUnidadDeTiempo.Segundos)
            { 
                _tiempoDePreparacion = TimeSpan.FromSeconds(tiempoPreparacion);
            }
            if(unidadDeTiempo == EUnidadDeTiempo.Minutos)
            {
                _tiempoDePreparacion = TimeSpan.FromMinutes(tiempoPreparacion);
            }
        }


        public async Task Cocinar()
        {
            _listoParaEntregar = false;

            _cronometro = Stopwatch.StartNew();
            _horaInicioCocinado = DateTime.Now;

            await Task.Delay(_tiempoDePreparacion); // Espera el tiempo de preparación sin bloquear el hilo principal
            _cronometro.Stop();
            _horaFinCocinado = DateTime.Now;

            _listoParaEntregar = true;// se termina de cocinar y es entregable

            OnPlatoListo();
        }


        private void OnPlatoListo()
        {
                EventPlatoListo?.Invoke(this); // Invoca el evento para notificar que el plato está listo
        }




        /// <summary>
        /// Calcula el precio Base segun el precio de sus ingredientes.
        /// 
        /// </summary>
        /// <returns>devuelve el precio de COSTO del plato</returns>
        public decimal CalcularPrecioDeCosto()
        {
            decimal precio = 0;

            foreach (IConsumible ingrediente in _ingredientesSeleccionadosParaEstePlato)
            {
                precio += ingrediente.CalcularPrecioDeCosto();
            }
            _precioDeCosto = precio;
            return precio;
        }
        /// <summary>
        /// obtiene el precio de costo
        /// </summary>
        /// <returns></returns>
        public decimal GetPrecioDeCosto()
        {
            return _precioDeCosto;
        }



        public bool VerificarStockIngredientes(List<IConsumible> ingredientesEnStock)
        {
            if (ingredientesEnStock == null || ingredientesEnStock.Count == 0)
            {
                throw new ListaVaciaException("La lista de ingredientes en stock está vacía.");
            }

            foreach (IConsumible ingrediente in _ingredientesSeleccionadosParaEstePlato)
            {
                bool encontrado = false;
                foreach (IConsumible ingredienteEnStock in ingredientesEnStock)
                {
                    if (ingrediente.Nombre == ingredienteEnStock.Nombre)
                    {
                        // Comparar cantidades disponibles
                        if ((Ingrediente)ingredienteEnStock > (Ingrediente)ingrediente)
                        {
                            encontrado = true;
                            break;
                        }
                    }
                }
                if (!encontrado)
                {
                    _disponibilidad = false;
                    return _disponibilidad;
                }
            }
            _disponibilidad = true;
            return _disponibilidad;
        }





        public Ingrediente ObtenerIngrediente(string nombreIngrediente)
        {
            foreach (IConsumible consumible in _ingredientesSeleccionadosParaEstePlato)
            {
                Ingrediente? ingrediente = consumible as Ingrediente;
                if (ingrediente != null && ingrediente.Nombre == nombreIngrediente)
                {
                    return ingrediente;
                }
            }
            return null; 
        }

        public List<IConsumible> ObtenerIngredientes()
        {
            return _ingredientesSeleccionadosParaEstePlato;
        }
        public void AgregarIngredienteAListaDeIngredientes(IConsumible ingrediente)
        {
            if (ingrediente != null)
            {
                _ingredientesSeleccionadosParaEstePlato.Add(ingrediente);
            }

        }

        public void ReemplazarIngredientes(List<IConsumible> ingredientes)
        {
            if (ingredientes.Count > 0)
            {
                _ingredientesSeleccionadosParaEstePlato = ingredientes;
            }
        }


        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }


        public ECategoriaConsumible Categoria
        {
            get { return _categoriaDeConsumible; }
            set { _categoriaDeConsumible = value; }
        }

        public bool Disponibilidad 
        {
            get { return _disponibilidad; } 
            set { _disponibilidad = value; } 
        }

        public TimeSpan TiempoDePreparacion
        {
            get { return _tiempoDePreparacion; }
            set { _tiempoDePreparacion = value; }
        }

        /// <summary>
        /// Precio Unitario DE VENTA./
        /// -- SETTER : Establece el precio de venta 
        /// ---GETTER : Devuelve le precio de venta
        /// </summary>
        public decimal Precio
        {
            get { return _precioDeVenta; }
            set
            {
                if (value > _precioDeCosto)
                {
                    _precioDeVenta = value;
                }
            }
        }

        public double Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        public bool ListoParaEntregar
        {
            get { return _listoParaEntregar; }
        }

        public DateTime HoraInicioCocinado
        {
            get { return _horaInicioCocinado; }
        }

        public DateTime HoraFinCocinado
        {
            get { return _horaFinCocinado; }
        }

        public TimeSpan TiempoTranscurrido
        {
            get { return _cronometro.Elapsed; }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nombre: {Nombre},Su Precio de Venta: {Precio}, Precio de Costo {_precioDeCosto}");
            return sb.ToString();
        }

    }
}
