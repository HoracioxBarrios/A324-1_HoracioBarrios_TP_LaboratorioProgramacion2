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

        private List<IPago> _historialPagosSalarioMensual;
        private bool _cobroMensualPendienteACobrar;

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

            _historialPagosSalarioMensual = new List<IPago>();
            _cobroMensualPendienteACobrar = true;
        }
        protected Empleado(int id, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(rol, nombre, apellido, contacto, direccion, salario)
        {
            _id = id;
        }

        protected Empleado(int id, string password, EStatus status, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(
            id, rol, nombre, apellido, contacto, direccion, salario)
        {
            _password = password;
            _status = status;
        }


        /// <summary>
        /// Recie el pago y cambia su estado de CobroMensualPendienteACobrar a False
        /// </summary>
        /// <param name="pagoMensual"></param>
        public void RecibirPago(IPago pagoMensual)
        {
            decimal montoMensual = pagoMensual.Monto;
            if(montoMensual > 0 && montoMensual == Salario)
            {
                _historialPagosSalarioMensual.Add(pagoMensual);

                _cobroMensualPendienteACobrar = false; // --- Se tiene que volver a setear con EVENTOS cuando empiece un nuevo mes (Usando DateTime o algo parecido ) asi refleja que ese mes esta pendiente a Cobrar. ----
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
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
            set { _salario = value; }
        }
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public EStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public List<IPago> HistorialDePagosSalariosMensual
        {
            get {return _historialPagosSalarioMensual;}
            private set {_historialPagosSalarioMensual = value;}
        }
        public bool CobroMensualPendienteACobrar
        {
            get { return _cobroMensualPendienteACobrar; }
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Apellido: {Apellido}, Contacto: {Contacto}, Rol: {Rol}, Direccion: {Direccion}, Salario: {Salario}";
        }
    }
}