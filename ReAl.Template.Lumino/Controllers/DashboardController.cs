using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        public DashboardController(db_seguridadContext context) : base(context)
        {
        }
        
        // GET: Dashboard
        public ActionResult Index(string app = "")
        {
            if (app != "")
                HttpContext.Session.SetString("currentApp", app);
            
            return View();
        }
    }
}