// // <copyright file="CMenus.cs" company="INTEGRATE - Soluciones Informaticas">
// // Copyright (c) 2016 Todos los derechos reservados
// // </copyright>
// // <author>re-al </author>
// // <date>2017-11-17 23:18</date>

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Helpers
{
    public static class CMenus
    {
        public static List<SegAplicaciones> GetAplicaciones(db_seguridadContext context)
        {
            return context.SegAplicaciones.ToList();            
        }
        
        public static List<SegPaginas> GetPages(HttpContext miContexto, db_seguridadContext context)
        {
            string currentApp = "--";
            if (miContexto.Session.Keys.Contains("currentApp"))
                currentApp  = miContexto.Session.GetString("currentApp").ToString();
            
            var objApp = context.SegAplicaciones.SingleOrDefault(app => app.Sigla == currentApp);
            
            if (objApp == null)
                return new List<SegPaginas>();
            else
                return  context.SegPaginas.ToList().Where(paginas =>
                paginas.Idsap == objApp.Idsap).OrderBy(x => x.Prioridad != null) // False comes before true
                    .ThenBy(x => x.Prioridad).ToList();

            /*
            obj = new SegPaginas();
            obj.descripcionspg = "Hijo normal";
            obj.nombrespg = "segusuario";
            obj.nombremenuspg = "index";
            lstPaginas.Add(obj);
            
            obj = new SegPaginas();
            obj.descripcionspg = "Dashboard";
            obj.nombrespg = "Template";
            obj.nombremenuspg = "Dashboard";
            lstPaginas.Add(obj);
            
            obj = new SegPaginas();
            obj.descripcionspg = "Widgets";
            obj.nombrespg = "Template";
            obj.nombremenuspg = "Widgets";
            lstPaginas.Add(obj);
            
            obj = new SegPaginas();
            obj.descripcionspg = "Charts";
            obj.nombrespg = "Template";
            obj.nombremenuspg = "Charts";
            lstPaginas.Add(obj);
            
            obj = new SegPaginas();
            obj.descripcionspg = "Buttons";
            obj.nombrespg = "Template";
            obj.nombremenuspg = "Buttons";
            lstPaginas.Add(obj);
            
            obj = new SegPaginas();
            obj.descripcionspg = "Forms";
            obj.nombrespg = "Template";
            obj.nombremenuspg = "Forms";
            lstPaginas.Add(obj);
            
            obj = new SegPaginas();
            obj.descripcionspg = "Tables";
            obj.nombrespg = "Template";
            obj.nombremenuspg = "Tables";
            lstPaginas.Add(obj);
            
            obj = new SegPaginas();
            obj.descripcionspg = "Alerts & Panels";
            obj.nombrespg = "Template";
            obj.nombremenuspg = "Panels";
            lstPaginas.Add(obj);
            
            obj = new SegPaginas();
            obj.descripcionspg = "Icons";
            obj.nombrespg = "Template";
            obj.nombremenuspg = "Icons";
            lstPaginas.Add(obj);
*/
        }
    }
}