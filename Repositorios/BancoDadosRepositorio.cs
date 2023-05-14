using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoParaProjetos.context;
using ProjetoParaProjetos.Interfaces;
using ProjetoParaProjetos.Models;

namespace ProjetoParaProjetos.Repositorios
{
    public class BancoDadosRepositorio : IBancoDados
    {
 
       private readonly AppDbContext _appDbContext;

        public BancoDadosRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Projeto> Projetos => _appDbContext.Projetos;

        public List<Projeto> GetTarefas()
        {
            return _appDbContext.Projetos.ToList();
        }

        public Projeto GetTarefa(int id)
        {
            return _appDbContext.Projetos.FirstOrDefault(t => t.ProjetoId == id);
        }

        public void AddTarefa(Projeto projeto)
        {
            _appDbContext.Projetos.Add(projeto);
            _appDbContext.SaveChanges();
        }

        public void UpdateTarefa(Projeto projeto)
        {
            

            var projetoEdit = projeto;


            _appDbContext.Projetos.Update(projetoEdit);
            _appDbContext.SaveChanges();
        }

        public void DeleteTarefa(Projeto projeto)
        {
            _appDbContext.Projetos.Remove(projeto);
            _appDbContext.SaveChanges();
        }

    }
    
}