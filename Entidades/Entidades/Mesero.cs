using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entidades.Enumerables;
using Entidades.Interfaces;

namespace Entidades
{
    public class Mesero : Empleado, ICobrador
    {
        private decimal _montoAcumulado = 0;

        private Mesero() { }     

        public Mesero(ERol rol, string nombre, string apellido, string contacto,string direccion, decimal salario)
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
            _montoAcumulado += monto;
        }

        //public void CerrarMesa(Mesa.Id)
        //{
        //    throw new NotImplementedException();
        //}

        

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
