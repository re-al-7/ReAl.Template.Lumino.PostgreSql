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
            return context.SegAplicaciones.OrderBy(x => x.Nombre) .ToList();            
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

        }
    }
}