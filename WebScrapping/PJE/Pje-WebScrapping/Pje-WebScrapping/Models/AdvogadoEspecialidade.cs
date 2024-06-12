using AngleSharp.Dom;
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
    [Table("AdvogadoEspecialidade")]
    public class AdvogadoEspecialidade : Entity<int>
    {
        public string? NomeAreaDireito { get; set; }
        public int? AdvogadoId { get; set; }
    }


}
