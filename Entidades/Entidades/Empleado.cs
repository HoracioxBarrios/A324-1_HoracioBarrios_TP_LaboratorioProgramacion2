using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public abstract class Empleado
    {
        private string? _nombre;
        private string? _apellido;

        protected Empleado(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;

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
    }
}