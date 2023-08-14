using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves
{
    public class DatasEhoras
    {
        public DateTime data;






        public void MostraDataCompleta()
        {
            DateTime Agora = DateTime.Now;

            //mostra data completa com dia da semana por extenso.
            string s1 = Agora.ToLongDateString();

            //mostra hora/mnuto/segundo
            string s2 = Agora.ToLongTimeString();



            //você estipula o tipo de formato de data que você quer
            string s3 = Agora.ToString("dd/MM/yyyy HH:mm:ss");
            


            Console.WriteLine(s1);
            Console.WriteLine(s2);
            Console.WriteLine(s3);
        }
        public void AdicionaHoras()
        {
            DateTime HoraNormal = DateTime.Now;

            //adicionamos 2 horas para a horanormal recebendo em outra variavel, da pra utilizar minutos e segundos também.
            DateTime HoraAdicionada = HoraNormal.AddHours(2);

            //adiciono 7 dias a data atual, retornando o dia e hora/minuto/segundo
            DateTime DiaAdicional = HoraNormal.AddDays(7);

            Console.WriteLine(HoraAdicionada);
            Console.WriteLine(DiaAdicional);

        }
    }
}
