using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FINALObservatorioNet.Classes
{
    public class Auxiliares
    {
        public string Convertiretiqueta(string entrada)
        {
            List<string> lista = entrada.Split('-').Where(r => r != "").ToList();
            string salida = "";
            foreach (var item in lista)
            {
                string aux = Etiquetas.diccionarioPublicacion.SingleOrDefault(r => r.Key == Convert.ToDecimal(item)).Value;
                salida = salida + aux + " - ";
            }
            salida = salida.TrimEnd(' ');
            salida = salida.TrimEnd('-');
            return salida;
        }

        public string Convertirautor(string entrada)
        {
            string salida = "";
            try
            {
                int aux = Convert.ToInt32(entrada);
                salida = Autores.diccionarioAutor.SingleOrDefault(r => r.Key == aux).Value;
                salida ="<img src='../../../Content/Images/Personas/"+aux+ ".png' class='img-redondear img-fluid img-vertical-center'> " + salida;
            }
            catch
            {
                salida = "Indefinido";
            }
            return salida;
        }

        public string Convertirtipo(decimal entrada)
        {
            string salida =TipoSecciones.diccionario.SingleOrDefault(r => r.Key == int.Parse(entrada.ToString())).Value;
            return salida;
        }


        public string ConvertirtipoPublicacion(string entrada)
        {
            string salida = TipoPublicacion.diccionarioTipoPublicacion.SingleOrDefault(r => r.Key == int.Parse(entrada.ToString())).Value;
            return salida;
        }
        public string Convertirurl(string entrada,decimal tipo)
        {
            switch (tipo)
            {
                case 1:
                    return "../../aplicacion/" + entrada;
                case 2:
                    return "../../visualizacion/repo/" + entrada;
                case 4:
                    return "../../proyecto/" + entrada;
                default:
                    return entrada;
            }
        }
        public string Convertirimagen(string entrada, decimal tipo)
        {
            switch (tipo)
            {
                case 1:
                    return "../Content/images/seccion/" + entrada;
                case 2:
                    return "../Content/images/seccion/" + entrada;
                case 3:
                    return "../Content/images/seccion/" + entrada;
                case 4:
                    return "../Content/images/seccion/" + entrada;
                case 5:
                    return "../Content/images/seccion/" + entrada;
                default:
                    return "../Content/images/" + entrada;
            }
        }





    }
}
