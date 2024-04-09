using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Models.Base
{
    public class Entity<T>
    {
        public virtual T Id { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public int CadastradoPor { get; set; }
        public DateTime DataAtualizacao { get; set; } =  DateTime.Now;
        public int AtualizadoPor { get; set; }
    }
}
