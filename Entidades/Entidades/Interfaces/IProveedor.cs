using Entidades.Enumerables;

namespace Entidades.Interfaces
{
    public interface IProveedor
    {
        string Nombre { get;set;}
        string Cuit { get;set; }
        string Direccion { get;set; }
        ETipoDeProductoProveido TipoDeProducto { get;set; }        
        EMediosDePago MediosDePago { get;set; }
        EAcreedor EsAcreedor { get;set;}
        EDiaDeLaSemana DiaDeEntrega { get;set; }
        int ID { get; }

    }
}
