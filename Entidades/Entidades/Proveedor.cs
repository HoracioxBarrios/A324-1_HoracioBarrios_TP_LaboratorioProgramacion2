using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Entidades
{
    /// <summary>
    /// Clase Publica: Proveedor
    /// </summary>
    public class Proveedor : IProveedor
    {
        private static int _contadorId = 0;
        private int _id;


        private string? _nombre;
        private string? _cuit;
        private string? _direccion;
        private ETipoDeProducto _tipoDeProductoProveido;
        private EMediosDePago _medioDePago;// podria ser una lista de IMediosDePago
        private EAcreedor _esAcreedor;
        private EDiaDeLaSemana _diaDeEntrega;
        

        public Proveedor(string nombre, string cuit, string direccion, ETipoDeProducto tipoDeproducto, EMediosDePago medioDePago,EAcreedor esAcreedor, EDiaDeLaSemana diaDeEntrega) 
        {
            Nombre = nombre;
            Cuit = cuit;
            Direccion = direccion;
            TipoDeProducto = tipoDeproducto;
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

        public ETipoDeProducto TipoDeProducto 
        { 
            get { return _tipoDeProductoProveido; }
            set { _tipoDeProductoProveido = value; }
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
            private set { _id = value; }
        }
        public override string ToString()
        {
            return $"ID: {ID}, Nombre: {Nombre}, CUIT: {Cuit}, Direccion: {Direccion}, Tipo de Producto que Provee: {TipoDeProducto}, Medio de Pago: {MediosDePago}, Es Acreedor? : {EsAcreedor}, Dia de Entrega: {DiaDeEntrega}";
        }
    }
}
