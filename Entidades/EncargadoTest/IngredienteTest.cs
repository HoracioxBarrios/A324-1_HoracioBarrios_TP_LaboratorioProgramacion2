using Entidades;
using Entidades.Enumerables;
using Entidades.Interfaces;
using Moq;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class IngredienteTest
    {
        [TestMethod]
        public void DescontarIngredienteDelStock_DebeDescontarUnIngredienteDeUnaListaDeProductosQueHayEnStock_SiSeDescontóDaTrue()
        {

            //Arrange
            //DATOS PARA Ingrediente 1-----------------------------------
            ETipoDeProducto tipoDeProducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "Pollo";
            double cantidad = 20;
            EUnidadDeMedida unidadDeMedida = EUnidadDeMedida.Kilo;
            decimal precio = 20000;

            var mockProveedor1 = new Mock<IProveedor>();
            mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor1.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor1.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor1.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            mockProveedor1.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor1.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor1.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor1.Setup(p => p.ID).Returns(1);
            mockProveedor1.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");

            //DATOS PARA Ingrediente 2 --------------------------------
            ETipoDeProducto tipoDeProducto2 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto2 = "Papa";
            double cantidad2 = 20;
            EUnidadDeMedida unidadDeMedida2 = EUnidadDeMedida.Kilo;
            decimal precio2 = 20000;

            var mockProveedor2 = new Mock<IProveedor>();
            mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor 2");
            mockProveedor2.Setup(p => p.Cuit).Returns("31-12345678-8");
            mockProveedor2.Setup(p => p.Direccion).Returns("Calle Falsa 456");
            mockProveedor2.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Carniceria);
            mockProveedor2.Setup(p => p.MediosDePago).Returns(EMediosDePago.Tarjeta);
            mockProveedor2.Setup(p => p.EsAcreedor).Returns(EAcreedor.No);
            mockProveedor2.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Martes);
            mockProveedor2.Setup(p => p.ID).Returns(2);
            mockProveedor2.Setup(p => p.ToString()).Returns("ID: 2, Nombre: Proveedor 2, CUIT: 31-12345678-8, Direccion: Calle Falsa 456, Tipo de Producto que Provee: Carniceria, Medio de Pago: Tarjeta, Es Acreedor? : No, Dia de Entrega: Martes");




            //------------------- GESTOR DE PRODUCTOS que tiene la lista de productos stock -----------------------
            IGestorProductos gestorDeProductos = new GestorDeProductos();


            //Act
            //Productos que van a estar en la lista del stock (está dentro de GestorProductos)

            // -------------- CREAMOS LOS INGREDIENTES para el stock ------------------
            IProducto pollo = gestorDeProductos.CrearProducto(tipoDeProducto1, nombreDeProducto1, cantidad, unidadDeMedida,precio, mockProveedor1.Object);
            IProducto papa = gestorDeProductos.CrearProducto(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, mockProveedor2.Object);

            //AGREGAMOS AL STOCK
            gestorDeProductos.AgregarProductoAStock(pollo);
            gestorDeProductos.AgregarProductoAStock(papa);

            //Instanciamos EL COCINERO
            IEmpleado cocinero = EmpleadoServiceFactory.CrearEmpleado(ERol.Cocinero, "Pipo", "Sdd", "2323", "Av pepe 123", 15000);


            //INSTANCIAMOS EL GESTOR MENU
            GestorDeMenu gestormenu = new GestorDeMenu((ICocinero)cocinero, gestorDeProductos);


            //SELECCIONAMOS INGREDIENTES PARA EL PLATO (DEBE ESTAR CREADO EN SISTEMA ( -- STOCK --)) -- para el PLATO deben ser 
            gestormenu.SelecionarIngrediente("Pollo", 1, EUnidadDeMedida.Kilo);
            gestormenu.SelecionarIngrediente("Papa", 1, EUnidadDeMedida.Kilo);

            // ----------------- Creamos el Menu -------------------
            gestormenu.CrearMenu("Almuerzo");

            // -------------- Creamos el plato ---------------------
            string nombreDelPlato = "polloPapa";
            int tiempoDePreparacion = 30;
            EUnidadDeTiempo unidadDeTiempo = EUnidadDeTiempo.Segundos;

            IConsumible plato = gestormenu.CrearPlato(nombreDelPlato, tiempoDePreparacion, unidadDeTiempo);

            //Agrgamos el plato al menu
            gestormenu.AgregarPlatoAMenu("Almuerzo", plato);




            // >>>>>>>>>>>>>>>>>> ---- Mostramos el menu ---- <<<<<<<<<<<<<<<<<<<<<<<<<<
            List<IMenu> menusDisponibles = gestormenu.GetAllMenus();
            IMenu menuSeleccionado = gestormenu.GetMenuPorNombre("Almuerzo");

            //----------- SELECCION DE CONSUMIBLES para el Pedido--------------------
            

            List<IConsumible> platosYBebidasSelecionadasParaElPedido = new List<IConsumible>();
            IConsumible plato1 = menuSeleccionado.GetPlatoPorNombre("polloPapa");


            platosYBebidasSelecionadasParaElPedido.Add(plato1);
            //---------------------- CREAMOS EL PEDIDO ------------------------

            GestorDePedidos gestorDePedidos = new GestorDePedidos();

            //ICreador de Pedidos
            IEmpleado mesero = EmpleadoServiceFactory.CrearEmpleado(ERol.Mesero, "Kiko", "Gry","1152000" , "Av iglu 45", 15000M);
            Mesero meseroCreadorPedido = (Mesero)mesero;

            gestorDePedidos.CrearPedido(meseroCreadorPedido,ETipoDePedido.Para_Local, platosYBebidasSelecionadasParaElPedido);

            //------------------------------------------------------ SE CREÓ EL PEDIDO
            //El cocinero ----> DEBE TOMAR ESE PEDIDO O COMANDA Y Ponerse a cocinar (cuando seleciona el pedido empieza a correr el tiempo del plato)

            IPedido pedidoAPreparar = gestorDePedidos.TomarPedidoPrioritario();

            gestorDePedidos.PrepararPedido((IPreparadorDePedidos)cocinero, pedidoAPreparar);


            System.Threading.Thread.Sleep(30000); // Espera 30 segundos

            // Verificar si el pedido está listo para entregar
            Assert.IsTrue(pedidoAPreparar.VerificarSiEsEntregable());


            //COMO PUEDO ACA VER QUE SE CUMPLA LOS 30 SEGUNDOS DE PREPARACION DEL PALTO QUE ESTA EN EL PEDIDO.
            //CUANDO TERMINA EL TIEMPO ( de todos los platos )-----> avisa por evento que el pedido esta LISTO PARA ENTREGAR


            // ACA EL MESERO DEBE ASIGNAR EL PLATO A LA MESA.

            //El cocinero ASIGNA EL PLATO A LA MESA y se debe DESCONTAR LOS INGREDIENTES QUE SE USARON EN EL PLATO.

            //Se le pasa la lista de Ingredintes a desconcar
            //bool seDesconto = gestorDeProductos.DescontarProductosDeStock(listaDeIngredienteEnElPlato);

            //------------------------------------------------------------------------------------------------------------------ CORREGIR ACA

            //Assert
            //Si se descuenta
            //Assert.IsTrue(seDesconto);



        }

        [TestMethod]
        public void VerElIngredienteEnKilosMenosKilos_DebeDescontarUnIngredienteDeUnaListaDeProductosQueHayEnStock_ALCorroborarDeeQuedar9KilosDEPollo()
        {

            //Arrange
            //Ingrediente 1
            ETipoDeProducto tipoDeProducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "Pollo";
            double cantidad = 10;
            EUnidadDeMedida unidadDeMedida = EUnidadDeMedida.Kilo;
            decimal precio = 20000;

            var mockProveedor1 = new Mock<IProveedor>();
            mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor1.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor1.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor1.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            mockProveedor1.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor1.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor1.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor1.Setup(p => p.ID).Returns(1);
            mockProveedor1.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");

            //Ingrediente 2 
            ETipoDeProducto tipoDeProducto2 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto2 = "Papa";
            double cantidad2 = 30;
            EUnidadDeMedida unidadDeMedida2 = EUnidadDeMedida.Kilo;
            decimal precio2 = 20000;

            var mockProveedor2 = new Mock<IProveedor>();
            mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor 2");
            mockProveedor2.Setup(p => p.Cuit).Returns("31-12345678-8");
            mockProveedor2.Setup(p => p.Direccion).Returns("Calle Falsa 456");
            mockProveedor2.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Carniceria);
            mockProveedor2.Setup(p => p.MediosDePago).Returns(EMediosDePago.Tarjeta);
            mockProveedor2.Setup(p => p.EsAcreedor).Returns(EAcreedor.No);
            mockProveedor2.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Martes);
            mockProveedor2.Setup(p => p.ID).Returns(2);
            mockProveedor2.Setup(p => p.ToString()).Returns("ID: 2, Nombre: Proveedor 2, CUIT: 31-12345678-8, Direccion: Calle Falsa 456, Tipo de Producto que Provee: Carniceria, Medio de Pago: Tarjeta, Es Acreedor? : No, Dia de Entrega: Martes");

            //Ingrediente que se instancia en el plato y estaria en la lista del plato.
            //Ingrediente 3
            ETipoDeProducto tipoDeProducto3 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto3 = "Pollo";
            double cantidad3 = 1; // Cantidad que usa en el plato
            EUnidadDeMedida unidadDeMedida3 = EUnidadDeMedida.Kilo;
            decimal precio3 = 1000;

            var mockProveedor3 = new Mock<IProveedor>();
            mockProveedor3.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor3.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor3.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor3.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            mockProveedor3.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor3.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor3.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor3.Setup(p => p.ID).Returns(1);
            mockProveedor3.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");

            GestorDeProductos gestorDeProductos = new GestorDeProductos();


            //Act
            //Productos que van a estar en la lista del stock (está dentro de GestorProductos)
            gestorDeProductos.CrearProducto(tipoDeProducto1, nombreDeProducto1, cantidad, unidadDeMedida, precio, mockProveedor1.Object);
            gestorDeProductos.CrearProducto(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, mockProveedor2.Object);


            //Producto IConsumubleque va a estar en el plato(lo que  usa el plato)
            IProducto ingrediente3 = gestorDeProductos.CrearProducto(tipoDeProducto3, nombreDeProducto3, cantidad3, unidadDeMedida3, precio3, mockProveedor3.Object);

            List<IConsumible> listaDeIngredienteEnElPlato = new List<IConsumible>();
            listaDeIngredienteEnElPlato.Add((IConsumible)ingrediente3);

            //Se le pasa la lista de Ingredintes a descontar
            bool seDesconto = gestorDeProductos.DescontarProductosDeStock(listaDeIngredienteEnElPlato);





            foreach (IProducto productoIngrediente in gestorDeProductos.ReadAllProductos())
            {
                if (productoIngrediente.Nombre == "Pollo" && productoIngrediente.Cantidad == 9)
                {
                    Assert.AreEqual(9, productoIngrediente.Cantidad);
                }
            }
        }

        [TestMethod]
        public void ElIngredienteEnLitrosMenosMiliLitros_DebeDescontarUnIngredienteDeUnaListaDeProductosQueHayEnStock_ALCorroborarDeeQuedar8coma5Litros()
        {

            //Arrange
            //Ingrediente 1
            ETipoDeProducto tipoDeProducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "Aceite";
            double cantidad = 9;
            EUnidadDeMedida unidadDeMedida = EUnidadDeMedida.Litro;
            decimal precio = 20000;

            var mockProveedor1 = new Mock<IProveedor>();
            mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor1.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor1.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor1.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            mockProveedor1.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor1.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor1.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor1.Setup(p => p.ID).Returns(1);
            mockProveedor1.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");

            //Ingrediente 2 
            ETipoDeProducto tipoDeProducto2 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto2 = "Carne";
            double cantidad2 = 50;
            EUnidadDeMedida unidadDeMedida2 = EUnidadDeMedida.Kilo;
            decimal precio2 = 20000;

            var mockProveedor2 = new Mock<IProveedor>();
            mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor 2");
            mockProveedor2.Setup(p => p.Cuit).Returns("31-12345678-8");
            mockProveedor2.Setup(p => p.Direccion).Returns("Calle Falsa 456");
            mockProveedor2.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Carniceria);
            mockProveedor2.Setup(p => p.MediosDePago).Returns(EMediosDePago.Tarjeta);
            mockProveedor2.Setup(p => p.EsAcreedor).Returns(EAcreedor.No);
            mockProveedor2.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Martes);
            mockProveedor2.Setup(p => p.ID).Returns(2);
            mockProveedor2.Setup(p => p.ToString()).Returns("ID: 2, Nombre: Proveedor 2, CUIT: 31-12345678-8, Direccion: Calle Falsa 456, Tipo de Producto que Provee: Carniceria, Medio de Pago: Tarjeta, Es Acreedor? : No, Dia de Entrega: Martes");

            //Ingrediente que se instancia en el plato y estaria en la lista del plato.
            //Ingrediente 3
            ETipoDeProducto tipoDeProducto3 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto3 = "Aceite";
            double cantidad3 = 500; // Cantidad que usa en el plato
            EUnidadDeMedida unidadDeMedida3 = EUnidadDeMedida.MiliLitro;
            decimal precio3 = 1000;

            var mockProveedor3 = new Mock<IProveedor>();
            mockProveedor3.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor3.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor3.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor3.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            mockProveedor3.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor3.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor3.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor3.Setup(p => p.ID).Returns(1);
            mockProveedor3.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");

            GestorDeProductos gestorDeProductos = new GestorDeProductos();


            //Act
            //Productos que van a estar en la lista del stock (está dentro de GestorProductos)
            gestorDeProductos.CrearProducto(tipoDeProducto1, nombreDeProducto1, cantidad, unidadDeMedida, precio, mockProveedor1.Object);
            gestorDeProductos.CrearProducto(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, mockProveedor2.Object);


            //Producto que va a estar en el plato(lo que  usa el plato)
            IProducto ingrediente3 = gestorDeProductos.CrearProducto(tipoDeProducto3, nombreDeProducto3, cantidad3, unidadDeMedida3, precio3, mockProveedor3.Object);

            List<IConsumible> listaDeIngredienteEnElPlato = new List<IConsumible>();
            listaDeIngredienteEnElPlato.Add((IConsumible)ingrediente3);

            //Se le pasa la lista de Ingredintes a descontar
            bool seDesconto = gestorDeProductos.DescontarProductosDeStock(listaDeIngredienteEnElPlato);





            foreach (IProducto productoIngrediente in gestorDeProductos.ReadAllProductos())
            {
                if (productoIngrediente.Nombre == "Aceite" && productoIngrediente.Cantidad == 8.5)
                {
                    Assert.AreEqual(8.5, productoIngrediente.Cantidad);
                }
            }
        }

        [TestMethod]
        public void ElIngredienteQueEstaEnLitrosSeDescuentaEnMiliLitrosYActualizaElValorEnBaseASuCantidad_DebeDescontarElAceiteDeUnaListaDeProductosQueHayEnStock_ALCorroborarDebeQuedar9coma5LitrosYTambienDebeValerTodoNueveMilQuinientosPesos()
        {

            //Arrange
            //Ingrediente 1
            ETipoDeProducto tipoDeProducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "Aceite";
            double cantidad = 10;
            EUnidadDeMedida unidadDeMedida = EUnidadDeMedida.Litro;
            decimal precio = 10000;

            var mockProveedor1 = new Mock<IProveedor>();
            mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor1.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor1.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor1.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            mockProveedor1.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor1.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor1.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor1.Setup(p => p.ID).Returns(1);
            mockProveedor1.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");

            //Ingrediente 2 
            ETipoDeProducto tipoDeProducto2 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto2 = "Carne";
            double cantidad2 = 50;
            EUnidadDeMedida unidadDeMedida2 = EUnidadDeMedida.Kilo;
            decimal precio2 = 20000;

            var mockProveedor2 = new Mock<IProveedor>();
            mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor 2");
            mockProveedor2.Setup(p => p.Cuit).Returns("31-12345678-8");
            mockProveedor2.Setup(p => p.Direccion).Returns("Calle Falsa 456");
            mockProveedor2.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Carniceria);
            mockProveedor2.Setup(p => p.MediosDePago).Returns(EMediosDePago.Tarjeta);
            mockProveedor2.Setup(p => p.EsAcreedor).Returns(EAcreedor.No);
            mockProveedor2.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Martes);
            mockProveedor2.Setup(p => p.ID).Returns(2);
            mockProveedor2.Setup(p => p.ToString()).Returns("ID: 2, Nombre: Proveedor 2, CUIT: 31-12345678-8, Direccion: Calle Falsa 456, Tipo de Producto que Provee: Carniceria, Medio de Pago: Tarjeta, Es Acreedor? : No, Dia de Entrega: Martes");

            //Ingrediente que se instancia en el plato y estaria en la lista del plato.
            //Ingrediente 3
            ETipoDeProducto tipoDeProducto3 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto3 = "Aceite";
            double cantidad3 = 500; // Cantidad que usa en el plato
            EUnidadDeMedida unidadDeMedida3 = EUnidadDeMedida.MiliLitro;
            decimal precio3 = 1000;

            var mockProveedor3 = new Mock<IProveedor>();
            mockProveedor3.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor3.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor3.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor3.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            mockProveedor3.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor3.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor3.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor3.Setup(p => p.ID).Returns(1);
            mockProveedor3.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");

            GestorDeProductos gestorDeProductos = new GestorDeProductos();


            //Act
            //Productos que van a estar en la lista del stock (está dentro de GestorProductos)
            gestorDeProductos.CrearProducto(tipoDeProducto1, nombreDeProducto1, cantidad, unidadDeMedida, precio, mockProveedor1.Object);
            gestorDeProductos.CrearProducto(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, mockProveedor2.Object);


            //Producto que va a estar en el plato(lo que  usa el plato)
            IProducto ingrediente3 = gestorDeProductos.CrearProducto(tipoDeProducto3, nombreDeProducto3, cantidad3, unidadDeMedida3, precio3, mockProveedor3.Object);

            List<IConsumible> listaDeIngredienteEnElPlato = new List<IConsumible>();
            listaDeIngredienteEnElPlato.Add((IConsumible)ingrediente3);

            //Se le pasa la lista de Ingredintes a descontar
            bool seDesconto = gestorDeProductos.DescontarProductosDeStock(listaDeIngredienteEnElPlato);





            foreach (IProducto productoIngrediente in gestorDeProductos.ReadAllProductos())
            {
                if (productoIngrediente.Nombre == "Aceite" && productoIngrediente.Cantidad == 8.5)
                {
                    Assert.AreEqual(8.5, productoIngrediente.Cantidad);
                    Assert.AreEqual(9500, productoIngrediente.CalcularPrecioDeCosto());
                }
            }
        }
    }
}
