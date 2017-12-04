using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Npgsql;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Pages.Roles
{
    public class CreateModel : BasePageModel
    {
        public CreateModel (db_seguridadContext context) : base(context)
        {
        }

        public IActionResult OnGet()
        {
			ListApp = this.GetAplicaciones();
            ListPages = this.GetPages();
            Usuario = this.GetUserName();
            CurrentApp = this.GetCurrentApp();
            return Page();
        }

        [BindProperty]
        public SegRoles SegRoles { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {		
            if (!ModelState.IsValid)
            {
                return Page();
            }
			
			try
			{
				_context.SegRoles.Add(SegRoles);
				await _context.SaveChangesAsync();

				return RedirectToPage("./Index");
			}
			catch (Exception exp)
			{
				if (exp.InnerException is NpgsqlException)
					ErrorDb = exp.InnerException.Message;                        
				else
					ModelState.AddModelError("", exp.Message);
		   
				ListApp = this.GetAplicaciones();
				ListPages = this.GetPages();
				Usuario = this.GetUserName();
				CurrentApp = this.GetCurrentApp();
				return Page();
			}
        }
    }
}