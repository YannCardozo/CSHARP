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
            if (saida <= chegada)
            {
                throw new Exception("Checkout precisa ser depois do checkin");
                //return "erro";
            }
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
            DateTime now = DateTime.Now;


            if (Checkin <= now || Checkout <= now)
            {

                throw new Exception("Check-in precisa ser data futura e checkout tb");
               // return "erro";

            }
            else if (Checkout <= Checkin)
            {
                throw new Exception("Checkout precisa ser depois do checkin");
                //return "erro";
            }
            checkin = Checkin;
            checkout = Checkout;

           // return null;
        }
        public override string ToString()
        {
            return "Quarto " + Numero_UH + ", entrada de : " + checkin.ToString("dd/mm/yy") + ", saida de: " + checkout.ToString("dd/mm/yy") + ", sua estadia foi de: " + Duracao() + " noites"  ;
        }

    }
}
