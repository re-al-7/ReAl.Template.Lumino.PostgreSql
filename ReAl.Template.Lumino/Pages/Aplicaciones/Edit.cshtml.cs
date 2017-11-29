using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Pages.Aplicaciones
{
    [Authorize]
    public class EditModel: BasePageModel
    {
        public EditModel(db_seguridadContext context) : base(context)
        {
        }
        
        public List<SegAplicaciones> ListApp { get; private set; }
        public List<SegPaginas> ListPages { get; private set; }
        public string Usuario { get; private set; }

        [BindProperty]
        public SegAplicaciones MiAplicacion { get; set; }
        
        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult OnGetAsync(string id)
        {
            ListApp = this.GetAplicaciones();
            ListPages = this.GetPages();
            Usuario = this.GetUserName();
            
            if (MiAplicacion == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //Guardamos el registro
            
                //Redireccionamos
                return RedirectToPage("./Index");
            }
            else
            {
                ListApp = this.GetAplicaciones();
                ListPages = this.GetPages();
                Usuario = this.GetUserName();
                ModelState.AddModelError("", string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)));
            }
            return Page();
        }
    }
}