//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FINALObservatorioNet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Datos_googleChart_Area
    {
        public int IdDato { get; set; }
        public Nullable<double> ValorPorcentaje { get; set; }
        public Nullable<long> ValorPeso { get; set; }
        public string Ano { get; set; }
        public Nullable<int> IdLineasFK { get; set; }
    
        public virtual Area_googleChart_Area Area_googleChart_Area { get; set; }
    }
}