using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino.Controllers
{
    public class SegConfiguracionController : BaseController
    {
        public SegConfiguracionController(db_seguridadContext context) : base(context)
        {
        }

        // GET: SegConfiguracion
        public async Task<IActionResult> Index()
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            var db_seguridadContext = _context.SegConfiguracion.Include(s => s.IdstaNavigation);
            return View(await db_seguridadContext.ToListAsync());
        }

        // GET: SegConfiguracion/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            if (id == null)
            {
                return NotFound();
            }

            var segConfiguracion = await _context.SegConfiguracion
                .Include(s => s.IdstaNavigation)
                .SingleOrDefaultAsync(m => m.Idscf == id);
            if (segConfiguracion == null)
            {
                return NotFound();
            }

            return View(segConfiguracion);
        }

        // GET: SegConfiguracion/Create
        public IActionResult Create()
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            ViewData["Idsta"] = new SelectList(_context.SegTablas, "Idsta", "Alias");
            return View();
        }

        // POST: SegConfiguracion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idscf,Idsta,Configuracion,Funcion,Parametrosentrada,Parametrossalida,Descripcion,Apiestado,Apitransaccion,Usucre,Feccre,Usumod,Fecmod")] SegConfiguracion segConfiguracion)
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            if (ModelState.IsValid)
            {
                _context.Add(segConfiguracion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idsta"] = new SelectList(_context.SegTablas, "Idsta", "Alias", segConfiguracion.Idsta);
            return View(segConfiguracion);
        }

        // GET: SegConfiguracion/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            if (id == null)
            {
                return NotFound();
            }

            var segConfiguracion = await _context.SegConfiguracion.SingleOrDefaultAsync(m => m.Idscf == id);
            if (segConfiguracion == null)
            {
                return NotFound();
            }
            ViewData["Idsta"] = new SelectList(_context.SegTablas, "Idsta", "Alias", segConfiguracion.Idsta);
            return View(segConfiguracion);
        }

        // POST: SegConfiguracion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Idscf,Idsta,Configuracion,Funcion,Parametrosentrada,Parametrossalida,Descripcion,Apiestado,Apitransaccion,Usucre,Feccre,Usumod,Fecmod")] SegConfiguracion segConfiguracion)
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            if (id != segConfiguracion.Idscf)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(segConfiguracion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SegConfiguracionExists(segConfiguracion.Idscf))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idsta"] = new SelectList(_context.SegTablas, "Idsta", "Alias", segConfiguracion.Idsta);
            return View(segConfiguracion);
        }

        // GET: SegConfiguracion/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            if (id == null)
            {
                return NotFound();
            }

            var segConfiguracion = await _context.SegConfiguracion
                .Include(s => s.IdstaNavigation)
                .SingleOrDefaultAsync(m => m.Idscf == id);
            if (segConfiguracion == null)
            {
                return NotFound();
            }

            return View(segConfiguracion);
        }

        // POST: SegConfiguracion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            var segConfiguracion = await _context.SegConfiguracion.SingleOrDefaultAsync(m => m.Idscf == id);
            _context.SegConfiguracion.Remove(segConfiguracion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SegConfiguracionExists(long id)
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            return _context.SegConfiguracion.Any(e => e.Idscf == id);
        }
    }
}
