using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReAl.Template.Lumino.Helpers;

namespace ReAl.Template.Lumino.Models
{
    public class BasePageModel : PageModel
    {
        public readonly db_seguridadContext _context;
        public List<SegAplicaciones> ListApp { get; internal set; }
        public List<SegPaginas> ListPages { get; internal set; }
        public string Usuario { get; internal  set; }
        public string CurrentApp { get; internal  set; }        
        public string ErrorDb { get; internal  set; }

        public BasePageModel(db_seguridadContext context)
        {
            _context = context;
        }
        
        public string GetCurrentApp()
        {
            if (this.HttpContext.Session.Keys.Contains("currentApp"))
                return this.HttpContext.Session.GetString("currentApp").ToString();
            return null;
        }
        
        public string GetLogin()
        {
            if (User.Identity.IsAuthenticated)
                return User.Identity.Name;
            return null;
        }

        public string GetUserName()
        {
            if (User.Identity.IsAuthenticated)
            {
                var obj = _context.SegUsuarios.SingleOrDefault(m => m.Login == User.Identity.GetGivenName());
                if (obj == null)
                {
                    if (User.Identity.GetGivenName().Length > 30)
                        return User.Identity.GetGivenName().Split(' ')[0];
                    else
                        return User.Identity.GetGivenName();
                }
                else
                {
                    var objPer = _context.SegPersonas.SingleOrDefault(persona => persona.Idspe == obj.Idspe);
                    if ((objPer.Nombres + " " + objPer.Apellidos).ToString().Length > 30)
                        return objPer.Nombres;
                    else
                        return objPer.Nombres + " " + objPer.Apellidos;
                }
            }
            return null;
        }

        public SegUsuarios GetUser()
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