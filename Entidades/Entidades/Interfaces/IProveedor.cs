using Entidades.Enumerables;

namespace Entidades.Interfaces
{
    public interface IProveedor
    {
        string Nombre { get;set;}
        string Cuit { get;set; }
        string Direccion { get;set; }
        ETipoDeProduto TipoDeProductoQueProvee { get;set; }        
        EMediosDePago MediosDePago { get;set; }
        EDiaDeLaSemana DiaDeEntrega { get;set; }

    }
}
