using JustoNovo.Domain.ProcessosEntidades;
using Pje_WebScrapping.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justo.Entities.Entidades
{
    public class ProcessosCompromissos : Entity<int>
    {
        public ProcessosCompromissos()
        {

        }
        public string Nome { get; set; }
        public string NomeAdvogado { get; set; }
        public string NomeCliente { get; set; }
        public DateTime Data { get; set; }
        public string Obs { get; set; }
        public string Local { get; set; }
        public string Prioridade { get; set; }
        public string Status { get; set; }
        public DateTime DataProximoEvento { get; set; }
        public int ProcessoId { get; set; }
    }
}
