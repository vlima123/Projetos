using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoParaProjetos.Models;

namespace ProjetoParaProjetos.Interfaces
{
    public interface IBancoDados
    {
        public IEnumerable<Projeto> Projetos { get;}


        public List<Projeto> GetTarefas();
        public Projeto GetTarefa(int id);
        public void AddTarefa(Projeto projeto);
        public void UpdateTarefa(Projeto projeto);
        public void DeleteTarefa(Projeto projeto);
        
    }
}