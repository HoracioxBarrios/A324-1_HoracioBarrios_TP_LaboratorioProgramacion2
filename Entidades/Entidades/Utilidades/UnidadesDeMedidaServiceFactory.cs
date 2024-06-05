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


        public static ITipoUnidadDeMedida CrearUnidadDeMedida(EUnidadMedida unidadDeMedida ,double valor) 
        {
            if(unidadDeMedida <= 0)
            {
                throw new EmpleadoDatosException("El valor es Incorrecto");
            }

            switch(unidadDeMedida)
            {
                case EUnidadMedida.Kilo:
                    return new Kilo(valor);
                case EUnidadMedida.Gramo:
                    return new Gramo(valor);                    
                case EUnidadMedida.Litro:
                    return new Litro(valor);              
                case EUnidadMedida.MiliLitro:
                    return new MiliLitro(valor);
                case EUnidadMedida.Unidad:
                    return new Unidad(valor);
                default: 
                    throw new UnidadDeMedidaDesconocidaException("Unidad de Medida Desconocida");
              
            }
            
        
        }


    }
}
