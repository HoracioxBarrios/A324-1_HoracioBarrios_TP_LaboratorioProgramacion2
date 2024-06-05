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

namespace TestEntidades
{
    [TestClass]
    public class IngredienteTest
    {
        [TestMethod]
        public void DescontarIngrediente_DebeDescontarUnIngredienteDeUnaListaDeIngrediente()
        {

            //Arrange
            //Ingrediente 1
            ETipoDeProducto tipoDeProducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "Pollo";
            double cantidad = 20;
            EUnidadMedida unidadDeMedida = EUnidadMedida.Kilo;
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
            string nombreDeProducto2 = "Pollo";
            double cantidad2 = 20;
            EUnidadMedida unidadDeMedida2 = EUnidadMedida.Kilo;
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
            EUnidadMedida unidadDeMedida3 = EUnidadMedida.Kilo;
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



            //Act
            //Productos que van a estar en la lista del stock
            IProducto ingrediente1 = ProductoServiceFactory.CrearProducto(tipoDeProducto1, nombreDeProducto1, cantidad, unidadDeMedida,precio, mockProveedor1.Object);
            IProducto ingrediente2 = ProductoServiceFactory.CrearProducto(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, mockProveedor2.Object);
            ingrediente1.Id = 1;
            ingrediente2.Id = 2;


            List<IProducto> listaDeProductosIngredientes = new List<IProducto>();
            listaDeProductosIngredientes.Add(ingrediente1);
            listaDeProductosIngredientes.Add(ingrediente2);


            //Producto que va a estar en el plato(lo que  usa el plato)

            IProducto ingrediente3 = ProductoServiceFactory.CrearProducto(tipoDeProducto3, nombreDeProducto3, cantidad3, unidadDeMedida3, precio3, mockProveedor3.Object);
            ingrediente3.Id = 1;
            List <IProducto> listaDeIngredienteEnElPlato = new List<IProducto>();
            listaDeIngredienteEnElPlato.Add(ingrediente3);



            //Esto seria un metodo que recie la lista de stock, y recibe la lista del plato (machea y descuenta):
            foreach (IProducto producto in listaDeProductosIngredientes)
            {
                if (producto is Ingrediente ingrediente)
                {
                    foreach (IProducto productoADescontar in listaDeIngredienteEnElPlato)
                    {
                        if (productoADescontar is Ingrediente ingredienteADescontar && ingrediente.Id == ingredienteADescontar.Id)
                        {
                            Ingrediente nuevoIngrediente = ingrediente - ingredienteADescontar;

                            // Encuentra el índice del ingrediente en la lista
                            int index = listaDeProductosIngredientes.FindIndex(p => p.Id == ingrediente.Id);

                            // Reemplaza el ingrediente actual con el nuevo ingrediente
                            listaDeProductosIngredientes[index] = nuevoIngrediente;
                        }
                    }
                }
            }





            // una vez descontado debemos ver la lista del stock si hay cambios:
            // Assert
            // Verificamos que el ingrediente de la ID 1 haya cambiado su cantidad a 19
            //var ingredienteDescontado = listaDeProductosIngredientes.OfType<Ingrediente>().FirstOrDefault(i => i.Id == 1);
            Ingrediente ingredienteDescontado = null;

            foreach (IProducto producto in listaDeProductosIngredientes)
            {
                if (producto is Ingrediente ingrediente && ingrediente.Id == 1)
                {
                    ingredienteDescontado = ingrediente;
                    break;
                }
            }

            // Verificar si las IDs de los productos 1 y 3 son las mismas
            Assert.AreEqual(ingrediente1.Id, ingrediente3.Id);

            //Si no es null
            Assert.IsNotNull(ingredienteDescontado);
            //Si se descuenta
            Assert.AreEqual(19, ingredienteDescontado.Cantidad);



        }

    }
}
