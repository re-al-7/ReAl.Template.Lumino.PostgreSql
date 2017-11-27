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
    public class SegPersonasController : BaseController
    {
		public SegPersonasController(db_seguridadContext context) : base(context)
        {
        }

        // GET: SegPersonas
        public async Task<IActionResult> Index()
        {
			ViewBag.ListApp = GetAplicaciones();
            ViewBag.ListPages = GetPages();
            ViewData["Usuario"] = getUserName();
			
            return View(await _context.SegPersonas.ToListAsync());
        }

        // GET: SegPersonas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
			ViewBag.ListApp = GetAplicaciones();
            ViewBag.ListPages = GetPages();
            ViewData["Usuario"] = getUserName();
			
            if (id == null)
            {
                return NotFound();
            }

            var segPersonas = await _context.SegPersonas
                .SingleOrDefaultAsync(m => m.Idspe == id);
            if (segPersonas == null)
            {
                return NotFound();
            }

            return View(segPersonas);
        }

        // GET: SegPersonas/Create
        public IActionResult Create()
        {
			ViewBag.ListApp = GetAplicaciones();
            ViewBag.ListPages = GetPages();
            ViewData["Usuario"] = getUserName();
			
            return View();
        }

        // POST: SegPersonas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idspe,Nombres,Apellidos,Sexo,Ci,Correo,Apiestado,Apitransaccion,Usucre,Feccre,Usumod,Fecmod")] SegPersonas segPersonas)
        {
			if (ModelState.IsValid)
            {
                _context.Add(segPersonas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(segPersonas);
        }

        // GET: SegPersonas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
			ViewBag.ListApp = GetAplicaciones();
            ViewBag.ListPages = GetPages();
            ViewData["Usuario"] = getUserName();			

            if (id == null)
            {
                return NotFound();
            }

            var segPersonas = await _context.SegPersonas.SingleOrDefaultAsync(m => m.Idspe == id);
            if (segPersonas == null)
            {
                return NotFound();
            }
            return View(segPersonas);
        }

        // POST: SegPersonas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Idspe,Nombres,Apellidos,Sexo,Ci,Correo,Apiestado,Apitransaccion,Usucre,Feccre,Usumod,Fecmod")] SegPersonas segPersonas)
        {
            if (id != segPersonas.Idspe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					(segPersonas).Apitransaccion = "MODIFICAR";
                    _context.Update(segPersonas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SegPersonasExists(segPersonas.Idspe))
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
            return View(segPersonas);
        }

        // GET: SegPersonas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
			ViewBag.ListApp = GetAplicaciones();
            ViewBag.ListPages = GetPages();
            ViewData["Usuario"] = getUserName();
			
            if (id == null)
            {
                return NotFound();
            }

            var segPersonas = await _context.SegPersonas
                .SingleOrDefaultAsync(m => m.Idspe == id);
            if (segPersonas == null)
            {
                return NotFound();
            }

            return View(segPersonas);
        }

        // POST: SegPersonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var segPersonas = await _context.SegPersonas.SingleOrDefaultAsync(m => m.Idspe == id);
            _context.SegPersonas.Remove(segPersonas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SegPersonasExists(long id)
        {
            return _context.SegPersonas.Any(e => e.Idspe == id);
        }
    }
}
