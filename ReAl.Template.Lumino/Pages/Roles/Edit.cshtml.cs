using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Pages.Roles
{
    public class EditModel : BasePageModel
    {
        public EditModel (db_seguridadContext context) : base(context)
        {
        }

        [BindProperty]
        public SegRoles SegRoles { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
			
			ListApp = this.GetAplicaciones();
            ListPages = this.GetPages();
            Usuario = this.GetUserName();
            CurrentApp = this.GetCurrentApp();
			
            SegRoles = await _context.SegRoles.SingleOrDefaultAsync(m => m.Idsro == id);

            if (SegRoles == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SegRoles).State = EntityState.Modified;
			(SegRoles).Apitransaccion = "MODIFICAR";
			
            try
            {	
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
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
            return RedirectToPage("./Index");
        }
    }
}
