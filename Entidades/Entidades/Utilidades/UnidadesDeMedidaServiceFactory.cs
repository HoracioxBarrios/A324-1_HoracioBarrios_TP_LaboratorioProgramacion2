using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using Entidades.Unidades_de_Medida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public static class UnidadesDeMedidaServiceFactory 
    {


        public static ITipoUnidadDeMedida CrearUnidadDeMedida(EUnidadDeMedida unidadDeMedida ,double valor) 
        {
            if(unidadDeMedida < 0)
            {
                throw new ValorNegativoEnTipoDeUnidadDeMedidaException("El valor en el tipo de unidad de medida es Incorrecto");
            }

            switch(unidadDeMedida)
            {
                case EUnidadDeMedida.Kilo:
                    return new Kilo(valor);
                case EUnidadDeMedida.Gramo:
                    return new Gramo(valor);                    
                case EUnidadDeMedida.Litro:
                    return new Litro(valor);              
                case EUnidadDeMedida.MiliLitro:
                    return new MiliLitro(valor);
                case EUnidadDeMedida.Unidad:
                    return new Unidad(valor);
                default: 
                    throw new UnidadDeMedidaDesconocidaException("Unidad de Medida Desconocida");
              
            }
            
        
        }


    }
}
