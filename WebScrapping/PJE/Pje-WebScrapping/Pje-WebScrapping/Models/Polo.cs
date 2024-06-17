using AngleSharp.Dom;
using Pje_WebScrapping.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Models
{
    public class Polo : Entity<int>
    {
        public Polo()
        {

        }
        public int ProcessoId { get; set; }
        public string? NomeParte { get; set; }
        public string? TipoParte { get; set; } = "não informado";
        public string? CPFCNPJParte { get; set; }
        public string? NomeAdvogado { get; set; } = "Não informado";
        public string? CPFAdvogado { get; set; } = "Não informado";
        public string? OAB { get; set; } = "Não informado";
    }
}
