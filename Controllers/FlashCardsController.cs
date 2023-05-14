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
    public class FlashCardsController : Controller
    {
        private readonly AppDbContext _context;

        public FlashCardsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FlashCards
        public async Task<IActionResult> Index()
        {
              return _context.FlashCards != null ? 
                          View(await _context.FlashCards.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.FlashCards'  is null.");
        }

        // GET: FlashCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FlashCards == null)
            {
                return NotFound();
            }

            var flashCard = await _context.FlashCards
                .FirstOrDefaultAsync(m => m.FlashCardId == id);
            if (flashCard == null)
            {
                return NotFound();
            }

            return View(flashCard);
        }

         public IActionResult Create()
        {

            var projetos = _context.Projetos.ToList();
            ViewData["Projetos"] = projetos;
            var notas = _context.Notas.ToList();
            ViewData["Notas"] = notas;
            var objetivos = _context.Objetivos.ToList();
            ViewData["Objetivos"] = objetivos;

            return View();
        }

        // POST: FlashCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlashCardId,Pergunta,Resposta,Titulo,ObjetivosId,NotaId,ProjetoId, Tipo, Categoria")] FlashCard flashCard)
        {

                if (flashCard.Tipo == "Projeto")
                {
                    //pegar o projeto
                    var projeto = _context.Projetos.Find(flashCard.ProjetoId);
                    //adicionar a categoria do projeto para o flashcard
                    flashCard.Categoria = projeto.Categoria;
                }
                else if (flashCard.Tipo == "Objetivo")
                {
                    //pegar o objetivo
                    var objetivo = _context.Objetivos.Find(flashCard.ObjetivosId);
                    //adicionar a categoria do objetivo para o flashcard
                    flashCard.Categoria = objetivo.Tipo;
                    
                }
                else if (flashCard.Tipo == "Nota")
                {
                    //pegar a nota
                    var nota = _context.Notas.Find(flashCard.NotaId);
                    //adicionar a categoria da nota para o flashcard
                    flashCard.Categoria = nota.Categoria;
                }
             

           
                _context.Add(flashCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
           
        }

        // GET: FlashCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FlashCards == null)
            {
                return NotFound();
            }

            var flashCard = await _context.FlashCards.FindAsync(id);
            if (flashCard == null)
            {
                return NotFound();
            }
            return View(flashCard);
        }

        // POST: FlashCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlashCardId,Pergunta,Resposta,Titulo,ObjetivosId,NotaId,ProjetoId")] FlashCard flashCard)
        {
            if (id != flashCard.FlashCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flashCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlashCardExists(flashCard.FlashCardId))
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
            return View(flashCard);
        }

        // GET: FlashCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FlashCards == null)
            {
                return NotFound();
            }

            var flashCard = await _context.FlashCards
                .FirstOrDefaultAsync(m => m.FlashCardId == id);
            if (flashCard == null)
            {
                return NotFound();
            }

            return View(flashCard);
        }

        // POST: FlashCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FlashCards == null)
            {
                return Problem("Entity set 'AppDbContext.FlashCards'  is null.");
            }
            var flashCard = await _context.FlashCards.FindAsync(id);
            if (flashCard != null)
            {
                _context.FlashCards.Remove(flashCard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlashCardExists(int id)
        {
          return (_context.FlashCards?.Any(e => e.FlashCardId == id)).GetValueOrDefault();
        }
    }
}
