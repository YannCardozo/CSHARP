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
            //procura incidencia na lista declarada com o método .Find
            lista.Find(x => x.Contains("M"));
            foreach(string achei in lista)
            {
                Console.WriteLine("Encontrados: " + achei);
            }
            //Console.WriteLine(lista.ToString());
            //Console.WriteLine("F");

            //como encontrar a primeira incidência de "padrão" ?
            //se retornar NULL , é pq não achou. Retorna a primeira string que inicie com a Letra M
            string encontrandoprimeiro = lista.Find(x => x[0] == 'M');
            Console.WriteLine(encontrandoprimeiro);

            //vai encontrar a ultima incidencia na lista e te retornar,
            //em caso de nao encontrar sera retornado um null.
            string encontrandoultimo = lista.FindLast(x => x[0] == 'M');
            Console.WriteLine(encontrandoultimo);

            //encontrando posição no vetor em que primeira incidência é a letra M
            //retorna um int equivalente a posição no vetor
            int encontrandoposiçãonovetor = lista.FindIndex(x => x[0] == 'M');
            Console.WriteLine("Posição encontrada em: " + encontrandoposiçãonovetor);

            //encontrando a ultima incidencia no vetor em que apareça a letra M
            int encontrandoultimaposicaonovetor = lista.FindLastIndex(x => x[0] == 'M');
            Console.WriteLine("Ultima incidencia da letra M no vetor foi em: " + encontrandoultimaposicaonovetor);

            //criando uma nova lista para receber toda a busca do método de um FindAll
            List<string> lista2 = lista.FindAll(x => x.Length > 5);
            Console.WriteLine("---------------");
            foreach(var incidencialista2 in lista2)
            {
                Console.WriteLine(incidencialista2);
            }
            RemovendoElemento(lista);
            Console.WriteLine("-------------------");
            foreach(var verificando in lista)
            {
                Console.WriteLine(verificando);
            }
        }
        public static void RemovendoElemento(List<string> removendo)
        {
            removendo.Remove("Alex");
            removendo.RemoveAll(x => x.Length < 5);
        }
        public static void RemovendoVariosElementosEmIntervaloEspecifico(List<string> removendo)
        {
            //vai remover 3 elementos a partir do indice numero 2 do vetor de string
            removendo.RemoveRange(2, 3);
        }
        
    }
}
