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
    
    public partial class nivel5
    {
        public int IdNivel5 { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Valor { get; set; }
        public Nullable<bool> Tipo { get; set; }
        public Nullable<int> Fk_IdNivel4 { get; set; }
        public Nullable<int> Valor2 { get; set; }
    
        public virtual nivel4 nivel4 { get; set; }
    }
}