using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtratorPlanilha
{
    public class Cep
    {
        public int? id_bairro { get; set; }

        public string bairro { get; set; }

        public string cidade { get; set; }

        public string uf { get; set; }

        public string cod_ibge { get; set; }

        public double? LATITUDE { get; set; }

        public double? LONGITUDE { get; set; }

    }
}
