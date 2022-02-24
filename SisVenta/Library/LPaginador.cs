using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisVenta.Library
{
    public class LPaginador<T>
    {
        //cantidad de resultados por pagina
        private int pagi_cuantos = 8;
        //Cantidad de enlaces que se mostraran como maximo en la barra de navgacion
        private int pagi_nav_num_enlaces = 3;
        private int pagi_actual;
        //Definimos que ira en el enlace a la pagina anterior
        private string pagi_nav_anterior = " &laquo; Anterior ";
        //Definimos que ira en el enlace a la pagina siguiente
        private string pagi_nav_siguiente = " Anterior &raquo; ";
        //Definimos que ira en el enlace a la pagina siguiente
        private string pagi_nav_primera = " &laquo; Primero ";
        private string pagi_nav_ultima = " Ultimo &raquo; ";
        private string pagi_navegacion = null;

        public object[] paginador(List<T> table, int pagina, int registros, string area, string controller, string action, string host)
        {
            pagi_actual = pagina == 0 ? 1 : pagina;
            pagi_cuantos = registros > 0 ? registros : pagi_cuantos;

            int pagi_totalReg = table.Count;
            double valor1 = Math.Ceiling((double)pagi_totalReg / (double)pagi_cuantos);
            int pagi_totalPags = Convert.ToInt16(Math.Ceiling(valor1));
            if (pagi_actual != 1)
            {
                //si no estamos en la pagina 1. Ponemos el enlace "primera"
                int pagi_url = 1;//sera el numero de pagina al que enlazamos
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action +
                    "?id=" + pagi_url + "&Registros=" + pagi_cuantos + "&area=" + area + "'>" + pagi_nav_primera + "</a>";

                //Si no estamos en la pagina1. Ponemos el enlace "Anterior"
                pagi_url = pagi_actual - 1;
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action +
                    "?id=" + pagi_url + "&Registros=" + pagi_cuantos + "&area=" + area + "'>" + pagi_nav_anterior + "</a>";
            }
            //si se definio la variable pagi_nav_num_enlaces
            //Calculamos el intervalo para restar y sumar a partir de la pagina actual
            double valor2 = (pagi_nav_num_enlaces / 2);
            int pagi_nav_intervalo = Convert.ToInt16(Math.Round(valor2));
            //Calculamos desde que numero de pagina se mostrara
            int pagi_nav_desde = pagi_actual - pagi_nav_intervalo;
            //Calculamos hasta que numero de pagina se mostrara
            int pagi_nav_hasta = pagi_actual + pagi_nav_intervalo;
            //Si pagi_nav_desde es un numero negativo
            if (pagi_nav_desde < 1)
            {
                //Le sumamos la cantidad sobrante al final para mantener
                //el numero de enlaces que se quiere mostrar
                pagi_nav_hasta -= (pagi_nav_desde - 1);
                //Establecemos pagi_nav_desde como 1
                pagi_nav_desde = 1;
            }
            if (pagi_nav_hasta > pagi_totalPags)
            {
                //Le restamos la cantidad excedida al comienzo para mantener
                //el numero de enlaces que se quiere mostrar
                pagi_nav_desde -= (pagi_nav_hasta - pagi_totalPags);
                //Establecemos pagi_nav_hasta como el total de paginas.
                pagi_nav_hasta = pagi_totalPags;
                //Hacemos el ultimo ajuste verificando que el cambiar pagi_nav_desde
                //no haya quedado con un valor no validado
                if (pagi_nav_desde < 1)
                {
                    pagi_nav_desde = 1;
                }
            }
            for (int pagi_i = pagi_nav_desde; pagi_i <= pagi_nav_hasta; pagi_i++)
            {
                //desde pagina 1 hasta ultima pagina (pagi_totalPags)
                if (pagi_i == pagi_actual)
                {
                    //Si el numero de pagina es la actual (pagi_actual). se escribe el numero, pero sin enlace y en negritas
                    pagi_navegacion += "<span class='btn btn-default' disabled='disabled'>" + pagi_i + "</span>";
                }
                else
                {
                    //Si es cualquier otro. Se escribe el enlace a dicho numero de paginas
                    pagi_navegacion += "<a class'btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagi_i + "&registros=" + pagi_cuantos + "&area=" + area + "'>" + pagi_i + "</a>";
                }
            }

            if (pagi_actual < pagi_totalPags)
            {
                // Si no estamos en la ultima pagina. Ponemos el enlace "Siguiente"
                int pagi_url = pagi_actual + 1;//sera el numero de pagina al que enlazamos
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" + pagi_nav_siguiente + "</a>";

                //Si no estamos en la ultima pagina. Punemos el enlace "Ultima"
                pagi_url = pagi_totalPags;//sera el numero de pagina al que enlazamos
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" + pagi_nav_ultima + "</a>";
            }

            /*Obtencion de los registros que se mostraran en la pagina actual
             */
            //Calculamos desde que registro se mostrara en esta pagina
            //Recordemos que el conteo empieza desde CERO.
            int pagi_inicial = (pagi_actual - 1) * pagi_cuantos;

            //Consulta SQL. Devuelve cantidad de registros empezando desde pagi_inicial
            var query = table.Skip(pagi_inicial).Take(pagi_cuantos).ToList();
            string pagi_info = " del <b>" + pagi_actual + "</b> al <b>" + pagi_totalPags + "</b> de <b>" + pagi_totalReg + "</b><b>/" + pagi_cuantos + " </b>";
            object[] data = { pagi_info, pagi_navegacion, query };

            return data;
        }
    }
}
