using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoParaProjetos.Models;
using ProjetoParaProjetos.context;

namespace ProjetoParaProjetos.Controllers
{
    public class SeloAprendizadoesController : Controller
    {
        private readonly AppDbContext _context;

        public SeloAprendizadoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SeloAprendizadoes
        public async Task<IActionResult> Index()
        {
              return _context.SelosAprendizado != null ? 
                          View(await _context.SelosAprendizado.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.SelosAprendizado'  is null.");
        }

        // GET: SeloAprendizadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SelosAprendizado == null)
            {
                return NotFound();
            }

            var seloAprendizado = await _context.SelosAprendizado
                .FirstOrDefaultAsync(m => m.SeloAprendizadoId == id);
            if (seloAprendizado == null)
            {
                return NotFound();
            }

            return View(seloAprendizado);
        }

        // GET: SeloAprendizadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SeloAprendizadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeloAprendizadoId,Nome,Descricao")] SeloAprendizado seloAprendizado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seloAprendizado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seloAprendizado);
        }

        // GET: SeloAprendizadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SelosAprendizado == null)
            {
                return NotFound();
            }

            var seloAprendizado = await _context.SelosAprendizado.FindAsync(id);
            if (seloAprendizado == null)
            {
                return NotFound();
            }
            return View(seloAprendizado);
        }

        // POST: SeloAprendizadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeloAprendizadoId,Nome,Descricao")] SeloAprendizado seloAprendizado)
        {
            if (id != seloAprendizado.SeloAprendizadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seloAprendizado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeloAprendizadoExists(seloAprendizado.SeloAprendizadoId))
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
            return View(seloAprendizado);
        }

        // GET: SeloAprendizadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SelosAprendizado == null)
            {
                return NotFound();
            }

            var seloAprendizado = await _context.SelosAprendizado
                .FirstOrDefaultAsync(m => m.SeloAprendizadoId == id);
            if (seloAprendizado == null)
            {
                return NotFound();
            }

            return View(seloAprendizado);
        }

        // POST: SeloAprendizadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SelosAprendizado == null)
            {
                return Problem("Entity set 'AppDbContext.SelosAprendizado'  is null.");
            }
            var seloAprendizado = await _context.SelosAprendizado.FindAsync(id);
            if (seloAprendizado != null)
            {
                _context.SelosAprendizado.Remove(seloAprendizado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeloAprendizadoExists(int id)
        {
          return (_context.SelosAprendizado?.Any(e => e.SeloAprendizadoId == id)).GetValueOrDefault();
        }
    }
}
