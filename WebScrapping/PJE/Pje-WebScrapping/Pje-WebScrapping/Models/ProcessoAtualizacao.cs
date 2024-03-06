﻿using AngleSharp.Dom;
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

        public string TituloPjeMovimentacaoProcessual { get; set; }
        public string MovimentacaoProcessual { get; set; }
        public string JulgamentoStatus { get; set; }
        public DateTime DataVencimentoProcessual { get; set; }
        public string Conteudo { get; set; }
        public string? LinkArquivos { get; set; }
        public string? TipoDeAcao { get; set; }
        public string? OrgaoJulgador { get; set; }
        public DateTime MarcacaoDeData { get; set; }
        public string ComarcaAtual { get; set; }
        public bool StatusProcesso { get; set; }
        public int ProcessoId { get; set; }
    }
}
