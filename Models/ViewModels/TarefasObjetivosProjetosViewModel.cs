using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoParaProjetos.Models.ViewModels
{
    public class TarefasObjetivosProjetosViewModel
    {
        public List<Tarefa> Tarefas { get; set; }
        public List<Objetivos> Objetivos { get; set; }
        public List<Projeto> Projetos { get; set; }

    }
}