using Pje_WebScrapping.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pje_WebScrapping.Models.ModelsDB;

namespace Pje_WebScrapping.Models.ModelsDB
{
    public class ProcessoDB : Entity<int>
    {
        //Cliente
        //ClienteCPF
        //Advogada
        //AdvogadaOAB
        //AdvogadaCPF
        //PoloAtivo
        //PoloPassivo
        public string Cliente { get; set; }
        public string ClienteCPF { get; set; }
        public string CodPJEC { get; set; }
        public string? ObsProcesso { get; set; }
        public DateTime? DataFim { get; set; }

        public string? MeioDeComunicacao { get; set; }
        public DateTime? MeioDeComunicacaoData { get; set; }
        public string? Prazo { get; set; }
        public string? ProximoPrazo { get; set; }
        public string? ProximoPrazoData { get; set; }
        public string? PJECAcao { get; set; }
        public string? UltimaMovimentacaoProcessual { get; set; }
        public DateTime? UltimaMovimentacaoProcessualData { get; set; }
        public string? AdvogadaCiente { get; set; }
        public string? Comarca { get; set; }
        public string? OrgaoJulgador { get; set; }
        public string? Competencia { get; set; }
        public string? MotivosProcesso { get; set; }
        public string? SegredoJustica { get; set; }
        public string? JusGratis { get; set; }
        public string? TutelaLiminar { get; set; }
        public string? Prioridade { get; set; }
        public string? Autuacao { get; set; }

        public string? TituloProcesso { get; set; }
        public string? PartesProcesso { get; set; }
        public DateTime? DataAbertura { get; set; }
        public string? ValorDaCausa { get; set; }
        public AdvogadoDB Advogado { get; set; } = new();


        public List<ProcessosCompromissosDB> ProcessosCompromissos = new List<ProcessosCompromissosDB>();

        public List<ProcessosAtualizacaoDB> ProcessosAtualizacoes { get; set; } = new List<ProcessosAtualizacaoDB>();
        public List<Polo> PoloPartes { get; set; } = new List<Polo>();
    }
}
