using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CorregirPedidoPlatoCommand : ICommand<>
    {
        private Stock _stock;
        private Plato _plato;

        public CorregirPedidoPlatoCommand(Stock restaurante, Plato plato)
        {
            _restaurante = restaurante;
            _plato = plato;
        }

        public string EjecutarAccion()
        {
            // Lógica para corregir el pedido del plato
            return $"Pedido del plato {_plato.Nombre} corregido con éxito.";
        }
    }
}
