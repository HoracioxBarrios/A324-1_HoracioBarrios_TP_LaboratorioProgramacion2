using Entidades.Enumerables;
using Entidades.Interfaces;
using Moq;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEntidades
{
    [TestClass]
    public class BebidaTest
    {
        [TestMethod]
        public void TestBebidas_VerificamosSiSeCreanYSeAñadenALStock_AlAgregarseAlStock_SiSeAgreganseDebeContarLosDosYSiEsAsiEsTaBien()
        {
            //Arrange
            //PROVEEDOR MOCK
            var mockProveedor4 = new Mock<IProveedor>();
            mockProveedor4.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor4.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor4.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor4.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            mockProveedor4.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor4.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor4.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor4.Setup(p => p.ID).Returns(1);
            mockProveedor4.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");
            //IPRODUCTO BEBIDA 1
            ETipoDeProducto tipoDeProductoBebida1 = ETipoDeProducto.Bebida;
            string nombreBebida1 = "CocaCola";
            double cantidadBebida1 = 20;
            EUnidadMedida eUnidadDeMedidaBebida1 = EUnidadMedida.Unidad;
            decimal precioBebida1 = 20000;
            IProveedor proveedorBebida1 = mockProveedor4.Object;
            ECategoriaConsumible categoriaConsumibleBebida1 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida1 = EClasificacionBebida.Sin_Añcohol;
           
            // BBEBIDA 2
            ETipoDeProducto tipoDeProductoBebida2 = ETipoDeProducto.Bebida;
            string nombreBebida2 = "Cerveza QUilmes";
            double cantidadBebida2 = 10;
            EUnidadMedida eUnidadDeMedidaBebida2 = EUnidadMedida.Unidad;
            decimal precioBebida2 = 10000;
            IProveedor proveedorBebida2 = mockProveedor4.Object;
            ECategoriaConsumible categoriaConsumibleBebida2 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida2 = EClasificacionBebida.Con_Alcohol;


            //GESTOR
            GestorDeProductos gestorDeProductos = new GestorDeProductos();
          
            //Act
            gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProductoBebida1, nombreBebida1, cantidadBebida1, eUnidadDeMedidaBebida1, precioBebida1, proveedorBebida1, categoriaConsumibleBebida1, clasificacionBebida1);
            gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProductoBebida2, nombreBebida2, cantidadBebida2, eUnidadDeMedidaBebida2, precioBebida2, proveedorBebida2, categoriaConsumibleBebida2, clasificacionBebida2);

            //Assert
            int contador = 0;
            foreach (var producto in gestorDeProductos.GetProductos())
            {                
                if (producto != null)
                {
                    if (producto.Nombre == "CocaCola")
                    {
                        Assert.AreEqual(20, producto.Cantidad);
                        contador++;
                    }
                    if (producto.Nombre == "Cerveza QUilmes")
                    {
                        Assert.AreEqual(10, producto.Cantidad);
                        contador++;
                    }
                    
                }
            }
            Assert.AreEqual(2, contador);
            
        }


        public void TestBebidas_VerificamosSiSeCreanYSeAñadenALStock_SeDebePoderSumarElValorDeLosProductosQueEstanEnStock_SiLoHaceDebeDar30000()
        {
            //Arrange
            //PROVEEDOR MOCK
            var mockProveedor4 = new Mock<IProveedor>();
            mockProveedor4.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor4.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor4.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor4.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            mockProveedor4.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor4.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor4.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor4.Setup(p => p.ID).Returns(1);
            mockProveedor4.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");
            //IPRODUCTO BEBIDA 1
            ETipoDeProducto tipoDeProductoBebida1 = ETipoDeProducto.Bebida;
            string nombreBebida1 = "CocaCola";
            double cantidadBebida1 = 20;
            EUnidadMedida eUnidadDeMedidaBebida1 = EUnidadMedida.Unidad;
            decimal precioBebida1 = 20000;
            IProveedor proveedorBebida1 = mockProveedor4.Object;
            ECategoriaConsumible categoriaConsumibleBebida1 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida1 = EClasificacionBebida.Sin_Añcohol;

            // BBEBIDA 2
            ETipoDeProducto tipoDeProductoBebida2 = ETipoDeProducto.Bebida;
            string nombreBebida2 = "Cerveza QUilmes";
            double cantidadBebida2 = 10;
            EUnidadMedida eUnidadDeMedidaBebida2 = EUnidadMedida.Unidad;
            decimal precioBebida2 = 10000;
            IProveedor proveedorBebida2 = mockProveedor4.Object;
            ECategoriaConsumible categoriaConsumibleBebida2 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida2 = EClasificacionBebida.Con_Alcohol;


            //GESTOR
            GestorDeProductos gestorDeProductos = new GestorDeProductos();

            //Act
            gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProductoBebida1, nombreBebida1, cantidadBebida1, eUnidadDeMedidaBebida1, precioBebida1, proveedorBebida1, categoriaConsumibleBebida1, clasificacionBebida1);
            gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProductoBebida2, nombreBebida2, cantidadBebida2, eUnidadDeMedidaBebida2, precioBebida2, proveedorBebida2, categoriaConsumibleBebida2, clasificacionBebida2);

            //Assert


            Assert.AreEqual(30000, gestorDeProductos.CalcularPrecio());

        }
    }
}
