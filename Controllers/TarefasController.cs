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
    public class TarefasController : Controller
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tarefas
        public async Task<IActionResult> Index()
        {
            var categorias = new List<string> { "Front-End", "Trabalho", "Back-End","Data-Science", "Vestibular", "FullStack", "Computação", "Outros" };
            ViewData["Categorias"] = categorias;
            return _context.Tarefas != null ?
                        View(await _context.Tarefas.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Tarefas'  is null.");
        }

        // GET: Tarefas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }
var categorias = new List<string> { "Front-End", "Trabalho", "Back-End","Data-Science", "Vestibular", "FullStack", "Computação", "Outros" };
            ViewData["Categorias"] = categorias;
            return View(tarefa);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            var prioridades = new List<string> { "Baixa", "Média", "Alta" };
            ViewData["Prioridades"] = prioridades;

            var categorias = new List<string> { "Front-End", "Trabalho", "Back-End","Data-Science", "Vestibular", "FullStack", "Computação", "Outros" };
            ViewData["Categorias"] = categorias;
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarefa tarefa)
        {

            tarefa.DataCriacao = DateTime.Now;
            tarefa.DataFinal = DateTime.Now.AddDays(7);
            tarefa.Status = "Aberta";





            _context.Add(tarefa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            var prioridades = new List<string> { "Baixa", "Média", "Alta" };
            ViewData["Prioridades"] = prioridades;
            var categorias = new List<string> { "Front-End", "Trabalho", "Back-End","Data-Science", "Vestibular", "FullStack", "Computação", "Outros" };
            ViewData["Categorias"] = categorias;
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TarefaId,Nome,Descricao,DataCriacao,DataFinal,Prioridade,Categoria")] Tarefa tarefa)
        {
            if (id != tarefa.TarefaId)
            {
                return NotFound();
            }
            var prioridades = new List<string> { "Baixa", "Média", "Alta" };
            ViewData["Prioridades"] = prioridades;
            var categorias = new List<string> { "Front-End", "Trabalho", "Back-End","Data-Science", "Vestibular", "FullStack", "Computação", "Outros" };
            ViewData["Categorias"] = categorias;


           
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
             
                    
                return RedirectToAction(nameof(Index));
            }
    

        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tarefas == null)
            {
                return Problem("Entity set 'AppDbContext.Tarefas'  is null.");
            }
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
            return (_context.Tarefas?.Any(e => e.TarefaId == id)).GetValueOrDefault();
        }

        //finaliza a tarefa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finalizar(int id)
        {
            if (_context.Tarefas == null)
            {
                return Problem("Entity set 'AppDbContext.Tarefas'  is null.");
            }
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa.Status == "Finalizada")
            {
                return RedirectToAction(nameof(Index));
            }

            if (tarefa != null)
            {
                tarefa.Status = "Finalizada";
                tarefa.DataFinal = DateTime.Now;
                _context.Tarefas.Update(tarefa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //procurar por categoria
              public async Task<IActionResult> ProcurarCategoria(string categoria)
        {
            if (_context.Tarefas == null)
            {
                return Problem("Entity set 'AppDbContext.Tarefas'  is null.");
            }
            var tarefas = await _context.Tarefas.Where(t => t.Categoria == categoria).ToListAsync();
            return View(nameof(Index), tarefas);
        }

        //procurar por prioridade
        public async Task<IActionResult> ProcurarPrioridade(string prioridade)
        {
            if (_context.Tarefas == null)
            {
                return Problem("Entity set 'AppDbContext.Tarefas'  is null.");
            }
            var tarefas = await _context.Tarefas.Where(t => t.Prioridade == prioridade).ToListAsync();
            return View(nameof(Index), tarefas);
        }

        //procurar por data
        public async Task<IActionResult> ProcurarData(DateTime data)
        {
            if (_context.Tarefas == null)
            {
                return Problem("Entity set 'AppDbContext.Tarefas'  is null.");
            }
            //considerar a data de criação e sem usar as horas
            var tarefas = await _context.Tarefas.Where(t => t.DataCriacao.Date == data.Date).ToListAsync();

           
            return View(nameof(Index), tarefas);

    }
}}
