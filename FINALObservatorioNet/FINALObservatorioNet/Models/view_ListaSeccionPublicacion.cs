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
    
    public partial class view_ListaSeccionPublicacion
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public string Imagen { get; set; }
        public string Direccion { get; set; }
        public Nullable<decimal> Tipo { get; set; }
        public Nullable<bool> Destacar { get; set; }
        public string Etiqueta { get; set; }
        public string Fecha { get; set; }
        public Nullable<int> Megusta { get; set; }
        public string autor { get; set; }
        public string TipoPublicacion { get; set; }
        public Nullable<System.DateTime> FechaFecha { get; set; }
    }
}