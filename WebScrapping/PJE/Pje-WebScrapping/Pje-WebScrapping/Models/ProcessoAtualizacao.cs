using AngleSharp.Dom;
using Pje_WebScrapping.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Models
{
    public class ProcessoAtualizacao : Entity<int>
    {
        //classe destinada a representar a classe processoatualizacao do justo novo
        //para atualizar a base de dados
        public ProcessoAtualizacao()
        {
        
        }

        public int? ProcessoId { get; set; }
        public string CodPJEC { get; set; }

        //as vezes só vai ter um titulo e não um conteudo, verificar "media interno tipo-D"
        public string? ConteudoAtualizacao { get; set; }
        public string? TituloMovimento { get; set; }
        public DateOnly DataMovimentacao { get; set; }
    }
}
