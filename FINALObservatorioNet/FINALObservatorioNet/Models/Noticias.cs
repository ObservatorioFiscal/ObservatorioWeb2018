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
    
    public partial class Noticias
    {
        public int IdNoticias { get; set; }
        public string Titulo { get; set; }
        public string Fuente { get; set; }
        public string Url { get; set; }
        public string Imagen { get; set; }
        public bool Visible { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> Internas { get; set; }
        public Nullable<int> IdProyecto { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
    }
}