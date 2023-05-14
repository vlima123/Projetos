using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoParaProjetos.Models
{
    public class Nota
    {
        public int NotaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }

        public int? ProjetoId { get; set; }
        public Projeto? Projeto { get; set; }

        public int? ObjetivosId { get; set; }
        public Objetivos? Objetivos { get; set; }

        public string Categoria { get; set; }

        public Boolean Revisao { get; set; }

        public DateTime DataRevisao { get; set; }

        public List<FlashCard> FlashCards { get; set; } = new List<FlashCard>();
        


        
    }
}