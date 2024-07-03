using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pago :IPago
    {
        private static readonly object _lock = new object();
        private static int _contadorId = 0;


        private int _id;
        private int _idMesaOCliente;
        private int _idDelCobrador;
        private ERol _rolDelCobrador;
        private decimal _monto;
        private DateTime _fecha;
        private ETipoDePago _tipoDePago;








        public Pago(int idMesaOCliente, int idDelCobrador, ERol rolDelCobrador, decimal monto,  ETipoDePago tipoPago)
        {
            _idMesaOCliente = idMesaOCliente;
            _monto = monto;
            _fecha = DateTime.Now;
            _idDelCobrador = idDelCobrador;
            _tipoDePago = tipoPago; 


            
            lock (_lock)// usamos el lock para garantizar thread-safety
            {
                _id = ++_contadorId;
            }
        }


        public int Id 
        {
            get {return _id; }
            set { _id = value; }
        }
        public int IdMesaOCliente 
        { 
            get { return _idMesaOCliente; }
            set { _idMesaOCliente = value; }
        }
        public decimal Monto 
        { 
            get { return _monto; }
            set { _monto = value; }
        }

        public DateTime Fecha 
        { 
            get { return _fecha; }
            set { _fecha = value; }
        }
        public int IdDelCobrador
        {
            get { return _idDelCobrador; }
            set { _idDelCobrador= value; }
        }

        public ERol RolDelCobrador
        {
            get { return _rolDelCobrador; }
            set { _rolDelCobrador = value; }
        }



        public ETipoDePago TipoPago
        {
            get { return _tipoDePago; }
            set { _tipoDePago = value; }
        } 

    }
}
