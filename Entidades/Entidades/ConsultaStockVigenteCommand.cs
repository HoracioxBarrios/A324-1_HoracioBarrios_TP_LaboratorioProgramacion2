using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Input;


namespace Entidades
{
    public class ConsultaStockVigenteCommand :ICommand<IProducto>
    {
        private Restaurante _restaurante;
        private List<IProducto> _productos;
        public ConsultaStockVigenteCommand(Restaurante restaurante)
        {
            _restaurante = restaurante;
        }

        public void EjecutarAccion()
        {
            var stock = _restaurante.GetInventario();
            foreach (var producto in stock)
            {
                _productos.Add(producto);
            }
            if (_productos.Count > 0)
            {
                return _productos;
            }
            
        }



    }

}
