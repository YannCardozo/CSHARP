using Microsoft.Win32;
using OpenQA.Selenium;
using Pje_WebScrapping.DataStorage;
using Pje_WebScrapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Actions.Push
{
    public static class Push
    {

        public static IWebDriver PushProcessos(IWebDriver PushDriver)
        {
            IWebDriver PushWebDriver = PushDriver;

            //antes de começar a verificar os processos, precisamos ter acesso ao LINK ATUAL da janela que estamos,
            //por isso instanciamos o endereço da janela que estamos antes de começar a abrir outros links de outras janelas
            string Janela_Principal = ActionsPJE.RetornarParaJanelaPrincipal(PushWebDriver);

            ActionsPJE.AguardarPje("Baixo");

            for(int i = 0; i<10; i++)
            {
                ActionsPJE.DescerBarraDeRolagem(PushWebDriver, "j_id169:j_id173");
            }


            IWebElement TabelaProcessos = PushWebDriver.FindElement(By.Id("j_id169:j_id173:tb"));
            List<IWebElement> TRSdaTabelaProcessos = TabelaProcessos.FindElements(By.TagName("tr")).ToList();





            //começamos a obter os processos e formar o objeto processo a partir daqui:
            foreach (var registro in TRSdaTabelaProcessos)
            {

                ActionsPJE.AguardarPje("Baixo");

                for (int i = 0; i < 10; i++)
                {
                    ActionsPJE.DescerBarraDeRolagem(PushWebDriver, "j_id169:j_id173");
                }



                //instanciando objetos da coluna a receber.
                //1 processo , 2 data de inclusao , 3 observacao
                List<IWebElement> TdsDentroDoTr = registro.FindElements(By.TagName("td")).ToList();
                if(TdsDentroDoTr.Count > 0 && TdsDentroDoTr != null)
                {
                    //BotaoDeMovimentacaoPROCESSUAL Dentro do TD
                    var BotaoLinkMovimentacaoProcessual = TdsDentroDoTr[0].FindElements(By.TagName("a")).ToArray();

                    //Processso
                    var numeroprocesso = TdsDentroDoTr[1].Text;

                    // Data de inclusao
                    var data_inclusao = ETLDataInclusao(TdsDentroDoTr[2].Text);


                    //Observacao
                    var observacao = ETLObservacao(TdsDentroDoTr[3].Text.ToString());

                    Console.WriteLine($"nº: {numeroprocesso}; data: {data_inclusao}; observacao: {observacao}");

                    Processo ProcessoFormadoPush = new()
                    {
                        Cliente = observacao,
                        CodPJEC = numeroprocesso,
                        DataAberturaDATETIME = data_inclusao,
                    };

                    //INICIAREMOS MOVIMENTACAO PROCESSUAL AQUI:
                    ActionsPJE.AguardarPje("Baixo");
                    BotaoLinkMovimentacaoProcessual[0].Click();
                    ActionsPJE.AguardarPje("Medio");
                    PushMovimentacaoProcessual(PushWebDriver, Janela_Principal);
                }
                else
                {
                    Console.WriteLine("Sem processos");
                    return PushWebDriver;
                }
            }
            //TabelaProcessos.FindElements(By.TagName("tr"));


            // essa tabela TERÁ QUEBRA DE PÁGINA.
            //vai quebbrar em algum momento aqui, ficar atento quanto a isso.
            return PushWebDriver;
        }

























        public static void PushMovimentacaoProcessual(IWebDriver driver, string Janela_Principal)
        {
            foreach (var NOVA_JANELA in driver.WindowHandles)
            {
                //janela principal é o link do navegador antes dele abrir o link ( entrar no foreach )
                if (NOVA_JANELA != Janela_Principal)
                {
                    //você muda o ponteiro do objeto driver para a nova_janela
                    driver.SwitchTo().Window(NOVA_JANELA);

                    // Verifique se a URL da nova janela corresponde à URL desejada, essa url é a da NOVA JANELA
                    //aqui abrirá para a janela do processo aberto
                    if (driver.Url.Contains("https://tjrj.pje.jus.br/1g/Processo/ConsultaProcesso/Detalhe/listProcessoCompletoAdvogado.seam"))
                    {
                        // Você está na janela desejada, execute ações necessárias aqui
                        Console.WriteLine("Mudei para: " + driver.Title);
                        ActionsPJE.AguardarPje("Baixo");

                        IList<IWebElement> movimentacaoprocessual = driver.FindElements(By.Id("divTimeLine:eventosTimeLineElement"));

                        Console.WriteLine("\n\n\n\n\n O que é salvar dados processo: \n\n");




                        Console.WriteLine("\n\n\n\n\n O que é movimentação processual: \n\n");
                        //aqui inicia o webscrapping para armazenar a movimentação processual do processo.



                        //SalvarDados.SalvarMovimentacaoProcessual(driver, ProcessoRetornado);






                        //ActionsPJE.EncerrarConsole();
                        //Processo VerificaProcesso = new();
                        //VerificaProcesso = ConnectDB.LerProcesso(ProcessoRetornado.CodPJEC);
                        //if (VerificaProcesso != null)
                        //{

                        //}



                        //salvar processo inicial
                        //SalvarDados.SalvarDadosProcesso(ConteudoProcessoAberto, linkmovimentacaoprocessual, driver);

                        //tentando entender o que é movimentacaoprocessual

                        foreach (var registro in movimentacaoprocessual)
                        {
                            Console.WriteLine(registro.Text);

                        }
                        Console.WriteLine("\n\n\n");




                        //continuar a desenvolver aqui e melhorar os métodos
                        ActionsPJE.AguardarPje("Baixo");
                        driver.Close();
                        Console.WriteLine("Fechei a janela nova.");
                        driver.SwitchTo().Window(Janela_Principal);
                        Console.WriteLine("Mudei para: " + driver.Title);


                    }
                }
            }
        }














        //Espaço para Extract Transform Load >>>>>
        public static string ETLObservacao(string ObservacaoSuja)
        {
            if (string.IsNullOrEmpty(ObservacaoSuja))
            {
                Console.WriteLine("observacao VAZIA");
                return null;
            }

            string ObservacaoLIMPA = ObservacaoSuja.Replace("AUTOR: ", "");
            ObservacaoLIMPA = ObservacaoLIMPA.Replace("RÉU: ", "");

            return ObservacaoLIMPA;
        }

        public static DateTime ETLDataInclusao(string data_inclusao_string)
        {
            DateTime data_inclusao;
            if (DateTime.TryParseExact(data_inclusao_string, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out data_inclusao))
            {
                return data_inclusao;
            }
            else
            {
                // Tratamento de erro, caso a conversão falhe
                throw new FormatException("Formato de data inválido");
            }
        }


    }
}
