using System;
using System.Collections.Generic;
using Entidades.Enumerables;
using Entidades.Interfaces;

namespace Entidades
{
    
    public class Delivery : Empleado, ICobrador
    {
        private decimal _montoAcumulado;

   
        public Delivery(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : base(
            rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Contacto = contacto;            
            this.Direccion = direccion;
            this.Salario = salario;
            this.Rol = rol;
        }

        public void Cobrar(decimal monto)
        {
            throw new NotImplementedException();
        }
        public decimal MontoAcumulado
        {
            get
            {
                return _montoAcumulado;
            }
            set
            {
                if (value > 0)
                {
                    _montoAcumulado += value;
                }
            }
        }
    }
}
