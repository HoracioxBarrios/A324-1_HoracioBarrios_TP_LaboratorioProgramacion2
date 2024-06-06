using Entidades.Enumerables;
using Entidades.Interfaces;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Clase Abstracta Producto :
    /// Implementa la interface IProducto, Las clases derivadas seran tambien un IProducto
    /// </summary>
    public abstract class Producto : IProducto, IProductoCreable
    {
        private string _nombre;
        private decimal _precioUnitario;
        private ITipoUnidadDeMedida _iTipoUnidadDeMedida;//Guarda la cantidad y por medio del Get : Cantidad tenemos acceso al dato
        private ETipoDeProducto _eTipoDeProducto;
        private bool _disponibilidad;
        private EUnidadMedida _eUnidadDeMedidad;
        private IProveedor _proveedor;
        private int _id;



        protected Producto(int id,string nombre, double cantidad, EUnidadMedida eUnidadDeMedida, decimal precio, ETipoDeProducto eTipoDeProducto,  IProveedor iProveedor)
        {

            _nombre = nombre;
            _precioUnitario = precio;
            _eUnidadDeMedidad = eUnidadDeMedida;
            _iTipoUnidadDeMedida = UnidadesDeMedidaServiceFactory.CrearUnidadDeMedida(eUnidadDeMedida, cantidad);
            _eTipoDeProducto = eTipoDeProducto;            
            _proveedor = iProveedor;
            if (Cantidad > 0) { _disponibilidad = true; }
            _id = id;
        }

        public abstract decimal CalcularPrecio();

        public string Nombre 
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public double Cantidad
        {
            get { return _iTipoUnidadDeMedida.Cantidad; }
            set { _iTipoUnidadDeMedida.Cantidad = value; }
        }
        public decimal Precio
        {
            get { return _precioUnitario; }
            set { _precioUnitario = value; }
        }
        
        public ITipoUnidadDeMedida TipoDeUnidadDeMedida 
        { 
            get { return _iTipoUnidadDeMedida; }
            set { _iTipoUnidadDeMedida = value; }
        }
        public ETipoDeProducto ETipoDeProducto 
        {  
            get { return _eTipoDeProducto; }
            set { _eTipoDeProducto = value; }
        }
        public bool Disponibilidad
        {
            get { return Cantidad > 0; }
            set { _disponibilidad = value; }
        }
        public EUnidadMedida EUnidadDeMedida 
        {
            get { return _eUnidadDeMedidad; }
            set { _eUnidadDeMedidad = value; }
        }
        public IProveedor Proveedor 
        { 
            get { return _proveedor; }
            set { _proveedor = value;}
        }
        public int Id 
        {  
            get { return _id;}
            set { _id = value; }
        }











    }
}
