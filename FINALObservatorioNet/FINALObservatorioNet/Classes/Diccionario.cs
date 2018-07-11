using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FINALObservatorioNet
{
    public class NombreSecciones
    {
        public static decimal aplicacion = 1000;
        public static decimal visualizacion = 2000;
        public static decimal publicacion = 3000;
        public static decimal proyecto = 4000;
        public static decimal noticia = 5000;

        public static Dictionary<decimal, string> diccionario = new Dictionary<decimal, string>
        {
            {visualizacion, "Visualizaciones"},
            {aplicacion, "Aplicaciones"},
            {publicacion, "Publicaciones"},
            {proyecto, "Proyectos"}
        };
    }

    public class TipoSecciones

    {
        public static int otros = 0;
        public static int aplicacion = 1;
        public static int visualizacion = 2;
        public static int publicacion = 3;
        public static int proyecto = 4;
        public static int noticia = 5;

        public static Dictionary<int, string> diccionario = new Dictionary<int, string>
        {
            {otros, "Otros"},
            {aplicacion, "Aplicación"},
            {visualizacion, "Visualización"},
            {publicacion, "Publicación"},
            {proyecto, "Proyecto"},
            {noticia, "Noticia"}
        };
    }
    public class EtiquetasVisualizacion
    {
        public static int GobiernoCentral = 1;
        public static int Regional = 2;
        public static int Municipal = 3;
        public static int Leydepresupuesto = 4;

        public static Dictionary<decimal, string> diccionario = new Dictionary<decimal, string>
        {
            {GobiernoCentral, "Gobierno Central"},
            {Regional, "Regional"},
            {Municipal, "Municipal"},
            {Leydepresupuesto, "Ley de Presupuesto"}
        };
    }
    public class EtiquetasAplicacion
    {
        public static int GobiernoCentral = 1;
        public static int Municipal = 3;

        public static Dictionary<decimal, string> diccionario = new Dictionary<decimal, string>
        {
            {GobiernoCentral, "Gobierno Central"},
            {Municipal, "Municipal"}
        };
    }
    public class EtiquetasPublicacion
    {
        public static int GobiernoCentral = 1;
        public static int Regional = 2;
        public static int Municipal = 3;
        public static int Leydepresupuesto = 4;
        public static int PersonaldelEstado = 5;
        public static int Calidaddedatos = 6;
        public static int Transparencia = 7;
        public static int RendiciondeCuentas = 8;
        public static int Compraspublicas = 9;
        public static int ModernizacionEstado = 10;

        public static Dictionary<decimal, string> diccionario = new Dictionary<decimal, string>
        {
            {GobiernoCentral, "Gobierno Central"},
            {Regional, "Regional"},
            {Municipal, "Municipal"},
            {Leydepresupuesto, "Ley de Presupuesto"},
            {PersonaldelEstado, "Personal del Estado"},
            {Calidaddedatos, "Calidad de Datos"},
            {Transparencia, "Transparencia"},
            {RendiciondeCuentas, "Rendición de Cuentas"},
            {Compraspublicas, "Compras Públicas"},
            {ModernizacionEstado, "Modernización del Estado"}
        };


    }
    public class Etiquetas
    {
        public static int GobiernoCentral    = 1;
        public static int Regional           = 2;
        public static int Municipal          = 3;
        public static int Leydepresupuesto   = 4;
        public static int PersonaldelEstado  = 5;
        public static int Calidaddedatos     = 6;
        public static int Transparencia      = 7;
        public static int RendiciondeCuentas = 8;
        public static int Compraspublicas    = 9;
        public static int ModernizacionEstado = 10;

        public static Dictionary<decimal, string> diccionario = new Dictionary<decimal, string>
        {
            {GobiernoCentral, "Gobierno Central"},
            {Regional, "Regional"},
            {Municipal, "Municipal"},
            {Leydepresupuesto, "Ley de Presupuesto"},
            {PersonaldelEstado, "Personal del Estado"},
            {Calidaddedatos, "Calidad de Datos"},
            {Transparencia, "Transparencia"},
            {RendiciondeCuentas, "Rendición de Cuentas"},
            {Compraspublicas, "Compras Públicas"}
        };

        public static Dictionary<decimal, string> diccionarioPublicacion = new Dictionary<decimal, string>
        {
            {GobiernoCentral, "Gobierno Central"},
            {Regional, "Regional"},
            {Municipal, "Municipal"},
            {Leydepresupuesto, "Ley de Presupuesto"},
            {PersonaldelEstado, "Personal del Estado"},
            {Calidaddedatos, "Calidad de Datos"},
            {Transparencia, "Transparencia"},
            {RendiciondeCuentas, "Rendición de Cuentas"},
            {Compraspublicas, "Compras Públicas"},
            {ModernizacionEstado, "Modernización del Estado"}
        };

        public static Dictionary<decimal, string> diccionarioAplicacion = new Dictionary<decimal, string>
        {
            {GobiernoCentral, "Gobierno Central"},
            {Regional, "Regional"},
            {Municipal, "Municipal"},
            {Leydepresupuesto, "Ley de Presupuesto"}
        };

        public static Dictionary<decimal, string> diccionarioVisualizacion = new Dictionary<decimal, string>
        {
            {GobiernoCentral, "Gobierno Central"},
            {Regional, "Regional"},
            {Municipal, "Municipal"},
            {Compraspublicas, "Compras Públicas"},
            {Leydepresupuesto, "Ley de Presupuestos"}
        };

    }

    public class Autores
    {
        public static int Guillermo = 1;
        public static int Jeannete  = 2;
        public static int Jose = 3;
        public static int Manuel = 4;
        public static int Orlando = 5;
        public static int Matias = 6;
        public static int Paula = 7;

        public static Dictionary<int, string> diccionarioAutor = new Dictionary<int, string>
        {
            {Jose, "Jose Mora"},
            {Manuel, "Manuel Henriquez"},
            {Jeannete, "Jeannette von Wolfersdorff"},
            {Guillermo, "Guillermo Patillo"},
            {Orlando, "Orlando Rojas"},
            {Matias, "Matias Jara"},
            {Paula, "Paula Diaz"}
        };
    }

    public class TipoPublicacion
    {
        public static int Columna       = 1;
        public static int Estudio       = 2;
        public static int Infografia    = 3;
        public static int Informe       = 4;
        public static int Principio     = 5;
        public static int Reportaje     = 6;
        public static int Analisis      = 7;

        public static Dictionary<int, string> diccionarioTipoPublicacion = new Dictionary<int, string>
        {
            {Columna, "Columnas"},
            {Estudio, "Estudios"},
            {Infografia, "Infografias"},
            {Informe, "Informes"},
            {Principio, "Principios"},
            {Reportaje, "Reportajes"},
            {Analisis, "Análisis"}
        };
    }
}
