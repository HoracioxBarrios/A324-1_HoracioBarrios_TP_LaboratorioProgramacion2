using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pago : IPago
    {
        private string _nombreDelCobrador;

        private decimal _monto;

        private DateTime _fechaDePago;

        private bool _pendiente; // en el caso de ser proveedor acreeedor podemos usar esto.
        public Pago(string nombreCobrador, decimal monto)
        {
            _nombreDelCobrador = nombreCobrador;
            _monto = monto;
            _fechaDePago = DateTime.Now; // Fecha actual al momento de crear el pago

            _pendiente = false;
        }

        public void EstablecerPagoPendienteAProveedorAcreedor()
        {
            _pendiente= true;
        }
        public void EstablecerPagoNoPendienteAProveedor()
        {
            _fechaDePago= DateTime.Now;
            _pendiente = false;
        }

        public bool PagoPendienteAProveedor
        {
            get { return _pendiente; }
        }

        public string NombreCobrador 
        { 
            get { return _nombreDelCobrador; }
            set { _nombreDelCobrador= value; }
        }
        public decimal Monto 
        {
            get { return _monto; }
            set { _monto = value; }
        }
        public DateTime FechaPago 
        {
            get { return _fechaDePago; }
            set { _fechaDePago = value; }
        
        }
        public override string ToString()
        {
            return $"Fecha: {FechaPago}, Cobrador: {NombreCobrador}, Monto: {Monto}";
        }
    }
}
