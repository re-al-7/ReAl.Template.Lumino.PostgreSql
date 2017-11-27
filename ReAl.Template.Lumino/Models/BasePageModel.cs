using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReAl.Template.Lumino.Helpers;

namespace ReAl.Template.Lumino.Models
{
    public class BasePageModel : PageModel
    {
        public readonly db_seguridadContext _context;

        public BasePageModel(db_seguridadContext context)
        {
            _context = context;
        }
        
        public string getLogin()
        {
            if (User.Identity.IsAuthenticated)
                return User.Identity.Name;
            return null;
        }

        public string getUserName()
        {
            if (User.Identity.IsAuthenticated)
            {
                var obj = _context.SegUsuarios.SingleOrDefault(m => m.Login == User.Identity.GetGivenName());
                if (obj == null)
                    return User.Identity.GetGivenName();
                else
                {
                    var objPer = _context.SegPersonas.SingleOrDefault(persona => persona.Idspe == obj.Idspe);
                    return objPer.Nombres + " " + objPer.Apellidos;
                }
            }
            return null;
        }

        public SegUsuarios getUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var obj = _context.SegUsuarios.SingleOrDefault(m => m.Login == User.Identity.GetGivenName());
                return obj;
            }
            else
                return null;
        }

        protected List<SegAplicaciones> GetAplicaciones()
        {
            return CMenus.GetAplicaciones(_context);
        }

        protected List<SegPaginas> GetPages()
        {
            return CMenus.GetPages(this.HttpContext, _context);
        }
    }
}