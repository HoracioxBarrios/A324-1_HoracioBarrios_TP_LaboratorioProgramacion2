using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;


namespace Negocio
{
    public class GestorDeEmpleados : IGestorDeEmpleados
    {
        private List<IEmpleado> _listaDeEmpleados;
        private IOperacionesDeBaseDeDatos<IEmpleado> _operacionesDeBaseDeDatos;

        public GestorDeEmpleados(IOperacionesDeBaseDeDatos<IEmpleado> operacionesDeBaseDeDatos)
        {
            _listaDeEmpleados = new List<IEmpleado>();
            _operacionesDeBaseDeDatos = operacionesDeBaseDeDatos;
        }

        public bool CrearEmpleado(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario)
        {
            bool seCreo = false;
            try
            {
                //EL EMPLEADO SERVICE FACTORY PODRIA ESTAR EN _operacionesDeBaseDeDatos?

                IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(rol, nombre, apellido, contacto, direccion, salario);
                if (empleado != null)
                {
                    seCreo = _operacionesDeBaseDeDatos.Create(empleado);                    
                    if (seCreo)
                    {
                        _listaDeEmpleados.Add(empleado);
                        return seCreo;
                    }
                    
                }
                return seCreo;
            }
            catch (EmpleadoDatosException ex)
            {
                throw new EmpleadoErrorAlCrearException("No se Pudo crear el Empleado: ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error desconocido : {ex.Message}", ex);
            }
            
        }

        public void EditarEmpleado()
        {
            _operacionesDeBaseDeDatos
        }

        public void EliminarEmpleado()
        {
            _operacionesDeBaseDeDatos
        }

        //public List<IEmpleado> GetEmpleados()
        //{
        //    if(_listaDeEmpleados.Count > 0)
        //    {
        //        return _listaDeEmpleados;
        //    }
        //    throw new ListaVaciaException("La lista esta vacia");
        //}
        //public IEmpleado GetEmpleado(string nombreEmpleado)
        //{
        //    if (_listaDeEmpleados.Count > 0)
        //    {
        //        foreach(IEmpleado empleado in _listaDeEmpleados)
        //        {
        //            if(empleado.Nombre == nombreEmpleado)
        //            {
        //                return empleado;                        
        //            }
        //        }
        //    }
        //    throw new ListaVaciaException("La lista esta vacia");
        //}

        //private int VerificarExistenciaDeEmpleado(string nombre, string apellido)
        //{

        //}

    }
}
