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
    public class IndexModel : BasePageModel
    {
        public IndexModel (db_seguridadContext context) : base(context)
        {
        }

		public IList<SegRoles> SegRoles { get;set; }
		
        public async Task OnGetAsync()
        {
			ListApp = this.GetAplicaciones();
            ListPages = this.GetPages();
            Usuario = this.GetUserName();
            CurrentApp = this.GetCurrentApp();
			
            SegRoles = await _context.SegRoles.ToListAsync();
        }
    }
}
