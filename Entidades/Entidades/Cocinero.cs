using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;

namespace Entidades
{



    public class Cocinero : Empleado, ICocinero, IPreparadorDePedidos
    {
        private List<IConsumible> _ingredientesSelecionados;
        private Queue<IPedido> _pedidosConPlatosParaCocinar;




        public Cocinero(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) :base(
            rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Contacto = contacto;
            this.Direccion = direccion;
            this.Salario = salario;
            this.Rol = rol;

            _ingredientesSelecionados = new List<IConsumible>();
            _pedidosConPlatosParaCocinar = new Queue<IPedido>();
        }
        public Cocinero(int id, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(
            rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Id = id;
        }
        public Cocinero(int id, string password, EStatus status, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(
            id, rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Password = password;
            this.Status = status;
        }




        /// <summary>
        /// Verifica si el plato esta en la lista de Menu
        /// </summary>
        /// <param name="nombrePlato"></param>
        /// <returns>Devuelve si existe en la Lista de Platos </returns>
        private bool ExistePlatoEnLista(string nombrePlato, List<IConsumible> listaPlatosEnMenu)
        {
            bool seEncontro = false;
            foreach (IConsumible plato in listaPlatosEnMenu)
            {
                if (plato.Nombre == nombrePlato)
                {
                    seEncontro = true;
                    break;
                }
            }
            return seEncontro;
        }


        /// <summary>
        /// Al momento de Crear un plato debe contar antes con ingredientes en la lista para el plato, y debe tener al menos 2 ingredientes.
        /// Al Crear el Plato, limpia la lista de ingredientes del cocinero para que este proceda a elejir nueva conbinacion de ingredientes para un nuevo plato.
        /// </summary>
        /// <param name="nombreDelPlato"></param>
        /// <returns></returns>
        /// <exception cref="AlObtenerListaDeIngredientesException"></exception>
        public IConsumible CrearPlato(string nombreDelPlato, List<IConsumible> ingredientes, int tiempoDePreparacion, EUnidadDeTiempo unidadDeTiempo)
        {
            
            if ( ingredientes.Count < 2)
            {
                throw new AlObtenerListaDeIngredientesException("El plato debe tener al menos 2 ingredientes.");
            }

            IConsumible plato =  new Plato(nombreDelPlato, ingredientes, tiempoDePreparacion, unidadDeTiempo);

            return plato;
        }


        public IConsumible EditarPlato(IConsumible plato, List<IConsumible> ingredientesActualizacion)
        {
            if (ingredientesActualizacion == null || ingredientesActualizacion.Count < 2)
            {
                throw new AlObtenerListaDeIngredientesException("El plato debe tener al menos 2 ingredientes.");
            }

            Plato platoActualizado = plato as Plato;
            if (platoActualizado == null)
            {
                throw new InvalidCastException("El objeto plato no puede ser convertido a Plato.");
            }

            platoActualizado.ReemplazarIngredientes(ingredientesActualizacion);

            return platoActualizado as IConsumible;
        }


        public void EliminarPlato(string nombre, List<IConsumible> listaDePlatos)
        {
            for (int i = 0; i < listaDePlatos.Count; i++)
            {
                if (listaDePlatos[i].Nombre == nombre)
                {
                    listaDePlatos.RemoveAt(i);
                    break; 
                }
            }
        }

        public bool PrepararPedido(IPedido pedido)
        {
            bool esEntregable = false;
            if (pedido == null)
            {
                throw new ArgumentNullException(nameof(pedido), "El pedido no puede ser nulo.");
            }

            _pedidosConPlatosParaCocinar.Enqueue(pedido);
            if(_pedidosConPlatosParaCocinar.Count > 0)
            {
               esEntregable = CocinarPlatosDelPedido();
            }

            return esEntregable;
        }


        /// <summary>
        /// Cocina los platos del Pedido
        /// </summary>
        /// <returns>Si todos los platos fueron cocinados devuelve true, sino false</returns>
        private bool CocinarPlatosDelPedido()
        {
            bool esEntregable = false;
            if (_pedidosConPlatosParaCocinar.Count > 0)
            {
                IPedido pedidoActual = _pedidosConPlatosParaCocinar.Peek(); // Tomamos el primer pedido sin eliminarlo de la cola                
                List<IConsumible> platosDelPedido = ObtenerPlatosDelPedido(pedidoActual);
                
                foreach (var plato in platosDelPedido)
                {
                    if (plato is ICocinable platoCocinable)
                    {
                        CocinarPlato(platoCocinable);
                    }
                }
                esEntregable = pedidoActual.VerificarSiEsEntregable();
                //if (esEntregable) // podria dejarlo para tener un historial de los pedidos con sus platos cocinados
                //{
                //    _pedidosConPlatosParaCocinar.Dequeue();//quitamos de la lista el pedido que ya se cocinaron los platos
                //}              

            }
            return esEntregable;
        }

        private List<IConsumible> ObtenerPlatosDelPedido(IPedido pedido)
        {
            List<IConsumible> platos = new List<IConsumible>();

            foreach (var consumible in pedido.GetPlatos())
            {
                if (consumible is IConsumible plato)
                {
                    platos.Add(plato);
                }
            }

            return platos;
        }


        public void CocinarPlato(ICocinable plato)
        {
            plato.Cocinar();
        }
    





        /// <summary>
        /// En Base a los IProductos o IConsumibles (Ingredientes) que estan disponibles en STOCK, Permite seleccionar los necesarios para el Cocinero pueda crear sus platos
        /// </summary>
        /// <param name="listaDeConsumiblesEnStock"></param>
        /// <param name="nombreDelIngrediente"></param>
        /// <param name="cantidadNecesaria"></param>
        /// <param name="unidadDeMedida"></param>
        public void SeleccionarIngredienteParaElPlato(List<IConsumible> listaDeConsumiblesEnStock, string nombreDelIngrediente, double cantidadNecesaria, EUnidadDeMedida unidadDeMedida)
        {
            IConsumible ingrediente = IngredienteService.ObtenerIngredienteParaPlato(listaDeConsumiblesEnStock, nombreDelIngrediente, cantidadNecesaria, unidadDeMedida);
            if (ingrediente != null)
            {
                _ingredientesSelecionados.Add(ingrediente);
            }
        }

        public List<IConsumible> GetListaDeIngredientesSeleccionados()
        {
            return _ingredientesSelecionados;
        }
    }
}
