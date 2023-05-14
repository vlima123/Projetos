using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoParaProjetos.Models
{
    public class Tarefa
    {
        public int TarefaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataFinal { get; set; }
        public string Status { get; set; }

        public string Prioridade { get; set; }
        public string Categoria { get; set; }

    }
}