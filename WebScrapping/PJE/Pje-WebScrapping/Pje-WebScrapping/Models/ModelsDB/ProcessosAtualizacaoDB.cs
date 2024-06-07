using AngleSharp.Dom;
using Pje_WebScrapping.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Models.ModelsDB
{
    public class ProcessosAtualizacaoDB : Entity<int>
    {
        public string CodPJEC { get; set; }
        public string? ConteudoAtualizacao { get; set; }
        public string? TituloMovimento { get; set; }
        public DateTime? DataMovimentacao { get; set; }


        public int ProcessoId { get; set; }
        public Processo Processo { get; set; }
    }
}
