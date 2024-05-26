using Entidades.Interfaces;
using Entidades.Enumerables;

namespace Entidades
{
    public class Restoran
    {
        private List<IEmpleado> _listEmpleados;
        private List<IConsumible> _listConsumibles;
        private List<IProducto> _listaProductos;

        public Restoran()
        {
            IEmpleado empleadoMesero1 = new Mesero("Raul", "Carizo", "contacto123", "San Pepe 4124", 15000M);
            IEmpleado empleadoMesero2 = new Mesero("Alice", "Tuil", "contacto555",  "El Sol 51", 15000M);
            IEmpleado empleadoMesero3 = new Mesero("Olivia", "Bliu", "contacto621", "El Dia 10", 15000M);
            IEmpleado empleadoCocinero1 = new Cocinero("Kled", "Smolder", "Contacto23",  "Calle 12", 30500M );
            IEmpleado empleadoDelivery1 = new Delivery("Larry", "DKlay", "Contacto74", "San Jope", 10000M);
            IEmpleado empleadoEncargado1 = new Encargado("Jarvan", "4to", "Grieta del invocador", "San Def", 45000M);
            _listEmpleados.Add(empleadoMesero1 );
            _listEmpleados.Add(empleadoMesero2 );
            _listEmpleados.Add(empleadoMesero3 );
            _listEmpleados.Add(empleadoCocinero1 );
            _listEmpleados.Add(empleadoDelivery1 );
            _listEmpleados.Add(empleadoEncargado1);


            

        }



    }
}
