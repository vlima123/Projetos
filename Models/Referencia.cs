using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoParaProjetos.Models
{
    public class Referencia
    {
        public int ReferenciaId { get; set; }
        public string Tipo { get; set; }

        public FlashCard FlashCard { get; set; }

        public int FlashCardId { get; set; }

        public int ReferenciaTipo { get; set; }
        
    }
}