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
    
    public partial class Lineas_googleChart_Line
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lineas_googleChart_Line()
        {
            this.Datos_googleChart_Line = new HashSet<Datos_googleChart_Line>();
            this.Grafico_googleChart_Area = new HashSet<Grafico_googleChart_Area>();
        }
    
        public int IdLinea { get; set; }
        public string Titulo { get; set; }
        public string Color { get; set; }
        public Nullable<int> IdGraficoFK { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Datos_googleChart_Line> Datos_googleChart_Line { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grafico_googleChart_Area> Grafico_googleChart_Area { get; set; }
        public virtual Grafico_googleChart_Line Grafico_googleChart_Line { get; set; }
    }
}