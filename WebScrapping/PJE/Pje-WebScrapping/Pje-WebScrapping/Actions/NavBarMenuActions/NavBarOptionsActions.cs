﻿using OpenQA.Selenium;
using Pje_WebScrapping.Actions.NavBarMenu;
using Pje_WebScrapping.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Actions.NavBarMenuActions
{
    public class NavBarOptionsActions
    {
        //destinado a todas as ações que forem criadas seguindos seus métodos

        public static void PAINELACTION(IWebDriver driver)
        {
            //PAINEL -> PAINEL DO REPRESENTANTE PROCESSUAL
            Console.WriteLine("Redirecionando para painel do representante processual, carregando página...");
            ActionsPJE.AguardarPje("Baixo");

            //clica no botão que redireciona para o painel do representante processual dentro de painel
            driver.FindElement(By.XPath("//*[@id=\"menu\"]/div[2]/ul/li[1]/div/ul/li/a")).Click();

            ActionsPJE.AguardarPje("Medio");

            //recebe a div que contém os elementos a serem capturados, caso haja.
            IWebElement menupainelrepresentanteprocessual = driver.FindElement(By.XPath("//*[@id=\"formAbaExpediente:divMenuContexto\"]"));


            //apagar esse objeto instanciado, sem necessidade
            //IList<IWebElement> contadorderegistrosrepresentanteprocessual = menupainelrepresentanteprocessual.FindElements(By.TagName("span"));

            //instancia a lista que irá receber todas as tags <a></a> a partir da div que contém todas as tags <a>
            //, será tag <a> quando tiver alguma notificação de processo para o advogado apenas, quando não tiver ficará como uma <div> normal.
            //observe que o if linkscomnotificacao.Count fará a verificação se a captura da lista possui algum elemento ou não, se existe notificação ou não. Caso não tenha imprimirá na tela a mensagem que nao tem.

            IList<IWebElement> linkscomnotificacao = menupainelrepresentanteprocessual.FindElements(By.TagName("a"));

            if (linkscomnotificacao.Count > 0)
            {

                //entrei no if é porque existem notificaçãoes , ou seja, tags <a> com movimentação processual capturadas a serem verificadas
                for (int i = 0; i < linkscomnotificacao.Count; i++)
                {
                    //imprime na tela o texto da tag <a> que esta sendo lida
                    Console.WriteLine("texto: " + linkscomnotificacao[i].Text);
                    Console.WriteLine("Nome da Tag: " + linkscomnotificacao[i].TagName);
                    //aqui clica nos links tag <a> com notificação e abre a opção com a table
                    linkscomnotificacao[i].Click();
                    ActionsPJE.AguardarPje("Medio");
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
                        //recebe a tag <TD> que é aberta após o clique na tag <a>
                        IWebElement testea = verificando[j];
                        Console.WriteLine("Aqui está o elemento tagname TD: " + testea.Text);

                        //o ultimo TD será preenchido com um texto
                        if (!string.IsNullOrEmpty(testea.Text))
                        {
                            Console.WriteLine("Não sou null e tenho o valor de: " + testea.Text);

                            //precisamos saber se é o último elemento pois o último TD vai abrir o link que precisamos, que abrirá a tabela com as informações do processo e o link do processo.
                            // Verificar se é o último elemento TD para entrar nele, pois o ultimo elemento TD é o que precisa ser clicado para continuar.
                            if (j == verificando.Count - 1)
                            {
                                Console.WriteLine("Este é o último elemento TD, sendo: " + testea.Text);
                                //clica no ultimo elemento TD gerado no menu lateral , que vai abrir a tabela no centro da página contendo o link para verificar a movimentação processual na tag <a>
                                testea.Click();
                                ActionsPJE.AguardarPje("Baixo");
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
                                string Janela_Principal = ActionsPJE.RetornarParaJanelaPrincipal(driver);

                                foreach (var linkprocesso in linkmovimentacaoprocessual)
                                {
                                    Console.WriteLine(linkprocesso.Text);
                                    Console.WriteLine(linkprocesso.TagName);
                                    linkprocesso.Click();
                                    ActionsPJE.AguardarPje("Baixo");
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
                                                ActionsPJE.AguardarPje("Baixo");

                                                IList<IWebElement> movimentacaoprocessual = driver.FindElements(By.Id("divTimeLine:eventosTimeLineElement"));
                                                SalvarDados.SalvarMovimentacaoProcessual(movimentacaoprocessual, driver);
                                                //foreach (var registro in movimentacaoprocessual)
                                                //{
                                                //    Console.WriteLine(registro.Text);

                                                //}




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
                Console.WriteLine("Finalizado Painel do Advogado, redirecionando agora para a index.");
                
                //retomando para a INDEX agora
                ActionsPJE.RetornarIndexPJE(driver);
            }
            else
            {
                Console.WriteLine("Não há atualizações no painel de representante processual.");
            }
        }





        //proximas "ACTIONS"
    }
}
