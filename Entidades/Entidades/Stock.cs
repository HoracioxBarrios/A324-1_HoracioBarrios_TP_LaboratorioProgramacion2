using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Stock
    {
        IProducto _producto;
        private List<IProducto> _productos;
        private List<IProducto> _productoPorAgotarse;
        public Stock()
        {

        }

        /// <summary>
        ///Propiedad GET:que sirve para Obtener la lista de IProductos ; o SET: Agregarle IProductos a la lista.
        /// </summary>
        public List<IProducto> Productos
        {
            get
            {
                if (_productos.Count > 0)
                    return _productos;
                else
                    return new List<IProducto>();
            }
            set
            {
                if (value != null)
                {
                    if (_productos == null)
                        _productos = new List<IProducto>();

                    _productos.AddRange(value);
                }
            }
        }


        /// <summary>
        /// Methodo Getter que devuelve una lista de IProductos por agotarse.
        /// </summary>
        /// <returns></returns>
        public List<IProducto> GetProductosPorAgotarse()
        {
            if(_productos.Count > 0)
            {
                foreach (IProducto producto in _productos)
                {
                    if(producto.Cantidad < 10)
                    {
                        _productoPorAgotarse.Add(producto);
                    }
                }
            }

            if(_productoPorAgotarse != null)
            {
                return _productoPorAgotarse ;
            }
            else
            {
                return new List<IProducto>();
            }
        }


    }
}
