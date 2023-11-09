﻿using AngleSharp.Css.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pje_WebScrapping.Actions.NavBarMenu
{
    public class NavBarActions
    {
        private static IWebDriver driver;
        public static void AcessarMenuNavPJE(IWebDriver driver)
        {
            //acessa o menu de navegação
            IWebElement NavMenu = driver.FindElement(By.XPath("//*[@id=\"barraSuperiorPrincipal\"]/div/div[1]/ul/li/a"));
            Thread.Sleep(1000);
            Console.WriteLine("Cliquei no menu navbar!");
            NavMenu.Click();
        }

        public static void ListaMenuOpcoesNavBar(IWebDriver driver, string AreaDoMenu)
        {
            string textoCompleto = "";
            IWebElement OpcaoMenuNavBar = driver.FindElement(By.XPath("//*[@id='menu']/div[2]/ul"));

            IList<IWebElement> ListaDosLi = OpcaoMenuNavBar.FindElements(By.TagName("li"));
            Console.WriteLine("Temos " + ListaDosLi.Count + " elementos LI");

            //opções dentro do menu navbar:
            //PAINEL
            //PROCESSOS
            //ATIVIDADES
            //AUDIÊNCIAS E SESSÕES
            //CONFIGURAÇÃO
            string MetodoFormado = "";

            foreach (var elementoLi in ListaDosLi)
            {

                if (elementoLi.Text.Contains(AreaDoMenu))
                {
                    elementoLi.Click();
                    Console.WriteLine("Opção selecionada: " + AreaDoMenu);
                    Thread.Sleep(1000);
                    break;
                }
            }

            //criando a referenciacao ao metodo de forma GENERICA
            MetodoFormado = AreaDoMenu + "ACTION";
            //com base na chamada da main em  ActionsPJE.ListaMenuOpcoesNavBar(Driver,"PAINEL"); , ira formar com strings o metodo a ser chamado e depois instanciara a variavel metodo recebendo o metodo formado por string.
            MethodInfo metodo = typeof(NavBarActions).GetMethod(MetodoFormado, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);



            if (metodo != null)
            {
                Console.WriteLine("indo para: " + MetodoFormado + " (painel do representante processual)");
                if (metodo.IsStatic)
                {
                    //aqui invocará o metodo.
                    Thread.Sleep(1000);
                    metodo.Invoke(null, new object[] { driver });
                }
            }
            else
            {
                Console.WriteLine("Método: " + MetodoFormado + " não encontrado.");
            }
        }

        //metodo que vai acessar o painel representante processual em PAINEL

        public static void PAINELACTION(IWebDriver driver)
        {
            //PAINEL -> PAINEL DO REPRESENTANTE PROCESSUAL
            Console.WriteLine("Redirecionando para painel do representante processual, carregando página...");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"menu\"]/div[2]/ul/li[1]/div/ul/li/a")).Click();

            Thread.Sleep(3000);

            IWebElement menupainelrepresentanteprocessual = driver.FindElement(By.XPath("//*[@id=\"formAbaExpediente:divMenuContexto\"]"));

            IList<IWebElement> contadorderegistrosrepresentanteprocessual = menupainelrepresentanteprocessual.FindElements(By.TagName("span"));


            IList<IWebElement> linkscomnotificacao = menupainelrepresentanteprocessual.FindElements(By.TagName("a"));

            if(linkscomnotificacao.Count > 0)
            {
                for (int i = 0; i < linkscomnotificacao.Count; i++)
                {

                    Console.WriteLine("texto: " +linkscomnotificacao[i].Text);
                    Console.WriteLine("Nome da Tag: " + linkscomnotificacao[i].TagName);
                    //aqui clica nos links tag <a> com notificação.
                    linkscomnotificacao[i].Click();
                    Thread.Sleep(3500);
                    Console.WriteLine("cliquei");


                    //IWebElement elementoComXmlns = driver.FindElement(By.XPath("//*[@xmlns='http://www.w3.org/1999/xhtml']"));
                    IWebElement elementoComXmlns = driver.FindElement(By.ClassName("rich-tree-node"));

                    //recebe a lista de todos os elementos <td> de dentro da table ( menu lateral, apos apertado o click do linkscomnotificacao )
                    IList<IWebElement> verificando = elementoComXmlns.FindElements(By.TagName("td"));

                    //instanciando o objeto que receberá TODAS AS TABELAS E TRANSPORTARÁ OS DADOS:
                    List<IWebElement> listaDeTabelas = new List<IWebElement>();

                    //cria outro for para percorrer todos os elementos selecionados de acordo com a notificação
                    for (int j = 0; j < verificando.Count; j++)
                    {
                        IWebElement testea = verificando[j];
                        Console.WriteLine("Aqui está o elemento tagname TD: " + testea.Text);

                        if (!string.IsNullOrEmpty(testea.Text))
                        {
                            Console.WriteLine("Não sou null e tenho o valor de: " + testea.Text);


                            // Verificar se é o último elemento
                            if (j == verificando.Count - 1)
                            {
                                Console.WriteLine("Este é o último elemento TD, sendo: " + testea.Text);
                                testea.Click();
                                Thread.Sleep(2000);
                                //retorna toda a tabela desejada para cá:
                                IList<IWebElement> Tabela = driver.FindElements(By.Id("divListaExpedientes"));

                                //insere novos elementos na lista ListaDeTabelas ( tables de registros que encontre )
                                listaDeTabelas.AddRange(Tabela);
                                foreach (var tabela in listaDeTabelas)
                                {
                                    Console.WriteLine(tabela.Text);
                                    Console.WriteLine(tabela.ToString());
                                    Console.WriteLine("Tabela é uma: " + tabela.TagName);
                                }
                                //instancia a lista que receberá todos os links <a> dos processos dentro da 
                                //divListExpedientes, ( que abre uma nova janela )
                                IList<IWebElement> linkmovimentacaoprocessual = driver.FindElements(By.ClassName("numero-processo-expediente"));

                                //recebe o link da pagina, antes de abrir outra janela
                                string Janela_Principal = RetornarParaJanelaPrincipal(driver);

                                foreach (var linkprocesso in linkmovimentacaoprocessual)
                                {
                                    Console.WriteLine(linkprocesso.Text);
                                    Console.WriteLine(linkprocesso.TagName);
                                    linkprocesso.Click();
                                    Thread.Sleep(2000);
                                    Console.WriteLine(Janela_Principal);

                                    //driver.windowhandles é a propriedade em lista que faz a verificação de janelas
                                    //abertas
                                    foreach (var NOVA_JANELA in driver.WindowHandles)
                                    {
                                        //janela principal é o link do navegador antes dele abrir o link ( entrar no foreach )
                                        if (NOVA_JANELA != Janela_Principal)
                                        {
                                            //você muda o ponteiro do objeto driver para a nova_janela
                                            driver.SwitchTo().Window(NOVA_JANELA);

                                            // Verifique se a URL da nova janela corresponde à URL desejada, essa url é a da NOVA JANELA
                                            if (driver.Url.Contains("https://tjrj.pje.jus.br/1g/Processo/ConsultaProcesso/Detalhe/listProcessoCompletoAdvogado.seam"))
                                            {
                                                // Você está na janela desejada, execute ações necessárias aqui
                                                Console.WriteLine("Mudei para: " + driver.Title);
                                                Thread.Sleep(2000);

                                                IList<IWebElement> movimentacaoprocessual = driver.FindElements(By.Id("divTimeLine:eventosTimeLineElement"));

                                                foreach (var registro in movimentacaoprocessual)
                                                {
                                                    Console.WriteLine(registro.Text);

                                                }




                                                //continuar a desenvolver aqui e melhorar os métodos
                                                driver.Close();
                                                Console.WriteLine("Fechei a janela nova.");
                                                driver.SwitchTo().Window(Janela_Principal);
                                                Console.WriteLine("Mudei para: " + driver.Title);

                                                
                                            }
                                        }
                                    }
                                    //https://tjrj.pje.jus.br/1g/Processo/ConsultaProcesso/Detalhe/listProcessoCompletoAdvogado.seam
                                    //linkprocesso.Click() abrirá uma nova janela no navegador


                                    //url principal https://tjrj.pje.jus.br/1g/Painel/painel_usuario/advogado.seam
                                    //url nova janela https://tjrj.pje.jus.br/1g/Processo/ConsultaProcesso/Detalhe/listProcessoCompletoAdvogado.seam





                                    //agora abrindo o link e armazenando toda a MOVIMENTAÇÃO PROCESSUAL


                                    //puxar desse ID a movimentacao processual do link aberto : divTimeLine:eventosTimeLineElement
                                }

                            }
                        }
                    }
                }
            }else
            {
                Console.WriteLine("Não há atualizações no painel de representante processual.");
            }







        }


        //public static void PAINELACTIONPROCESSOS(IWebDriver driver, IList<IWebElement> spanscomnotificacao)
        //{
        //    foreach (var iten in spanscomnotificacao)
        //    {

        //    }

        //}


        public static string RetornarParaJanelaPrincipal(IWebDriver driver)
        {
            string endereço_do_navegador = driver.CurrentWindowHandle;
            return endereço_do_navegador;
        }






    }

}
