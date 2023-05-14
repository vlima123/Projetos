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
    public class ObjetivosController : Controller
    {
        private readonly AppDbContext _context;

        public ObjetivosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Objetivos
        public async Task<IActionResult> Index()
        {
            return _context.Objetivos != null ?
                        View(await _context.Objetivos.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Objetivos'  is null.");
        }

        // GET: Objetivos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Objetivos == null)
            {
                return NotFound();
            }

            var objetivos = await _context.Objetivos
                .FirstOrDefaultAsync(m => m.ObjetivosId == id);
            if (objetivos == null)
            {
                return NotFound();
            }

            return View(objetivos);
        }

        // GET: Objetivos/Create
        public IActionResult Create()
        {
            List<string> listaTipos = new List<string> { "Programação", "Data Science", "Vestibular", "Cloud", "Academia", "Outros" };
            ViewData["Tipos"] = listaTipos;

            return View();
        }

        // POST: Objetivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObjetivosId,Objetivo,Descricao,Tipo,Status")] Objetivos objetivos)
        {

            objetivos.Status = "Ativo";
            _context.Add(objetivos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        // GET: Objetivos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Objetivos == null)
            {
                return NotFound();
            }
            List<string> listaTipos = new List<string> { "Programação", "Data Science", "Vestibular", "Cloud", "Academia", "Outros" };
            List<string> listaStatus = new List<string> { "Ativo", "Inativo" };
            ViewData["Status"] = listaStatus;
            ViewData["Tipos"] = listaTipos;

            var objetivos = await _context.Objetivos.FindAsync(id);
            if (objetivos == null)
            {
                return NotFound();
            }
            return View(objetivos);
        }

        // POST: Objetivos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObjetivosId,Objetivo,Descricao,Tipo,Status")] Objetivos objetivos)
        {

              List<string> listaTipos = new List<string> { "Programação", "Data Science", "Vestibular", "Cloud", "Academia", "Outros" };
            List<string> listaStatus = new List<string> { "Ativo", "Inativo" };
            ViewData["Status"] = listaStatus;
            ViewData["Tipos"] = listaTipos;
           
                    _context.Update(objetivos);
                    await _context.SaveChangesAsync();



             
                return RedirectToAction(nameof(Index));
          
        }

        // GET: Objetivos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Objetivos == null)
            {
                return NotFound();
            }

            var objetivos = await _context.Objetivos
                .FirstOrDefaultAsync(m => m.ObjetivosId == id);
            if (objetivos == null)
            {
                return NotFound();
            }

            return View(objetivos);
        }

        // POST: Objetivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Objetivos == null)
            {
                return Problem("Entity set 'AppDbContext.Objetivos'  is null.");
            }
            var objetivos = await _context.Objetivos.FindAsync(id);
            if (objetivos != null)
            {
                _context.Objetivos.Remove(objetivos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        //procurar por tipo

        public async Task<IActionResult> ProcurarPorTipo(string tipo)
        {
            if (tipo == null || _context.Objetivos == null)
            {
                return NotFound();
            }

            //lista de objetivos com aquele tipo
            var objetivos = await _context.Objetivos
                .Where(m => m.Tipo == tipo).ToListAsync();


            if (objetivos == null)
            {
                return NotFound();
            }

            return View("index", objetivos);
        }

        //procurar por status

        public async Task<IActionResult> ProcurarPorStatus(string status)
        {
            if (status == null || _context.Objetivos == null)
            {
                return NotFound();
            }

              var objetivos = await _context.Objetivos
                .Where(m => m.Status == status).ToListAsync();
            if (objetivos == null)
            {
                return NotFound();
            }

            return View("index", objetivos);
        }

        private bool ObjetivosExists(int id)
        {
            return (_context.Objetivos?.Any(e => e.ObjetivosId == id)).GetValueOrDefault();
        }
    }
}
