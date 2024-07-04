using Negocio;
using System;
using System.Collections.Generic;
using Entidades.Enumerables;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Interfaces;

namespace Test
{
    [TestClass]
    public class GestorDeProveedoresTest
    {

        [TestMethod]
        public void TestProveedores()
        {
            GestorDeProveedores gestorProveedor = new GestorDeProveedores();

            gestorProveedor.CrearProveedor("E Almacen", "203120", "calle 88", ETipoDeProducto.Almacen, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Lunes);
            gestorProveedor.CrearProveedor("Best Carnes", "355019", "calle 66", ETipoDeProducto.Carniceria, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Lunes);
            gestorProveedor.CrearProveedor("Fresh Verduleria", "203155", "av asd 5", ETipoDeProducto.Verduleria, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Martes);

            gestorProveedor.CrearProveedor("Frio Almacen", "203177", "calle 1", ETipoDeProducto.Almacen, EMediosDePago.Contado, EAcreedor.No, EDiaDeLaSemana.Miercoles);
            gestorProveedor.CrearProveedor("Asa Verduras", "204444", "av gol 10", ETipoDeProducto.Verduleria, EMediosDePago.Contado, EAcreedor.No, EDiaDeLaSemana.viernes);


            List<IProveedor> proveedors = gestorProveedor.ObtenerProveedores();

            Assert.AreEqual(5, proveedors.Count);


        }
    }
}
