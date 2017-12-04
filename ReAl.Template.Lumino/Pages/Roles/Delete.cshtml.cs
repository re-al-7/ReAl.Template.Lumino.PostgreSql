using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Pages.Roles
{
    public class DeleteModel : BasePageModel
    {
        public DeleteModel (db_seguridadContext context) : base(context)
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
			
			try
			{
				SegRoles = await _context.SegRoles.FindAsync(id);

				if (SegRoles != null)
				{
					_context.SegRoles.Remove(SegRoles);
					await _context.SaveChangesAsync();
				}

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
