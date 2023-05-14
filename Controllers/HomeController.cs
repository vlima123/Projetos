using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoParaProjetos.context;
using ProjetoParaProjetos.Models;
using ProjetoParaProjetos.Models.ViewModels;

namespace ProjetoParaProjetos.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

 

    public IActionResult Index()
    {
        var tarefas = _context.Tarefas.ToList();
        var objetivos = _context.Objetivos.ToList();
        var projetos = _context.Projetos.ToList();

        var viewModel = new TarefasObjetivosProjetosViewModel
        {
            Tarefas = tarefas,
            Objetivos = objetivos,
            Projetos = projetos
        };


        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
