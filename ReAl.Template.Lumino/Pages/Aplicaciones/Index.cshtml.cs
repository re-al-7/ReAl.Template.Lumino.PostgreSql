using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Pages.Aplicaciones
{
    [Authorize]
    public class IndexModel: BasePageModel
    {
        public IndexModel(db_seguridadContext context) : base(context)
        {
        }
        
        public List<SegAplicaciones> ListApp { get; private set; }
        public List<SegPaginas> ListPages { get; private set; }        
        public string Usuario { get; private set; }
        
        public List<SegAplicaciones> Listado { get; private set; }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public void OnGet()
        {
            ListApp = this.GetAplicaciones();
            ListPages = this.GetPages();
            Usuario = this.GetUserName();

            Listado = _context.SegAplicaciones.ToList();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPost(string id)
        {
            try
            {
                //Eliminamos el registro
            
                //Refrescamos
                TempData["Message"] = "Se ha eliminado el registro";
                return RedirectToPage();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}