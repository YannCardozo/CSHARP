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

            foreach (var iten in HistoricoDeProcessos)
            {
                Console.WriteLine("Começando a imprimir o que tem na movimentação processual");
                ActionsPJE.AguardarPje("Baixo");
                Console.WriteLine("SalvarMovimentacaoProcessual, iten: " + ponto_de_parada + "  " + iten.Text);
                if(HistoricoDeProcessos.Count - 1 == ponto_de_parada)
                {
                    Console.WriteLine("Finalizei a lista");
                }

                ponto_de_parada++;

            }
            ponto_de_parada = 0;
            //foreach (var iten in QuadradoDaMovimentacao)
            //{
                
            //    Console.WriteLine("Item: " + ponto_de_parada + " " + iten.Text);

            //    ponto_de_parada++;
            //}



            //ponto_de_parada = 0;
            //foreach (var iten in DatasDaMovimentacao)
            //{
            //    Console.WriteLine("Item: " + ponto_de_parada + " " + iten.Text);

            //    ponto_de_parada++;

            //}

            Console.WriteLine("lista finalizada com um total de: " + HistoricoDeProcessos.Count + " registros");
            //Console.WriteLine("lista finalizada com um total de: " + QuadradoDaMovimentacao.Count + " registros");
            //Console.WriteLine("lista finalizada com um total de: " + DatasDaMovimentacao.Count + " registros");
        }



    }
}
