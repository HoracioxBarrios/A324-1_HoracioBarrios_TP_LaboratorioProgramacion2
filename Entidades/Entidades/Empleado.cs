using Entidades.Enumerables;
using Entidades.Interfaces;

namespace Entidades
{
    public abstract class Empleado : IEmpleado
    {
        private string? _nombre;
        private string? _apellido;
        private string? _contacto;
        private ERol _rol;
        private string? _direccion;
        private decimal _salario;
        
        protected Empleado() { }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value;}
        }
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value;}
        }
        public string Contacto
        {
            get { return _contacto; }
            set { _contacto = value; }
        }
        public ERol Rol
        {
            get { return _rol; }
            set { _rol = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
        public decimal Salario
        {
            get { return _salario; }
            set { _salario=value; }
        }
        public override string ToString()
        {
            return $"Nombre: {Nombre}, Apellido: {Apellido}, Contacto: {Contacto}, Rol: {Rol}, Direccion: {Direccion}, Salario: {Salario}";
        }
    }
}