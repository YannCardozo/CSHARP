using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Services
{
    public class RentalService
    {
        public double Preçohora { get; set; }
        public double Diaria { get; set; }

        private ITaxService _taxService;

        //rental service agora nao instanciara mais a classe dela e instanciara ITaxService
        public RentalService(double preçohora, double diaria, ITaxService tax)
        {
            Preçohora = preçohora;
            Diaria = diaria;
            _taxService = tax;
        }
        public void ProcessInvoice(AluguelCarro aluguel)
        {
            TimeSpan duration = AluguelCarro.Finish.Subtract(aluguel.start);
            if(duration.)
        }
    }
}
