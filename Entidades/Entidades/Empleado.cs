using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Enumerables;

namespace Entidades
{
    public abstract class Empleado
    {
        private string? _nombre;
        private string? _apellido;
        private string? _direccion;
        private string? _contacto;
        private decimal _salario;
        private ERolEmpleado _rol;

        protected Empleado(string nombre, string apellido,ERolEmpleado rol)
        {
            Nombre = nombre;
            Apellido = apellido;
            _rol = rol;
        }
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _nombre = value;
                }
            }
        }
        public string Apellido
        {
            get { return _apellido; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _apellido = value;
                }
            }
        }
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }


    }
}