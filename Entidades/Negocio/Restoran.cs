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
    /// <summary>
    /// Class Restoran
    /// 
    /// </summary>
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
            bool seCreoEmpleado1 = _gestorDeEmpleados.CrearEmpleado(ERol.Cocinero, "Gille", "Rel", "1150654", "Av los pericos 5142", 20000m);
            bool seCreoEmpleado2 = _gestorDeEmpleados.CrearEmpleado(ERol.Encargado, "Mar", "Ruy", "1156202", "Principio Solid 5", 30000m);
            bool seCreoEmpleado3 = _gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Jun", "Fre", "117778", "Calle 88", 15000m);
            bool seCreoEmpleado4 = _gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Kler", "Dry", "117563", "Calle 41", 15000m);
            bool seCreoEmpleado5 = _gestorDeEmpleados.CrearEmpleado(ERol.Delivery, "Cris", "Lol", "115632", "Calle 56", 10000m);

            

            _gestorDeProveedores = new GestorDeProveedores();

            //----- Instancio los PROVEEDORES ---------
            _gestorDeProveedores.CrearProveedor("Aser S.A", "452", "Av Los Macacos 35", ETipoDeProducto.Almacen, EMediosDePago.Transferencia, EAcreedor.No, EDiaDeLaSemana.Lunes);
            _gestorDeProveedores.CrearProveedor("Carnes Argentinas SRL", "126", "Av Sin Agua 1500", ETipoDeProducto.Carniceria, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Miercoles);
            _gestorDeProveedores.CrearProveedor("Almacenes S.A", "598", "Av Sin Tierra 1500", ETipoDeProducto.Verduleria, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Jueves);
            _gestorDeProveedores.CrearProveedor("Verduras S.A", "5", "Av Sin Verduras 200", ETipoDeProducto.Verduleria, EMediosDePago.Contado, EAcreedor.No, EDiaDeLaSemana.Lunes);


            _gestorDeProductos = new GestorDeProductos();

            //------- instancio PRODUCTOS (INGREDIENTES) ---------

            //Proveedor para Ingrediente 1 -ALMACEN
            IProveedor proveedor1 = _gestorDeProveedores.GetProveedor(1);
            //Proveedor para Ingrediente 2 -CARNICERIA
            IProveedor proveedor2 = _gestorDeProveedores.GetProveedor(2);
            //Proveedor para Ingrediente 3  -VERDULERIA
            IProveedor proveedor3 = _gestorDeProveedores.GetProveedor(3);
            //proveedor para Ingrediente 4 -VERDULERIA
            IProveedor proveedor4 = _gestorDeProveedores.GetProveedor(4);

            _gestorDeProductos.CrearProductoParaListaDeStock(ETipoDeProducto.Ingrediente, "Aceite", 2, EUnidadDeMedida.Litro, 1000M, proveedor1);
            _gestorDeProductos.CrearProductoParaListaDeStock(ETipoDeProducto.Ingrediente, "Pollo", 6, EUnidadDeMedida.Kilo, 6000M, proveedor2) ;
            _gestorDeProductos.CrearProductoParaListaDeStock(ETipoDeProducto.Ingrediente, "Tomate", 2, EUnidadDeMedida.Kilo , 2000M, proveedor3);
            _gestorDeProductos.CrearProductoParaListaDeStock(ETipoDeProducto.Ingrediente, "Papa", 6, EUnidadDeMedida.Kilo, 6000M, proveedor4);
            _gestorDeProductos.CrearProductoParaListaDeStock(ETipoDeProducto.Ingrediente, "Lechuga",2, EUnidadDeMedida.Kilo, 2000M, proveedor4);
            _gestorDeProductos.CrearProductoParaListaDeStock(ETipoDeProducto.Ingrediente, "Carne", 10, EUnidadDeMedida.Kilo, 10000M, proveedor2);


            // ------- Instancio PRODUCTOS (BEBIDAS) ----------

            _gestorDeProductos.CrearProductoParaListaDeStock(ETipoDeProducto.Bebida, "CocaCola", 50, EUnidadDeMedida.Unidad, 50000M, proveedor1, ECategoriaConsumible.Bebida, EClasificacionBebida.Sin_Añcohol);
            _gestorDeProductos.CrearProductoParaListaDeStock(ETipoDeProducto.Bebida, "CocaLigh", 50, EUnidadDeMedida.Unidad, 50000M, proveedor1, ECategoriaConsumible.Bebida, EClasificacionBebida.Sin_Añcohol);
            _gestorDeProductos.CrearProductoParaListaDeStock(ETipoDeProducto.Bebida, "Cerveza", 50, EUnidadDeMedida.Unidad, 50000M, proveedor1, ECategoriaConsumible.Bebida, EClasificacionBebida.Con_Alcohol);
            _gestorDeProductos.CrearProductoParaListaDeStock(ETipoDeProducto.Bebida, "Vino", 50, EUnidadDeMedida.Unidad, 50000M, proveedor1, ECategoriaConsumible.Bebida, EClasificacionBebida.Con_Alcohol);
            _gestorDeProductos.CrearProductoParaListaDeStock(ETipoDeProducto.Bebida, "Jugo", 50, EUnidadDeMedida.Unidad, 50000M, proveedor1, ECategoriaConsumible.Bebida, EClasificacionBebida.Sin_Añcohol);


            //-------- Instancio INGREDIENTES PARA CREAR LOS PLATOS (ICONSUMIBLE)
            // Ingredientes para el Plato 1
            List<IConsumible> listaDeIngredienteParaElPlato1 = new List<IConsumible>();
            IConsumible ingrediente1ParaPlato1 = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "Carne", 1, EUnidadDeMedida.Kilo, 1000M, proveedor2);
            IConsumible ingrediente2ParaPlato1 = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "Papa", 1, EUnidadDeMedida.Kilo, 1000M, proveedor4);
            listaDeIngredienteParaElPlato1.Add(ingrediente2ParaPlato1);
            listaDeIngredienteParaElPlato1.Add(ingrediente1ParaPlato1);

            // Ingredientes para el Plato 2
            List<IConsumible> listaDeIngredienteParaElPlato2 = new List<IConsumible>();
            IConsumible ingrediente1ParaPlato2 = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "Carne", 1, EUnidadDeMedida.Kilo, 1000M, proveedor2);
            IConsumible ingrediente2ParaPlato2 = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "Tomate", 1, EUnidadDeMedida.Kilo, 1000M, proveedor3);
            listaDeIngredienteParaElPlato2.Add(ingrediente1ParaPlato2);
            listaDeIngredienteParaElPlato2.Add(ingrediente2ParaPlato2);

            // Para el PLATO (Necesitamos el Cocinero), Crea El PLATO, LO AGREGA A UN MENU Y ESTE SE GUARDA EN UNA LISTA DE MENUES EN EL GESTOR
            IEmpleado cocinero = _gestorDeEmpleados.GetEmpleadoEnList("Gille");
            _gestorMenu = new GestorDeMenu((ICocinero)cocinero);

            _gestorMenu.CrearMenu("Almuerzo");

            // Selecciona ingredientes para el primer plato
            _gestorMenu.SeleccionarIngredienteParaElPlato(listaDeIngredienteParaElPlato1, "Carne", 1, EUnidadDeMedida.Kilo);
            _gestorMenu.SeleccionarIngredienteParaElPlato(listaDeIngredienteParaElPlato1, "Papa", 1, EUnidadDeMedida.Kilo);
            _gestorMenu.AgregarPlatoAMenu("Almuerzo", "Milanesa con papas");





            // Selecciona ingredientes para el segundo plato // AL MENOS DEBE HABER 2 INGREDIENTES PARA EL PLATO EN LA LISTA DE INGREDIENTES
            _gestorMenu.SeleccionarIngredienteParaElPlato(listaDeIngredienteParaElPlato2, "Carne", 1, EUnidadDeMedida.Kilo);
            _gestorMenu.SeleccionarIngredienteParaElPlato(listaDeIngredienteParaElPlato2, "Tomate", 1, EUnidadDeMedida.Kilo);
            _gestorMenu.AgregarPlatoAMenu("Almuerzo", "Ensalada");


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
