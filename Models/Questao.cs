using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoParaProjetos.Models
{
    public class Questao
    {
        public int QuestaoId { get; set; }

        public string Enunciado { get; set; }

        public string RespostaCorreta { get; set; }


        public List<Alternativa> Alternativas { get; set; } = new List<Alternativa>();

    }
}