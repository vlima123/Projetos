using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoParaProjetos.Models
{
    public class Objetivos
    {
        public int ObjetivosId { get; set; }
        public string Objetivo { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }

        public List<FlashCard> FlashCards { get; set; } = new List<FlashCard>();

        
    }
}