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
    
    public partial class Grafico_Plotly_Listbox
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Grafico_Plotly_Listbox()
        {
            this.Categoria_Plotly_Listbox = new HashSet<Categoria_Plotly_Listbox>();
            this.Lineas_Plotly_Listbox = new HashSet<Lineas_Plotly_Listbox>();
        }
    
        public int IdGrafico { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string GlobalColor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categoria_Plotly_Listbox> Categoria_Plotly_Listbox { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lineas_Plotly_Listbox> Lineas_Plotly_Listbox { get; set; }
    }
}
