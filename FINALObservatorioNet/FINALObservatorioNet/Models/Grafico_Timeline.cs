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
    
    public partial class Grafico_Timeline
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Grafico_Timeline()
        {
            this.Padre_Grafico_Timeline = new HashSet<Padre_Grafico_Timeline>();
        }
    
        public int IdGrafico { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Padre_Grafico_Timeline> Padre_Grafico_Timeline { get; set; }
    }
}
