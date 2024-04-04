using AngleSharp.Dom;
using Pje_WebScrapping.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Models
{
    public class Processo : Entity<int>
    {
        //classe destinada a representar a entidade processo do justo novo

        public Processo()
        {
        
        }
        public string Cliente { get; set; }
        public string Advogada { get; set; }
        public string MeioDeComunicacao { get; set; }
        public string MeioDeComunicacaoData { get; set; }
        public string Prazo { get; set; }
        public string ProximoPrazo { get; set; }
        public string ProximoPrazoData { get; set; }
        public string CodPJEC { get; set; }
        public string CodPJECAcao { get; set; }
        public string UltimaMovimentacaoProcessual { get; set; }
        public string UltimaMovimentacaoProcessualData { get; set; }
        public string AdvogadaCiente { get; set; }





        public string TituloProcesso { get; set; }
        public string PartesProcesso { get; set; }

        public string TipoProcesso { get; set; }
        public string Situacao { get; set; }
        public string ComarcaInicial { get; set; }
        public string ConteudoInicial { get; set; }

        public string? ObsProcesso { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataFim { get; set; }
    }
}
