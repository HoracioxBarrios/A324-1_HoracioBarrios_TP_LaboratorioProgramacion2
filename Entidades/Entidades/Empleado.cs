using Entidades.Enumerables;
using Entidades.Interfaces;
using System.Net;

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
        private string _password;
        private int _id;
        private EStatus _status;

        
        protected Empleado(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario)
        {
            _rol = rol;
            _nombre = nombre;
            _apellido = apellido;
            _contacto = contacto;
            _direccion = direccion;
            _salario = salario;
            _status = EStatus.Activo;
            _password = "123456";
        }
        protected Empleado(int id, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) :this(rol, nombre,  apellido,  contacto, direccion,  salario)
        { 
            _id = id;        
        }

        protected Empleado(int id,string password, EStatus status,  ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this (
            id, rol, nombre, apellido, contacto, direccion, salario)
        {
            _password = password;
            _status = status;
        }



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
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Password
        {
            get { return _password; }
            set { Password = value; }
        }
        public EStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Apellido: {Apellido}, Contacto: {Contacto}, Rol: {Rol}, Direccion: {Direccion}, Salario: {Salario}";
        }
    }
}