using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Pages.Roles
{
    public class DetailsModel : BasePageModel
    {
        public DetailsModel (db_seguridadContext context) : base(context)
        {
        }

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
    }
}
