using OpenQA.Selenium;
using Pje_WebScrapping.Actions;
using Pje_WebScrapping.Models;
using Pje_WebScrapping.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Pje_WebScrapping.DataStorage
{
    public class SalvarDados
    {
        //classe destinada a fazer a inserção no banco 

        //listas para inserir no banco
        public List<Processo> ListaProcessosIniciais = new List<Processo>();
        public List<ProcessoAtualizacao> ListaProcessosAtualizados = new List<ProcessoAtualizacao>();


        //método destinado a salvar o processo INICIAL
        //verificar se são realmente 7 divs em EXPEDIENTES dentro dos TDS
        public static string SalvarDadosProcesso(IList<IWebElement> CabecalhoProcesso, IList<IWebElement> NumProcesso ,IWebDriver driver)
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
                    //objetos para inserir no banco

                    //processo a ser inserido
                    Processo ProcessoEntidade = new Processo();

                    //lista de processos a serem inseridos
                    List<string> TestandoLista = new List<string>();


                    //testando inserir dados 
                    ProcessoEntidade.Cliente = ConteudoDasDivsProcessoAberto[0].Text;
                    ProcessoEntidade.Despacho = ConteudoDasDivsProcessoAberto[1].Text;
                    ProcessoEntidade.MeioDeComunicacao = ConteudoDasDivsProcessoAberto[2].Text;
                    ProcessoEntidade.Prazo = ConteudoDasDivsProcessoAberto[3].Text;

                    if (ConteudoDasDivsProcessoAberto[4].Text != "")
                    {
                        // Texto fornecido
                        string texto = ConteudoDasDivsProcessoAberto[4].Text;

                        // Encontrar o índice de "tomou ciência"
                        int indiceTomouCiencia = texto.IndexOf("tomou ciência");

                        // Extrair o nome da advogada
                        string nomeAdvogada = texto.Substring(0, indiceTomouCiencia).Trim();

                        // Armazenar o nome da advogada em ProcessoEntidade
                        ProcessoEntidade.Advogada = nomeAdvogada;
                        ProcessoEntidade.AdvogadaCiente = ConteudoDasDivsProcessoAberto[4].Text.ToString().Replace(nomeAdvogada + " ", "");

                    }

                    ProcessoEntidade.Resposta = ConteudoDasDivsProcessoAberto[6].Text;
                    ProcessoEntidade.ProximoPrazo = ConteudoDasDivsProcessoAberto[8].Text;
                    ProcessoEntidade.Causa = ConteudoDasDivsProcessoAberto[9].Text;

                    if(ConteudoDasDivsProcessoAberto[9].Text != "")
                    {
                        //num processo é a lista de links que redirecionam para o processo
                        //numero do processo em azul 
                        ProcessoEntidade.CodProcessoTJ = NumProcesso[0].Text.ToString().Replace("PJEC ","");
                    }

                    //ultima movimentacao processual
                    if (ConteudoDasDivsProcessoAberto[10].Text.Contains("Último movimento:"))
                    {
                        var stringverificadora = ConteudoDasDivsProcessoAberto[10].Text;
                        var indiceInicio = stringverificadora.IndexOf("Último movimento: ") + "Último movimento:".Length;
                        var parteDaString = stringverificadora.Substring(indiceInicio);
                        // agora 'parteDaString' contém a parte da string após "Último movimento:"
                        ProcessoEntidade.UltimaMovimentacaoProcessual = parteDaString;
                    }
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n Começando a imprimir  PROCESSOENTIDADE: ");
                    foreach (var propriedade in typeof(Processo).GetProperties())
                    {
                        var valor = propriedade.GetValue(ProcessoEntidade);
                        Console.WriteLine("{0}: {1}", propriedade.Name, valor);
                    }
                    Console.WriteLine("\n\nFinalizei  PROCESSOENTIDADE: ");
                    return "SalvarDadosProcesso foi concluido com sucesso";
                }
                return "Driver esta null em SalvarDadosProcesso";
            }
            return "Erro em SalvarDadosProcesso";
        }

        public static void SalvarMovimentacaoProcessual(IList<IWebElement> ListaDeMovimentacaoProcessual,
            IWebDriver driver)
        {
            int ponto_de_parada = 0;
            //instanciar um novo perfil de advogado e salvar usando o perfil dele no banco?



            //instancia a lista de mediabodybox ( boxes dentro da movimentação processual contendo as atualizações )
            IList<IWebElement> MediaBodyBoxHistoricoProcessual = new List<IWebElement>();
            MediaBodyBoxHistoricoProcessual = driver.FindElements(By.ClassName("media-body"));


            //instancia a lista de iwebelements contendo a tag <SPAN> que tem os titulos de cada atualizacao dentro da classe mediabodybox
            IList<IWebElement> TituloMovimentacaoProcessual = new List<IWebElement>();
            TituloMovimentacaoProcessual = driver.FindElements(By.ClassName("texto-movimento"));







            IList<IWebElement> TestandoANEXOS = new List<IWebElement>();
            TestandoANEXOS = driver.FindElements(By.ClassName("anexos"));


            //Objeto contando todos os elementos da tela de movimentação processual
            //irei UTILIZAR ESTE
            IWebElement PaginaMovimentacaoProcessual = driver.FindElement(By.Id("divTimeLine:eventosTimeLineElement"));


            //começo a buscar novos elementos pelo objeto instanciado que já está com os dados da pagina que eu quero

            //IList<IWebElement> webElements = new List<IWebElement>();
            //webElements = PaginaMovimentacaoProcessual.FindElements(By.ClassName(""));




            // Encontrar todos os elementos filhos do elemento pai
            IList<IWebElement> filhos = PaginaMovimentacaoProcessual.FindElements(By.XPath(".//*"));

            // Armazenar os elementos filhos em uma lista
            List<IWebElement> listaDeElementosFilhos = new List<IWebElement>(filhos);

            Console.WriteLine("\n\n\n\n");



            //esta lendo do sentido correto agora
            listaDeElementosFilhos.Reverse();




                                    LER ISSO:


            //preciso fazer agora com que identifique o tipo de objeto da lista
            //sendo: media interno tipo-M , media interno tipo-D ou media data
            
            OBS:
            //classe media interno tipo-M será para as divs que contém APENAS titulos de movimentação processual sem ATUALIZAÇÕES
            //classe media interno tipo-D será para as divs que contenham anexos e informações alem dos titulos
            //media data é a data

            // Exemplo de como iterar sobre a lista de elementos filhos
            foreach (var filho in listaDeElementosFilhos)
            {
                //dar continuidade mais tarde para verificar se é  CLASSE OU NAO DO ELEMETNOS
                Console.WriteLine("atributo filho " + filho.GetAttribute("class"));
                Console.WriteLine("nome do filho: " + filho.Text);
            }

            Console.WriteLine("\n\n\n\n");







            Console.WriteLine("\n o que é MEDIABODYBOX??? SalvarMovimentacaoProcessual?\n\n");

            //colocando ela de forma CRONOLÓGICA ( desde o início até o presente momento da movimentação processual .Reverse() )
            //MediaBodyBoxHistoricoProcessual é o título de cada atualização de processo


            //vou fazer um foreach para verificar cada mediabodybox e acessar cada vetor do array para poder retornar ao usuario
            //os elementos contidos na bodybox
            //criar uma lista de processoatualizacao para inserir dentro do objeto cada value pertinente


            List<ProcessoAtualizacao> ProcessosAtualizacoes = new List<ProcessoAtualizacao>();




            //testar para ver se altera a ordem de forma correta
            //foreach (var teste in TituloMovimentacaoProcessual.Reverse())
            //{
            //    Console.WriteLine("TESTE PARA TITULOS DAS ATUALIZAÇÕES: " + teste.Text);
            //}

            //Console.WriteLine("\n\n\n\n\n");

            ////será o CONTEUDO DA ATUALIZACAO PROCESSOS
            //foreach (var teste in TestandoANEXOS.Reverse())
            //{
            //    if (!string.IsNullOrEmpty(teste.Text))
            //    {
            //        Console.WriteLine("TESTE PARA ANEXOS: " + teste.Text);
            //    }
            //}


            //for(int i = 0; i <= MediaBodyBoxHistoricoProcessual.Count; i++)
            //{

            //    if (!string.IsNullOrEmpty(MediaBodyBoxHistoricoProcessual[i].Text))
            //    {
            //        if(!string.IsNullOrEmpty())
            //    }

            //}


            Console.WriteLine("Testando: \n\n " + PaginaMovimentacaoProcessual.Text);




            //for(int i = 0; i< PaginaMovimentacaoProcessual.Text.Length;i++)
            //{
            //    //vamos verificar o tipo de tag que está dentro de paginamovimentacaoprocessual
            //    //IWebElement Teste = PaginaMovimentacaoProcessual.GetAttribute

                
            //}

            foreach (var teste in MediaBodyBoxHistoricoProcessual)
            { 
                if(!string.IsNullOrEmpty(teste.Text))
                {
                    
                }
                    Console.WriteLine("TESTE PARA ANEXOS: " + teste.Text + "Sou uma Tag: " + teste.TagName);

            }







                //foreach (var verificabodybox in MediaBodyBoxHistoricoProcessual.Reverse())
                //{

                //    ProcessoAtualizacao processoAtualizacao = new ProcessoAtualizacao(); // Criar um novo objeto a cada iteração
                //    processoAtualizacao.TituloPjeMovimentacaoProcessual = verificabodybox.Text.ToString();
                //    if (verificabodybox.Text != "")

                //        Console.WriteLine("meu valor da bodybox é de: " + verificabodybox.Text);
                //    ProcessosAtualizacoes.Add(processoAtualizacao);
                //}

                // Verificar lista sendo preenchida para inserirmos no banco
                foreach (var listadosprocessosatualizados in ProcessosAtualizacoes)
            {
                Console.WriteLine("Lista processo atualizado: " + listadosprocessosatualizados.TituloPjeMovimentacaoProcessual);
            }





            //agilizando testes
            ActionsPJE.EncerrarConsole();




            //como fazer buscar pelo classname que contenha esse nome de classe media interno tipo


            //IList<IWebElement> QuadradoDaMovimentacao = driver.FindElements(By.ClassName().ToString().Contains);



            //na hora de inserir no banco preciso fazer da seguinte forma:
            //fazer lista com media interno tipo ( que são as divs com todas as descrições da movimentação )
            //fazer lista com media data para puxar as datas da movimentação que ficam acima das divs.

            int roda = 0;













            //VERIFICAR DPS ISSO DAQUI PARAMETRO DE SALVARMOVIMENTACAOPROCESSUAL INSTANCIADA AQUI




            ////objeto instanciado da div BRUTA da movimentaão processual, TODOS OS ELEMENTOS AQUI:
            //IList<IWebElement> HistoricoDeProcessos = ListaDeMovimentacaoProcessual;

            ////IList<IWebElement> QuadradoDaMovimentacao = driver.FindElements(By.ClassName("media-body.box"));
            ////IList<IWebElement> DatasDaMovimentacao = driver.FindElements(By.ClassName("mediadata"));

            //Console.WriteLine("\n o que é historicoDeProcessos dentro de SalvarMovimentacaoProcessual?\n\n");
            //foreach (var testando in HistoricoDeProcessos)
            //{
            //    Console.WriteLine("historico de processos: " + testando.Text);
            //}

            //Console.WriteLine("\n\n\n\n\n\n");
            //foreach (var stringonamovimentacao in HistoricoDeProcessos)
            //{
            //    string texto = stringonamovimentacao.Text.ToString();
            //    int comprimentoTotal = texto.Length;

            //    // Calcula o tamanho de cada parte
            //    int tamanhoParte = comprimentoTotal / 3;

            //    // Divide a string em três partes
            //    string parte1 = texto.Substring(0, tamanhoParte);
            //    string parte2 = texto.Substring(tamanhoParte, tamanhoParte);
            //    string parte3 = texto.Substring(2 * tamanhoParte, tamanhoParte);

            //    // Imprime as partes
            //    Console.WriteLine("Parte 1: " + parte1);
            //    Console.WriteLine("\n\n\n");
            //    Console.WriteLine("Parte 2: " + parte2);
            //    Console.WriteLine("\n\n\n");
            //    Console.WriteLine("Parte 3: " + parte3);
            //    Console.WriteLine("\n\n\n");



            //    //roda++;
            //    //Console.WriteLine("Começando a imprimir o que tem na movimentação processual");
            //    //ActionsPJE.AguardarPje("Baixo");
            //    ////imprime TODA A MOVIMENTAÇÃO PROCESSUAL
            //    //Console.WriteLine("\n +++ "  + stringonamovimentacao.TagName);
            //    //Console.WriteLine("SalvarMovimentacaoProcessual, iten: " + ponto_de_parada + "  " + stringonamovimentacao.Text);
            //    //Console.WriteLine("Total : " + stringonamovimentacao.ToString().Length);
            //    //foreach (var teste in stringonamovimentacao.Text)
            //    //{

            //    //}


            //    //if(HistoricoDeProcessos.Count - 1 == ponto_de_parada)
            //    //{
            //    //    Console.WriteLine("Finalizei a lista");
            //    //}
            //    //ponto_de_parada++;

            //}
            ponto_de_parada = 0;

            //Console.WriteLine("lista finalizada com um total de: " + HistoricoDeProcessos.Count + " registros");
            //Console.WriteLine("Roda rodou: " + roda);
            //Console.WriteLine("lista finalizada com um total de: " + QuadradoDaMovimentacao.Count + " registros");
            //Console.WriteLine("lista finalizada com um total de: " + DatasDaMovimentacao.Count + " registros");
        }


    }
}
