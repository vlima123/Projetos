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
    public class ProjetoesController : Controller
    {
        private readonly AppDbContext _context;

        public ProjetoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Projetoes
        public async Task<IActionResult> Index()
        {
            var objetivos = _context.Objetivos.ToList();
            ViewData["Objetivos"] = objetivos;
            return _context.Projetos != null ?
                        View(await _context.Projetos.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Projetos'  is null.");
        }

        // GET: Projetoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projetos == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos
                .FirstOrDefaultAsync(m => m.ProjetoId == id);
            if (projeto == null)
            {
                return NotFound();
            }
            var objetivos = _context.Objetivos.ToList();
            ViewData["Objetivos"] = objetivos;

            return View(projeto);
        }

        // GET: Projetoes/Create
        public IActionResult Create()
        {
            //lista de objetivos
            var objetivos = _context.Objetivos.ToList();
            ViewData["Objetivos"] = objetivos;

            return View();
        }

        // POST: Projetoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.mi'crosoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Projeto projeto)
        {
            var objetivos = _context.Objetivos.ToList();
            ViewData["Objetivos"] = objetivos;

            projeto.Atualiacao = DateTime.Now;
            projeto.Status = "Ativo";
            projeto.Selos = "Nenhum";

            projeto.DataInicio = DateTime.Now;

            _context.Add(projeto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }

        // GET: Projetoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projetos == null)
            {
                return NotFound();
            }
            var objetivos = _context.Objetivos.ToList();
            ViewData["Objetivos"] = objetivos;

            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto == null)
            {
                return NotFound();
            }
            return View(projeto);
        }

        // POST: Projetoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Projeto projeto)
        {
            if (id != projeto.ProjetoId)
            {
                return NotFound();
            }

            //pegar o projeto do banco
            // var projetoBanco = await _context.Projetos.FindAsync(id);

            // projeto.DataInicio = projetoBanco.DataInicio;
            // projeto.Atualiacao = DateTime.Now;

            projeto.Selos = "Nenhum";


            try
            {
                _context.Update(projeto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjetoExists(projeto.ProjetoId))
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

        // GET: Projetoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projetos == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projetos
                .FirstOrDefaultAsync(m => m.ProjetoId == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }
        // POST: Projetoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projetos == null)
            {
                return Problem("Entity set 'AppDbContext.Projetos'  is null.");
            }
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto != null)
            {
                _context.Projetos.Remove(projeto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjetoExists(int id)
        {
            return (_context.Projetos?.Any(e => e.ProjetoId == id)).GetValueOrDefault();
        }



        //GET: Projetoes/Home
        [HttpGet]
        public IActionResult Home()
        {
            var projetos = _context.Projetos.ToList();

            var contagem = projetos.Count();

            //passar uma view data com os objetivos possiveis


            return View(projetos);
        }


        //POST: finalizar projeto
        [HttpPost]
        public async Task<IActionResult> Finalizar(int id)
        {
            if (_context.Projetos == null)
            {
                return Problem("Entity set 'AppDbContext.Projetos'  is null.");
            }
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto != null)
            {
                projeto.Status = "Finalizado";
                projeto.DataFim = DateTime.Now;
                _context.Projetos.Update(projeto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //POST: pausar projeto
        [HttpPost]
        public async Task<IActionResult> Pausar(int id)
        {
            if (_context.Projetos == null)
            {
                return Problem("Entity set 'AppDbContext.Projetos'  is null.");
            }
            var projeto = await _context.Projetos.FindAsync(id);


            if (projeto.Status == "Pausado")
            {
                projeto.Status = "Ativo";
                _context.Projetos.Update(projeto);

            }

            else
            {
            projeto.Status = "Pausado";
            _context.Projetos.Update(projeto);
            }




            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //procurar por status
        [HttpPost]
        public async Task<IActionResult> Procurar(string status)
        {
            if (status == "")
            {
                var objetivos = _context.Objetivos.ToList();
                ViewData["Objetivos"] = objetivos;

                return RedirectToAction(nameof(Index));
            }


            if (_context.Projetos == null)
            {
                return Problem("Entity set 'AppDbContext.Projetos'  is null.");
            }
            var projetos = await _context.Projetos.Where(p => p.Status == status).ToListAsync();
            return View("Index", projetos);
        }

        //procurar por categoria
        [HttpPost]
        public async Task<IActionResult> ProcurarCategoria(string categoria)
        {
            var projetos = await _context.Projetos.Where(p => p.Categoria == categoria).ToListAsync();

            var objetivos = _context.Objetivos.ToList();
            ViewData["Objetivos"] = objetivos;

            return View("Index", projetos);
        }


    }
}
