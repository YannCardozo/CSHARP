using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wattpad.DTO
{
    public class WattpadHomeDTO
    {
        public WattpadHomeDTO() 
        { 
        
        }

        public WattpadHomeDTO(string conteudo, string taghtml)
        {
            Conteudo = conteudo;
            Taghtml = taghtml;
        }
        public string Conteudo { get; set; }
        public string Taghtml { get; set; }

    }
}
