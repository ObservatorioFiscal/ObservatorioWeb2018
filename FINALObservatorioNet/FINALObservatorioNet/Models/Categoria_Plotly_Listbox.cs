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
    
    public partial class Categoria_Plotly_Listbox
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categoria_Plotly_Listbox()
        {
            this.SubCategoria_Plotly_Listbox = new HashSet<SubCategoria_Plotly_Listbox>();
        }
    
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Numero { get; set; }
        public string Color { get; set; }
        public Nullable<int> IdGraficoFK { get; set; }
    
        public virtual Grafico_Plotly_Listbox Grafico_Plotly_Listbox { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubCategoria_Plotly_Listbox> SubCategoria_Plotly_Listbox { get; set; }
    }
}
