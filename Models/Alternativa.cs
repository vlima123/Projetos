using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoParaProjetos.Models
{
    public class Alternativa
    {
        public int AlternativaId { get; set; }

        public string Texto { get; set; }

        public int QuestaoId { get; set; }

        public Questao Questao { get; set; }
    }
}