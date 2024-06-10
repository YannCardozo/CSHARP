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
        public string ClienteCPF { get; set; }
        public string Advogada { get; set; }
        public string AdvogadaOAB { get; set; }
        public string AdvogadaCPF{ get; set; }
        public string MeioDeComunicacao { get; set; }
        //datetime MeioDeComunicacaoData
        public string MeioDeComunicacaoData { get; set; }
        public string Prazo { get; set; }
        public string ProximoPrazo { get; set; }
        public string ProximoPrazoData { get; set; }
        public string CodPJEC { get; set; }
        public string CodPJECAcao { get; set; }
        public string UltimaMovimentacaoProcessual { get; set; }
        //datetime UltimaMovimentacaoProcessualData
        public string UltimaMovimentacaoProcessualData { get; set; }
        public string AdvogadaCiente { get; set; }


        //detalhes:

        public string Comarca { get; set; }
        public string OrgaoJulgador { get; set; }
        public string Competencia { get; set; }
        //assunto
        public string MotivosProcesso { get; set; }
        public string ValorCausa { get; set; }
        public string SegredoJustica { get; set; }
        public string JusGratis { get; set; }
        public string TutelaLiminar { get; set; }
        public string Prioridade { get; set; }
        public string Autuacao { get;set; }





        public Polo PoloAtivo { get; set; }
        public Polo PoloPassivo { get; set; }



        //colocar em movimentacaoprocessual
        //public bool SegredoJus { get; set; }


        public string TituloProcesso { get; set; }
        public string PartesProcesso { get; set; }
        public string ComarcaInicial { get; set; }

        public string? ObsProcesso { get; set; }
        //datetime DataAbertura
        public DateOnly DataAbertura { get; set; } = DateOnly.MinValue;
        //datetime DATAFIM
        public DateOnly DataFim { get; set; } = DateOnly.MinValue;
    }
}
