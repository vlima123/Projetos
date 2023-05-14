using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoParaProjetos.Models
{
    public class FlashCard
    {
        public int FlashCardId { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public string Titulo { get; set; }


        public int ObjetivosId { get; set; }
    
        public int NotaId { get; set; }
    

    
        public int ProjetoId { get; set; }

        public string Tipo { get; set; }

        public string Categoria { get; set; }



    }
}