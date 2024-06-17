using Pje_WebScrapping.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Models
{
    public class Advogado : Entity<int>
    {
        public string? Nome { get; set; }
        public string? Oab { get; set; }
        public string? Cpf { get; set; }
    }
}
