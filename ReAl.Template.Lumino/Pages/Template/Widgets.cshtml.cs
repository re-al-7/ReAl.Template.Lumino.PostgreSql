// // <copyright file="Widgets.cshtml.cs" company="INTEGRATE - Soluciones Informaticas">
// // Copyright (c) 2016 Todos los derechos reservados
// // </copyright>
// // <author>re-al </author>
// // <date>2017-11-16 21:42</date>

using System;
using System.Collections.Generic;

using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Pages.Template
{
    public class WidgetsModel : BasePageModel
    {
        public WidgetsModel(db_seguridadContext context) : base(context)
        {
        }
        
        public List<SegAplicaciones> ListApp { get; private set; }
        public List<SegPaginas> ListPages { get; private set; }
        public string Usuario { get; private set; }
        public string CurrentApp { get; private set; }

        public void OnGet()
        {
            ListApp = this.GetAplicaciones();
            ListPages = this.GetPages();
            Usuario = this.GetUserName();
            CurrentApp = GetCurrentApp();
        }
    }
}