using EstudoCsharpNelioAlves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

            //transformar uma lista em uma string separadas por virgula
            string listaComoString = string.Join(", ", ListaDeString2);
            Console.WriteLine(listaComoString);

            ListaDeString2.Add("Maria");
            ListaDeString2.Add("Joao");
            ListaDeString2.Add("Marcos");
            ListaDeString2.Insert(3, "NAtali Marques");
            Console.WriteLine("--------------------------------");
            foreach (string unidade in ListaDeString2)
            {
                Console.WriteLine(unidade);
            }


            //contando quantos elementos tem na lista agora:
            ContandoListas(ListaDeString2);
            EncontrandoItenNaLista(ListaDeString2);
            //parar programa:
            //Environment.Exit(1);
        }
        public void ContandoListas(List<string> lista)
        {   
            //contando quantos elementos tem na lista
            Console.WriteLine("A lista tem: " + lista.Count());

        }
        public static void EncontrandoItenNaLista(List<string> lista)
        {
            lista.Find(x => x.Contains("M"));
            foreach(string achei in lista)
            {
                Console.WriteLine("Encontrados: " + achei);
            }
        }
    }
}
