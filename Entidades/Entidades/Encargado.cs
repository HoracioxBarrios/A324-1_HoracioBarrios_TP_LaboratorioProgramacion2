using System;
using System.Collections.Generic;
using Entidades.Enumerables;
using Entidades.Interfaces;

namespace Entidades
{
    public class Encargado : Empleado, IGestionarStock, IEstablecerPrecioPlato, ICorregirPedido
    {
        private Encargado()
        {
            this.Rol = ERol.Encargado;
        }
        public Encargado(string nombre, string apellido, string contacto, string direccion, decimal salario) :this()
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Contacto = contacto;
            this.Direccion = direccion;
            this.Salario = salario;
            
        }

        public void ConsultarStock()
        {
            throw new NotImplementedException();
        }

        public void ConsultarStockPorAgotamiento()
        {
            throw new NotImplementedException();
        }

        public void CargarAStock()
        {
            throw new NotImplementedException();
        }
          

        public void EstablecerPrecioPlato()
        {
            throw new NotImplementedException();
        }

        public void CorrigePedidoPlato()
        {
            throw new NotImplementedException();
        }
    }
}
