using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReAl.Template.Lumino.Helpers;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(db_seguridadContext context) : base(context)
        {
        }
        
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost, ActionName("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SegUsuarios user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                const string badUserNameOrPasswordMessage = "Usuario o contraseña incorrectos.";
                if (user == null)
                {
                    ModelState.AddModelError("", badUserNameOrPasswordMessage);
                    return View();
                }
                
                const string incompleteInformation = "Debe especificar un usuario y contraseña para continuar.";
                if (user.Login == "" || user.Password == "")
                {
                    ModelState.AddModelError("", incompleteInformation);
                    return View();
                }
                
                var obj = _context.SegUsuarios.SingleOrDefault(m => m.Login == user.Login);
                if (obj == null)
                {
                    ModelState.AddModelError("", badUserNameOrPasswordMessage);
                    return View();
                }
                    
                if (!CFuncionesEncriptacion.generarMD5(user.Password).ToUpper().Equals(obj.Password.ToUpper()))
                {
                    ModelState.AddModelError("", badUserNameOrPasswordMessage);
                    return View();
                }
                
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, obj.Login));

                var objPer = _context.SegPersonas.SingleOrDefault(persona => persona.Idspe == obj.Idspe);
                identity.AddClaim(new Claim(ClaimTypes.GivenName, objPer.Nombres + " " + objPer.Apellidos));

                //Añadimos la aplicacion por defecto
                var objApp = _context.SegAplicaciones.First();
                HttpContext.Session.SetString("currentApp", objApp.Sigla);
                
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (returnUrl == null)
                {
                    returnUrl = TempData["returnUrl"]?.ToString();
                }
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
