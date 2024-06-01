using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;


namespace Entidades
{
    /// <summary>
    /// Clase Publica: Proveedor
    /// </summary>
    public class Proveedor : IProveedor
    {
        private static int _contadorId = 0;
        private readonly int _id;


        private string? _nombre;
        private string? _cuit;
        private string? _direccion;
        private ECategoriaDEProducto _tipoDeProductoQueProvee;
        private EMediosDePago _medioDePago;
        private EAcreedor _esAcreedor;
        private EDiaDeLaSemana _diaDeEntrega;
        

        public Proveedor(string nombre, string cuit, string direccion, ECategoriaDEProducto tipoDeproducto, EMediosDePago medioDePago,EAcreedor esAcreedor, EDiaDeLaSemana diaDeEntrega) 
        {
            Nombre = nombre;
            Cuit = cuit;
            Direccion = direccion;
            TipoDeProductoQueProvee = tipoDeproducto;
            MediosDePago = medioDePago;
            EsAcreedor = esAcreedor;
            DiaDeEntrega = diaDeEntrega;
            _id = ++_contadorId;
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

        public ECategoriaDEProducto TipoDeProductoQueProvee 
        { 
            get { return _tipoDeProductoQueProvee; }
            set { _tipoDeProductoQueProvee = value; }
        }

        public EMediosDePago MediosDePago
        {
            get { return _medioDePago; }
            set { _medioDePago = value;}
        }
        public EAcreedor EsAcreedor
        {
            get { return _esAcreedor; }
            set { _esAcreedor = value;}
        }
        public EDiaDeLaSemana DiaDeEntrega
        {
            get { return _diaDeEntrega; }
            set { _diaDeEntrega = value; }
        }
        public int ID
        {
            get { return _id; }
        }
        public override string ToString()
        {
            return $"ID: {ID}, Nombre: {Nombre}, CUIT: {Cuit}, Direccion: {Direccion}, Tipo de Producto que Provee: {TipoDeProductoQueProvee}, Medio de Pago: {MediosDePago}, Es Acreedor? : {EsAcreedor}, Dia de Entrega: {DiaDeEntrega}";
        }
    }
}
