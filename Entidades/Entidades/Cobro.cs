using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cobro : ICobro
    {
        private static readonly object _lock = new object();
        private static int _contadorId = 0;


        private int _id;
        private int _idMesaOCliente;
        private int _idDelCobrador;
        private ERol _rolDelCobrador;
        private decimal _monto;
        private DateTime _fecha;
        private ETipoDePago _tipoDeCobro;

        private bool _contabilizado; // cuando se cierra el dia se recolectan todos los Cobros y se agregan a las arcas (con esto aclaramos que ya lo agregamos a las arcas luego)


        private Cobro()
        {
            _contabilizado = false;
        }



        public Cobro(int idMesaOCliente, int idDelCobrador, ERol rolDelCobrador, decimal monto, ETipoDePago tipoPago) : this()
        {
            _idMesaOCliente = idMesaOCliente;
            _monto = monto;
            _fecha = DateTime.Now;
            _idDelCobrador = idDelCobrador;
            _rolDelCobrador = rolDelCobrador;
            _tipoDeCobro = tipoPago;



            lock (_lock)// usamos el lock para garantizar thread-safety
            {
                _id = ++_contadorId;
            }
        }





        public void MarcarComoContabilizado()
        {
            _contabilizado = true;
        }
        public bool Contabilizado
        {
            get { return _contabilizado; }
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



        public ETipoDePago TiposDeCobro
        {
            get { return _tipoDeCobro; }
            set { _tipoDeCobro = value; }
        } 

    }
}
