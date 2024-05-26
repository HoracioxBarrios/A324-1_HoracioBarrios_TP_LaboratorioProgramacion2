using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;

namespace Entidades
{
    public class Bebidas : Producto,IProducto,  IConsumible
    {
        private EDisponibilidad _disponiblididad;
        private ECategoriaConsumible _categoriaConsumible;
        public EDisponibilidad Disponibilidad { get => _disponiblididad; set => _disponiblididad = value; }
        public ECategoriaConsumible Categoria { get => _categoriaConsumible; set => _categoriaConsumible = value; }
    }
}
