using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Models
{
    public class Reservas
    {
        public int Numero_UH { get; set; }
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }

        public Reservas()
        {

        }

        public Reservas(int numero, DateTime chegada, DateTime saida)
        {
            Numero_UH = numero;
            checkin = chegada;
            checkout = saida;
        }
           
        public int Duracao()
        {
            TimeSpan duracao = checkout.Subtract(checkin);
            return (int)duracao.TotalDays;

        }
        public void AtualizaData(DateTime Checkin , DateTime Checkout)
        {
            checkin = Checkin;
            checkout = Checkout;
        }
        public override string ToString()
        {
            return "Quarto " + Numero_UH + ", entrada de : " + checkin.ToString("dd/mm/yy") + ", saida de: " + checkout.ToString("dd/mm/yy") + ", sua estadia foi de: " + Duracao() + " noites"  ;
        }

    }
}
