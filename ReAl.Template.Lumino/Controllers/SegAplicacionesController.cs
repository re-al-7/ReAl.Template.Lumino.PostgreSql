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
    public class SegAplicacionesController : BaseController
    {
        public SegAplicacionesController(db_seguridadContext context) : base(context)
        {
        }

        // GET: SegAplicaciones
        public async Task<IActionResult> Index()
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            return View(await _context.SegAplicaciones.ToListAsync());
        }

        // GET: SegAplicaciones/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            if (id == null)
            {
                return NotFound();
            }

            var segAplicaciones = await _context.SegAplicaciones
                .SingleOrDefaultAsync(m => m.Idsap == id);
            if (segAplicaciones == null)
            {
                return NotFound();
            }

            return View(segAplicaciones);
        }

        // GET: SegAplicaciones/Create
        public IActionResult Create()
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            return View();
        }

        // POST: SegAplicaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idsap,Sigla,Nombre,Descripcion,Apiestado,Apitransaccion,Usucre,Feccre,Usumod,Fecmod")] SegAplicaciones segAplicaciones)
        {           
            if (ModelState.IsValid)
            {
                _context.Add(segAplicaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(segAplicaciones);
        }

        // GET: SegAplicaciones/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            if (id == null)
            {
                return NotFound();
            }

            var segAplicaciones = await _context.SegAplicaciones.SingleOrDefaultAsync(m => m.Idsap == id);
            if (segAplicaciones == null)
            {
                return NotFound();
            }
            return View(segAplicaciones);
        }

        // POST: SegAplicaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Idsap,Sigla,Nombre,Descripcion,Apiestado,Apitransaccion,Usucre,Feccre,Usumod,Fecmod")] SegAplicaciones segAplicaciones)
        {
            if (id != segAplicaciones.Idsap)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    segAplicaciones.Apitransaccion = "MODIFICAR";
                    _context.Update(segAplicaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SegAplicacionesExists(segAplicaciones.Idsap))
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
            return View(segAplicaciones);
        }

        // GET: SegAplicaciones/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            ViewBag.ListApp = this.GetAplicaciones();
            ViewBag.ListPages = this.GetPages();
            ViewData["Usuario"] = this.getUserName();
            
            if (id == null)
            {
                return NotFound();
            }

            var segAplicaciones = await _context.SegAplicaciones
                .SingleOrDefaultAsync(m => m.Idsap == id);
            if (segAplicaciones == null)
            {
                return NotFound();
            }

            return View(segAplicaciones);
        }

        // POST: SegAplicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var segAplicaciones = await _context.SegAplicaciones.SingleOrDefaultAsync(m => m.Idsap == id);
            _context.SegAplicaciones.Remove(segAplicaciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SegAplicacionesExists(long id)
        {
            return _context.SegAplicaciones.Any(e => e.Idsap == id);
        }
    }
}
