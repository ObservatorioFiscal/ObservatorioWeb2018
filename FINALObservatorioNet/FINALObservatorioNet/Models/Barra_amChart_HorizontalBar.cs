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
    
    public partial class Barra_amChart_HorizontalBar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Barra_amChart_HorizontalBar()
        {
            this.Tabla_amChart_HorizontalBar = new HashSet<Tabla_amChart_HorizontalBar>();
        }
    
        public int IdCuadrado { get; set; }
        public string Titulo { get; set; }
        public Nullable<double> Size { get; set; }
        public string Color { get; set; }
        public Nullable<int> IdGraficoFK { get; set; }
        public Nullable<int> valor { get; set; }
        public string Idregion { get; set; }
        public Nullable<int> ValorPercapita { get; set; }
        public string tooltip { get; set; }
    
        public virtual Grafico_amChart_HorizontalBar Grafico_amChart_HorizontalBar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tabla_amChart_HorizontalBar> Tabla_amChart_HorizontalBar { get; set; }
    }
}