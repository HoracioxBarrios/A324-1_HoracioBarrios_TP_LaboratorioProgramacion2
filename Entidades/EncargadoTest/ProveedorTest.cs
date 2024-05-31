using Entidades;
using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadTest
{
    [TestClass]
    public class ProveedorTest
    {

        [TestMethod]
        public void verificarSiInstanciaProveedor_DebeDarTrue()
        {
            string nombre = "Asa";
            string cuit = "25565";
            string direccion = "Av los pikachus";
            ETipoDeProduto tipoDeProducto = ETipoDeProduto.Carniceria;
            EMediosDePago medioDePago = EMediosDePago.Contado;
            EDiaDeLaSemana diaDeEntrega = EDiaDeLaSemana.Lunes;

            Proveedor proveedor = new Proveedor(nombre, cuit, direccion, tipoDeProducto,medioDePago,diaDeEntrega);

            Assert.IsNotNull(proveedor);
        }
    }
}
