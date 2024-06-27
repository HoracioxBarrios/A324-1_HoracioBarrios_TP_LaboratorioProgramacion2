using Datos;
using Entidades;
using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{

    public class Restoran
    {
        private IGestorDeEmpleados _gestorDeEmpleados;
        private IGestorDeProveedores _gestorDeProveedores;
        private IGestorProductos _gestorDeProductos;
        private IGestorMenu _gestorMenu;

        IOperacionesEmpleadoDB _operacionesDeBaseDatosEmpleados;

        public Restoran() 
        {
            IOperacionesEmpleadoDB operacionesDeBaseDeDatosEmpleados = new EmpleadoDB();
            _operacionesDeBaseDatosEmpleados = operacionesDeBaseDeDatosEmpleados;

            _gestorDeEmpleados = new GestorDeEmpleados(_operacionesDeBaseDatosEmpleados);

            //----- instancio los EMPLEADOS -------
            //bool seCreoEmpleado1 = _gestorDeEmpleados.CrearEmpleado(ERol.Cocinero, "Gille", "Rel", "1150654", "Av los pericos 5142", 20000m);
            //bool seCreoEmpleado2 = _gestorDeEmpleados.CrearEmpleado(ERol.Encargado, "Mar", "Ruy", "1156202", "Principio Solid 5", 30000m);
            //bool seCreoEmpleado3 = _gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Jun", "Fre", "117778", "Calle 88", 15000m);
            //bool seCreoEmpleado4 = _gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Kler", "Dry", "117563", "Calle 41", 15000m);
            //bool seCreoEmpleado5 = _gestorDeEmpleados.CrearEmpleado(ERol.Delivery, "Cris", "Lol", "115632", "Calle 56", 10000m);

            

            _gestorDeProveedores = new GestorDeProveedores();

            //----- Instancio los PROVEEDORES ---------
            _gestorDeProveedores.CrearProveedor("Aser S.A", "452", "Av Los Macacos 35", ETipoDeProducto.Almacen, EMediosDePago.Transferencia, EAcreedor.No, EDiaDeLaSemana.Lunes);
            _gestorDeProveedores.CrearProveedor("Carnes Argentinas SRL", "126", "Av Sin Agua 1500", ETipoDeProducto.Carniceria, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Miercoles);
            _gestorDeProveedores.CrearProveedor("Almacenes S.A", "598", "Av Sin Tierra 1500", ETipoDeProducto.Verduleria, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Jueves);
            _gestorDeProveedores.CrearProveedor("Verduras S.A", "5", "Av Sin Verduras 200", ETipoDeProducto.Verduleria, EMediosDePago.Contado, EAcreedor.No, EDiaDeLaSemana.Lunes);


            _gestorDeProductos = new GestorDeProductos();

            //------- instancio PRODUCTOS (INGREDIENTES)  Y Se Agregan al Stock ---------

            //Proveedor para Ingrediente 1 -ALMACEN
            IProveedor proveedor1 = _gestorDeProveedores.GetProveedor(1);
            //Proveedor para Ingrediente 2 -CARNICERIA
            IProveedor proveedor2 = _gestorDeProveedores.GetProveedor(2);
            //Proveedor para Ingrediente 3  -VERDULERIA
            IProveedor proveedor3 = _gestorDeProveedores.GetProveedor(3);
            //proveedor para Ingrediente 4 -VERDULERIA
            IProveedor proveedor4 = _gestorDeProveedores.GetProveedor(4);

            IProducto aceite = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "Aceite", 2, EUnidadDeMedida.Litro, 1000M, proveedor1);
            IProducto pollo = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "Pollo", 6, EUnidadDeMedida.Kilo, 6000M, proveedor2) ;
            IProducto tomate = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "Tomate", 2, EUnidadDeMedida.Kilo , 2000M, proveedor3);
            IProducto papa = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "Papa", 6, EUnidadDeMedida.Kilo, 6000M, proveedor4);
            IProducto lechuga = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "Lechuga",2, EUnidadDeMedida.Kilo, 2000M, proveedor4);
            IProducto carne = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "Carne", 10, EUnidadDeMedida.Kilo, 10000M, proveedor2);

            //---- Existe la manera de crear el producto con (CrearProducto() y luego agregarlo a la lista de productos de stock (AgregarproductoAStock()) -----

            // ------- Instancio PRODUCTOS (BEBIDAS)  y se Agregan al Stock ----------

            IProducto cocaCola = _gestorDeProductos.CrearProducto(ETipoDeProducto.Bebida, "CocaCola", 50, EUnidadDeMedida.Unidad, 50000M, proveedor1, ECategoriaConsumible.Bebida, EClasificacionBebida.Sin_Añcohol);
            IProducto cocaLight = _gestorDeProductos.CrearProducto(ETipoDeProducto.Bebida, "CocaLigh", 50, EUnidadDeMedida.Unidad, 50000M, proveedor1, ECategoriaConsumible.Bebida, EClasificacionBebida.Sin_Añcohol);
            IProducto cerveza = _gestorDeProductos.CrearProducto(ETipoDeProducto.Bebida, "Cerveza", 50, EUnidadDeMedida.Unidad, 50000M, proveedor1, ECategoriaConsumible.Bebida, EClasificacionBebida.Con_Alcohol);
            IProducto vino = _gestorDeProductos.CrearProducto(ETipoDeProducto.Bebida, "Vino", 50, EUnidadDeMedida.Unidad, 50000M, proveedor1, ECategoriaConsumible.Bebida, EClasificacionBebida.Con_Alcohol);
            IProducto jugo = _gestorDeProductos.CrearProducto(ETipoDeProducto.Bebida, "Jugo", 50, EUnidadDeMedida.Unidad, 50000M, proveedor1, ECategoriaConsumible.Bebida, EClasificacionBebida.Sin_Añcohol);


            //AGREGAMOS AL STOCK
            _gestorDeProductos.AgregarProductoAStock(aceite);
            _gestorDeProductos.AgregarProductoAStock(pollo);
            _gestorDeProductos.AgregarProductoAStock(tomate);
            _gestorDeProductos.AgregarProductoAStock(papa);
            _gestorDeProductos.AgregarProductoAStock(lechuga);
            _gestorDeProductos.AgregarProductoAStock(carne);
            _gestorDeProductos.AgregarProductoAStock(cocaCola);
            _gestorDeProductos.AgregarProductoAStock(cocaLight);
            _gestorDeProductos.AgregarProductoAStock(cerveza);
            _gestorDeProductos.AgregarProductoAStock(vino);
            _gestorDeProductos.AgregarProductoAStock(jugo);

            // TENIENDO STOCK DE PRODUCTOS PODEMOS CREAR EL PLATO

            //INSTANCIAMOS EL COCINERO
            IEmpleado cocinero = _gestorDeEmpleados.GetEmpleadoEnList("Gille");

            //INSTACIAMOS EL GESTOR MENU
            _gestorMenu = new GestorDeMenu((ICocinero)cocinero, _gestorDeProductos);

            //CREAMOS UN MENU
            _gestorMenu.CrearMenu("Almuerzo");


            // Selecciona ingredientes para el primer PLATO // AL MENOS DEBE HABER 2 INGREDIENTES PARA EL PLATO EN LA LISTA DE INGREDIENTES
            _gestorMenu.SelecionarIngredienteParaUnPlato("Carne", 1, EUnidadDeMedida.Kilo);
            _gestorMenu.SelecionarIngredienteParaUnPlato("Papa", 1, EUnidadDeMedida.Kilo);


            //Crear El Plato 1
            string nombreDelPLato1 = "Milanesa con papas";
            int tiempoDePreparacion = 30;
            EUnidadDeTiempo unidadDeTiempo = EUnidadDeTiempo.Segundos;

            IConsumible plato1 = _gestorMenu.CrearPlato(nombreDelPLato1, tiempoDePreparacion, unidadDeTiempo);


            //AGREGAMOS EL PLATO CREADO AL MENU: 'Almuerzo'
            _gestorMenu.AgregarPlatoAMenu("Almuerzo", plato1);





            // Selecciona ingredientes para el plato 2 // AL MENOS DEBE HABER 2 INGREDIENTES PARA EL PLATO EN LA LISTA DE INGREDIENTES
            _gestorMenu.SelecionarIngredienteParaUnPlato("Carne", 1, EUnidadDeMedida.Kilo);
            _gestorMenu.SelecionarIngredienteParaUnPlato("Tomate", 1, EUnidadDeMedida.Kilo);

            //Creamos el plato 2
            string nombreDelPlato2 = "Ensalada";
            int tiempoDePreparacion2 = 30;
            EUnidadDeTiempo unidadDeTiempo2 = EUnidadDeTiempo.Segundos;

            IConsumible plato2 = _gestorMenu.CrearPlato(nombreDelPlato2, tiempoDePreparacion2, unidadDeTiempo2);

            _gestorMenu.AgregarPlatoAMenu("Almuerzo", plato2);


        }






        public IGestorDeEmpleados GestorEmpleados
        {
            get { return _gestorDeEmpleados; }
            set {_gestorDeEmpleados = value;}
        }
        public IGestorDeProveedores GestorDeProveedores
        {
            get { return _gestorDeProveedores; }
            set { _gestorDeProveedores = value; }
        }

        public IGestorProductos GestorProductos
        {
            get { return _gestorDeProductos; }
            set { _gestorDeProductos = value;}
        }
        public IGestorMenu GestorMenu
        {
            get { return _gestorMenu; }
            set { _gestorMenu = value; }
        }
    }
}
