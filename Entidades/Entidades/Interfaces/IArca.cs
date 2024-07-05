namespace Entidades.Interfaces
{
    public interface IArca
    {
        void AgregarDinero(decimal montoAAgregar);
        decimal TomarDinero(decimal montoNecesario);
        decimal ObtenerMontoDisponible();


    }
}