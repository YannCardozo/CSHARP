using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves
{
    public class Listas
    {
        public void DeclarandoListasComImprimindo()
        {

            //trabalhando com LISTAS
            List<string> ListaDeString2 = new List<string>() { "alfa", "beta", "gama" };

            //imprimindo uma lista
            foreach(string dado in ListaDeString2)
            {
                Console.WriteLine(dado);
            }

            //outra forma de imprimir uma lista , nesse caso você junta toda a lista em uma string e a separa todos os elementos por uma virgula.
            string listaComoString = string.Join(", ", ListaDeString2);
            Console.WriteLine(listaComoString);
            //Environment.Exit(1);
         }
        public void ContandoListas(List<string> lista)
        {   
            //contando quantos elementos tem na lista
            Console.WriteLine(lista.Count());

        }
        public static void EncontrandoItenNaLista(List<string> lista)
        {
            Verifica("s");
            static bool Verifica(string s)
            {
                return true;
            }
        }
    }
}
