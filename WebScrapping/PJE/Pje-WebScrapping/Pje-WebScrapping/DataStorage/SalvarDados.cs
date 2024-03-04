using OpenQA.Selenium;
using Pje_WebScrapping.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.DataStorage
{
    public class SalvarDados
    {
        //classe destinada a fazer a inserção no banco 

        public static string SalvarDadosProcesso(IList<IWebElement> CabecalhoProcesso, IWebDriver driver)
        {
            if(driver != null)
            {
                if(CabecalhoProcesso.Count > 0) 
                {


                    IList<IWebElement> ConteudoDasDivsProcessoAberto = new List<IWebElement>();
                    foreach (var elemento in CabecalhoProcesso)
                    {
                        // Encontra todas as divs dentro do elemento atual e adiciona à lista ConteudoDasDivsProcessoAberto
                        IList<IWebElement> divsInternas = elemento.FindElements(By.TagName("div"));
                        foreach (var div in divsInternas)
                        {
                            //insere todas as divs dentro do elemento TD referente ao processo
                            ConteudoDasDivsProcessoAberto.Add(div);
                        }
                    }
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n Testando SALVARDADOSPROCESSOAGORA");
                    foreach (var ConteudoDivDoProcesso in ConteudoDasDivsProcessoAberto)
                    {
                        Console.WriteLine(ConteudoDivDoProcesso.Text);
                    }
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n");
                }
            }
            return "Erro em SalvarDadosProcesso";
        }





        //public static string SalvarNumeroProcesso(string NumeroProcesso, IWebDriver driver)
        //{
        //    Console.WriteLine("\n\n\n\n\n\n");
        //    Console.WriteLine("Nº do processo salvo: " + NumeroProcesso);
        //    return "";
        //}

        public static void SalvarMovimentacaoProcessual(IList<IWebElement> ListaDeMovimentacaoProcessual,
            IWebDriver driver)
        {
            int ponto_de_parada = 0;
            //instanciar um novo perfil de advogado e salvar usando o perfil dele no banco?


            //objeto instanciado da div BRUTA da movimentaão processual, TODOS OS ELEMENTOS AQUI:
            IList<IWebElement> HistoricoDeProcessos = ListaDeMovimentacaoProcessual;

            //IList<IWebElement> QuadradoDaMovimentacao = driver.FindElements(By.ClassName("media-body.box"));
            //IList<IWebElement> DatasDaMovimentacao = driver.FindElements(By.ClassName("mediadata"));




            //como fazer buscar pelo classname que contenha esse nome de classe media interno tipo


            //IList<IWebElement> QuadradoDaMovimentacao = driver.FindElements(By.ClassName().ToString().Contains);



            //na hora de inserir no banco preciso fazer da seguinte forma:
            //fazer lista com media interno tipo ( que são as divs com todas as descrições da movimentação )
            //fazer lista com media data para puxar as datas da movimentação que ficam acima das divs.

            int roda = 0;

            Console.WriteLine("\n\n\n\n\n\n");
            foreach (var stringonamovimentacao in HistoricoDeProcessos)
            {
                string texto = stringonamovimentacao.Text.ToString();
                int comprimentoTotal = texto.Length;

                // Calcula o tamanho de cada parte
                int tamanhoParte = comprimentoTotal / 3;

                // Divide a string em três partes
                string parte1 = texto.Substring(0, tamanhoParte);
                string parte2 = texto.Substring(tamanhoParte, tamanhoParte);
                string parte3 = texto.Substring(2 * tamanhoParte, tamanhoParte);

                // Imprime as partes
                Console.WriteLine("Parte 1: " + parte1);
                Console.WriteLine("\n\n\n");
                Console.WriteLine("Parte 2: " + parte2);
                Console.WriteLine("\n\n\n");
                Console.WriteLine("Parte 3: " + parte3);
                Console.WriteLine("\n\n\n");



                //roda++;
                //Console.WriteLine("Começando a imprimir o que tem na movimentação processual");
                //ActionsPJE.AguardarPje("Baixo");
                ////imprime TODA A MOVIMENTAÇÃO PROCESSUAL
                //Console.WriteLine("\n +++ "  + stringonamovimentacao.TagName);
                //Console.WriteLine("SalvarMovimentacaoProcessual, iten: " + ponto_de_parada + "  " + stringonamovimentacao.Text);
                //Console.WriteLine("Total : " + stringonamovimentacao.ToString().Length);
                //foreach (var teste in stringonamovimentacao.Text)
                //{

                //}


                //if(HistoricoDeProcessos.Count - 1 == ponto_de_parada)
                //{
                //    Console.WriteLine("Finalizei a lista");
                //}
                //ponto_de_parada++;

            }
            ponto_de_parada = 0;

            Console.WriteLine("lista finalizada com um total de: " + HistoricoDeProcessos.Count + " registros");
            Console.WriteLine("Roda rodou: " + roda);
            //Console.WriteLine("lista finalizada com um total de: " + QuadradoDaMovimentacao.Count + " registros");
            //Console.WriteLine("lista finalizada com um total de: " + DatasDaMovimentacao.Count + " registros");
        }



    }
}
