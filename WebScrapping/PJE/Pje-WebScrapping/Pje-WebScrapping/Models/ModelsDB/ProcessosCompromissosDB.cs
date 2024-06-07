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
    public class ProcessosCompromissosDB : Entity<int>
    {
        public string NomeAdvogado { get; set; }
        public string NomeCliente { get; set; }
        public DateTime Data { get; set; }
        public string Obs { get; set; }
        public string Local { get; set; }
        public string Prioridade { get; set; }
        public string Status { get; set; }
        public DateTime DataProximoEvento { get; set; }
        public int ProcessoId { get; set; }
        public Processo Processo { get; set; }
    }
}
