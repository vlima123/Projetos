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
    public class NotasController : Controller
    {
        private readonly AppDbContext _context;

        public NotasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
              return _context.Notas != null ? 
                          View(await _context.Notas.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Notas'  is null.");
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .FirstOrDefaultAsync(m => m.NotaId == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotaId,Titulo,Descricao,DataCriacao,Categoria")] Nota nota)
        {
                nota.Revisao = true;
                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
           
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotaId,Titulo,Descricao,DataCriacao,Categoria")] Nota nota)
        {
            if (id != nota.NotaId)
            {
                return NotFound();
            }

         
             _context.Update(nota);
                    await _context.SaveChangesAsync();
                
              
                
                return RedirectToAction(nameof(Index));
       
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .FirstOrDefaultAsync(m => m.NotaId == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notas == null)
            {
                return Problem("Entity set 'AppDbContext.Notas'  is null.");
            }
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //procurar por categoria
       
        public async Task<IActionResult> ProcurarPorCategoria(string categoria)
        {
            if (categoria == null || _context.Notas == null)
            {
                return NotFound();
            }

            var notas = await _context.Notas.Where(m => m.Categoria == categoria).ToListAsync();
            if (notas == null)
            {
                return NotFound();
            }

            return View("Index", notas);
        }

        private bool NotaExists(int id)
        {
          return (_context.Notas?.Any(e => e.NotaId == id)).GetValueOrDefault();
        }
    }
}
