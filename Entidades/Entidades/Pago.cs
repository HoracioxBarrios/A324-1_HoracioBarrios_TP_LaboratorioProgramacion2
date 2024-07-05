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
        public Pago(string nombreCobrador, decimal monto)
        {
            _nombreDelCobrador = nombreCobrador;
            _monto = monto;
            _fechaDePago = DateTime.Now; // Fecha actual al momento de crear el pago
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
