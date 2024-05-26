using Entidades.Interfaces;
using System;
using System.Collections.Generic;

namespace Entidades
{
    public class EstablecerPrecioPlatoCommand : ICommand<string>
    {
        private Plato _plato;
        private decimal _precio;

        public EstablecerPrecioPlatoCommand(Plato plato, decimal precio)
        {
            _plato = plato;
            _precio = precio;
        }

        public string EjecutarAccion()
        {
            _plato.Precio = _precio;
            return $"Precio del plato {_plato.Nombre} establecido en {_precio:C}.";
        }
    }
}
