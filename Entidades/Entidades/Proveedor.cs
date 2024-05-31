using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Proveedor : IProveedor
    {
        private static int _id = 0;

        private string _nombre;
        private string _cuit;
        private string _direccion;
        private ETipoDeProduto _tipoDeProductoQueProvee;
        private EMediosDePago _medioDePago;
        private EDiaDeLaSemana _diaDeEntrega;

        public Proveedor(string nombre, string cuit, string direccion, ETipoDeProduto tipoDeproducto, EMediosDePago medioDePago, EDiaDeLaSemana diaDeEntrega)
        {
            Nombre = nombre;
            Cuit = cuit;
            Direccion = direccion;
            TipoDeProductoQueProvee = tipoDeproducto;
            MediosDePago = medioDePago;
            DiaDeEntrega = diaDeEntrega;
        }
        public string Nombre 
        { 
            get { return _nombre; }
            set {  _nombre = value; }
        }

        public string Cuit 
        { 
            get {return _cuit;}
            set { _cuit = value;}                
        }

        public string Direccion 
        { 
            get { return _direccion; }
            set { _direccion = value; }
        }

        public ETipoDeProduto TipoDeProductoQueProvee 
        { 
            get { return _tipoDeProductoQueProvee; }
            set { _tipoDeProductoQueProvee = value; }
        }

        public EMediosDePago MediosDePago
        {
            get { return _medioDePago; }
            set { _medioDePago = value;}
        }
        public EDiaDeLaSemana DiaDeEntrega
        {
            get { return _diaDeEntrega; }
            set { _diaDeEntrega = value; }
        }
    }
}
