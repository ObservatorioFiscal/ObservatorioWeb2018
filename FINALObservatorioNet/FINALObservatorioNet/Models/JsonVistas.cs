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
    
    public partial class JsonVistas
    {
        public int IdJsonVistas { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public int IdJsonFK { get; set; }
    
        public virtual Json Json { get; set; }
    }
}
