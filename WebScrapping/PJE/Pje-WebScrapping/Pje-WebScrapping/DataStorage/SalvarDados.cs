using AngleSharp.Dom;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Pje_WebScrapping.Actions;
using Pje_WebScrapping.Models;
using Pje_WebScrapping.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public static string SalvarDadosProcesso(IWebElement PrimeiraColDados, IWebElement SegundaColDados, IWebElement NumProcesso)
        {

            //APAGAR A PORRA TODA E FAZER DO ZERO PASSANDO O PARAMETRO DO QUE IRÁ SALVAR VINDO DE NAVBAROPTIONSACTIONS


            //IF verificacao no banco de dados ID EFC para saber se ja existe o registro cadastrado

            //processo a ser inserido
            //redefinir classe processo, reduzir atributos
            Processo ProcessoEntidade = new Processo();


            //os atributos serão obtidos pela tag span com o title respectivo

            IList<IWebElement> ListaSpansPrimeiracolDados = PrimeiraColDados.FindElements(By.TagName("span"));

            Console.WriteLine("\n\n\n");

            IWebElement spanDestinatario = null;
            IWebElement spanTipoDeDocumento = null;
            IWebElement spanMeioDeComunicacao = null;
            Prazo para manifestação
            //o que não conseguir pelo title do span, tentar conseguir pela posição dentro de primeiracoldados

            //funcionou, melhorar.
            foreach (IWebElement span in ListaSpansPrimeiracolDados)
            {
                string title = span.GetAttribute("title");
                if (title != null && title.Equals("Destinatário", StringComparison.OrdinalIgnoreCase))
                {
                    spanDestinatario = span;
                    break;
                }
                else if (title != null && title.Equals("Tipo de documento", StringComparison.OrdinalIgnoreCase))
                {
                    spanTipoDeDocumento = span;
                    break;
                }
                else if(title != null && title.Equals("spanMeioDeComunicacao", StringComparison.OrdinalIgnoreCase))
                {
                    spanMeioDeComunicacao = span;
                    break;
                }







            }



            if (spanDestinatario != null)
            {
                Console.WriteLine("\n\n\n\n Inserindo nome do destinatário: " + spanDestinatario.Text);
                ProcessoEntidade.Cliente = spanDestinatario.Text;

            }

            ActionsPJE.EncerrarConsole();
            Console.WriteLine("Testando: \n\n\n");


            Console.WriteLine("Sou a 1 coluna: \n " + PrimeiraColDados.Text);
            Console.WriteLine("Sou a 2 coluna: \n " + SegundaColDados.Text);
            Console.WriteLine(NumProcesso.Text);



            return "string";


            //try
            //{
            //    if (driver != null)
            //    {
            //        //Console.WriteLine("Meu tamanho de cabecalho processo é: " + CabecalhoProcesso.Count);
            //        //ActionsPJE.EncerrarConsole();
            //        if (CabecalhoProcesso.Count > 0)
            //        {
            //            IList<IWebElement> ConteudoDasDivsProcessoAberto = new List<IWebElement>();
            //            foreach (var elemento in CabecalhoProcesso)
            //            {
            //                // Encontra todas as divs dentro do elemento atual e adiciona à lista ConteudoDasDivsProcessoAberto
            //                IList<IWebElement> divsInternas = elemento.FindElements(By.TagName("div"));
            //                foreach (var div in divsInternas)
            //                {
            //                    //insere todas as divs dentro do elemento TD referente ao processo
            //                    ConteudoDasDivsProcessoAberto.Add(div);
            //                }
            //            }
            //            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n Testando SALVARDADOSPROCESSOAGORA");
            //            //objetos para inserir no banco



            //            //lista de processos a serem inseridos
            //            List<string> TestandoLista = new List<string>();


            //            //testando inserir dados 
            //            ProcessoEntidade.Cliente = ConteudoDasDivsProcessoAberto[0].Text;
            //            ProcessoEntidade.Despacho = ConteudoDasDivsProcessoAberto[1].Text;
            //            ProcessoEntidade.MeioDeComunicacao = ConteudoDasDivsProcessoAberto[2].Text;
            //            ProcessoEntidade.Prazo = ConteudoDasDivsProcessoAberto[3].Text;

            //            if (ConteudoDasDivsProcessoAberto[4].Text != "")
            //            {
            //                // Texto fornecido
            //                string texto = ConteudoDasDivsProcessoAberto[4].Text;

            //                // Encontrar o índice de "tomou ciência"
            //                int indiceTomouCiencia = texto.IndexOf("tomou ciência");

            //                // Extrair o nome da advogada
            //                string nomeAdvogada = texto.Substring(0, indiceTomouCiencia).Trim();

            //                // Armazenar o nome da advogada em ProcessoEntidade
            //                ProcessoEntidade.Advogada = nomeAdvogada;
            //                ProcessoEntidade.AdvogadaCiente = ConteudoDasDivsProcessoAberto[4].Text.ToString().Replace(nomeAdvogada + " ", "");

            //            }

            //            ProcessoEntidade.Resposta = ConteudoDasDivsProcessoAberto[6].Text;
            //            ProcessoEntidade.ProximoPrazo = ConteudoDasDivsProcessoAberto[8].Text;
            //            ProcessoEntidade.Causa = ConteudoDasDivsProcessoAberto[9].Text;

            //            if (ConteudoDasDivsProcessoAberto[9].Text != "")
            //            {
            //                //num processo é a lista de links que redirecionam para o processo
            //                //numero do processo em azul 
            //                ProcessoEntidade.CodProcessoTJ = NumProcesso[0].Text.ToString().Replace("PJEC ", "");
            //            }

            //            //ultima movimentacao processual
            //            if (ConteudoDasDivsProcessoAberto[10].Text.Contains("Último movimento:"))
            //            {
            //                var stringverificadora = ConteudoDasDivsProcessoAberto[10].Text;
            //                var indiceInicio = stringverificadora.IndexOf("Último movimento: ") + "Último movimento:".Length;
            //                var parteDaString = stringverificadora.Substring(indiceInicio);
            //                // agora 'parteDaString' contém a parte da string após "Último movimento:"
            //                ProcessoEntidade.UltimaMovimentacaoProcessual = parteDaString;
            //            }
            //            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n Começando a imprimir  PROCESSOENTIDADE: ");
            //            foreach (var propriedade in typeof(Processo).GetProperties())
            //            {
            //                var valor = propriedade.GetValue(ProcessoEntidade);
            //                Console.WriteLine("{0}: {1}", propriedade.Name, valor);
            //            }
            //            Console.WriteLine("\n\nFinalizei  PROCESSOENTIDADE: ");

            //            return "\n Processo nº " + ProcessoEntidade.CodProcessoTJ + " inserido com sucesso no banco.";
            //        }
            //        return "Driver esta null em SalvarDadosProcesso";
            //    }
            //    return "Erro em SalvarDadosProcesso";
            //}
            //catch(Exception ex)
            //{
            //     Console.WriteLine("Erro em Salvar dadosProcesso: " + ex.Message);
            //    //ActionsPJE.EncerrarConsole();
            //    return "Erro em SalvarDadosProcesso";
            //}
 
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


            int contatipod = 0;
            int contaanexos = 0;
            int ContaTextoTipoM = 0;


            //ActionsPJE.AguardarPje("Medio");
            ActionsPJE.DescerBarraDeRolagem(driver, "divTimeLine:divEventosTimeLine");

            //Objeto contando todos os elementos da tela de movimentação processual
            IWebElement PaginaMovimentacaoProcessual = driver.FindElement(By.Id("divTimeLine:eventosTimeLineElement"));


            // Encontrar todos os elementos filhos do elemento pai
            IList<IWebElement> filhos = PaginaMovimentacaoProcessual.FindElements(By.XPath(".//*"));

            // Armazenar os elementos filhos em uma lista
            List<IWebElement> listaDeElementosFilhos = new List<IWebElement>(filhos);

            Console.WriteLine("\n\n\n\n");

            //esta lendo do sentido correto agora
            listaDeElementosFilhos.Reverse();


            // elemento da div totalmente carregada apos descer rolagem
            // PaginaMovimentacaoProcessual

            IList<IWebElement> ElementosDentroDeMovimentacaoProcessual = PaginaMovimentacaoProcessual.FindElements(By.XPath("./*"));

            //removendo o elemento div-data-rolagem
            ElementosDentroDeMovimentacaoProcessual = ElementosDentroDeMovimentacaoProcessual.Where(elemento => !elemento.GetAttribute("class").Contains("div-data-rolagem")).ToList();
            // Itere sobre os elementos filhos




            int posicao = 0;
            //int posicao_inicial = 1;
            int posicao_inicial = 0;



            int Controle_Elementos_Lista = 0;
            int Controle_Elementos_Lista_Atualizado = 0;












            int controle_elementos_anteriores_data = 0;
            int posicao_data = 0;

            int controle_inicio_for_elementos = 0;
            int proximaPosicaoMediaData = -1;
            int FimMediaData = 0;
            int LocalizadorElementoMediaData = 0;
            int marco_inicial = 0;



            // Lista para armazenar os elementos irmãos antes de cada "media data"
            List<List<IWebElement>> ElementosAnteriores = new List<List<IWebElement>>();


            //controle_elementos_anteriores_data

            //criando lista para receber elementos invertidos
            IList<IWebElement> ElementosDentroDeMovimentacaoProcessualINVERTIDO = ElementosDentroDeMovimentacaoProcessual.Where(elemento => !elemento.GetAttribute("class").Contains("div-data-rolagem")).ToList();





            //lista destinada aos CONTEUDOS nos casos da classe media tipo-D , que tem varias informações dentro do media box
            IList<IWebElement> ListaConteudoMovimentoProcessual = new List<IWebElement>();

            string ConteudoTipoD = "";



            //for para inverter a ordem dos elementos da lista da movimentação processual, colocando em ordem CRONOLÓGICA.
            for (int i = ElementosDentroDeMovimentacaoProcessual.Count - 1 , j = 0; i >= 0; i--, j++)
            {
                //Console.WriteLine("Elemento: " + ElementosDentroDeMovimentacaoProcessual[i].Text);
                ElementosDentroDeMovimentacaoProcessualINVERTIDO[j] = ElementosDentroDeMovimentacaoProcessual[i];
            }



            //instanciando LISTA de objetos que receberão os registros da lista de movimentação processual
            List<ProcessoAtualizacao> ListaProcessosAtualizados = new List<ProcessoAtualizacao>();

            //esse for faz a leitura dos elementos dentro de movimentacao processual
            for (int i = 0; i < ElementosDentroDeMovimentacaoProcessualINVERTIDO.Count; i++)
            {

                //for para localizar a proxima data na lista de elementos e pegar o indice e atrbuir a variavel proximaPosicaoMediaData
                //que vai entrar no for de indice J
                for (int z = LocalizadorElementoMediaData; z < ElementosDentroDeMovimentacaoProcessualINVERTIDO.Count; z++)
                {
                    if (ElementosDentroDeMovimentacaoProcessualINVERTIDO[z].GetAttribute("class").Contains("media data") && !ElementosDentroDeMovimentacaoProcessualINVERTIDO[z].GetAttribute("class").Contains("div-data-rolagem"))
                    {
                        proximaPosicaoMediaData = z;

                        //Console.WriteLine("Achei a próxima data e ela está em: " + proximaPosicaoMediaData);
                        break;
                    }
                    else
                    {
                        proximaPosicaoMediaData = -1;
                    }
                }
                if (ElementosDentroDeMovimentacaoProcessualINVERTIDO[i].GetAttribute("class").Contains("media data") && !ElementosDentroDeMovimentacaoProcessualINVERTIDO[i].GetAttribute("class").Contains("div-data-rolagem"))
                {

                    //recebe o index da data atual para não se perder
                    posicao_data = i;
                    //Console.WriteLine("antes de: " + ElementosDentroDeMovimentacaoProcessualINVERTIDO[i].Text);

                    Console.WriteLine("Testando value pos: " + posicao + " e pos inic: " + posicao_inicial + "\n\n");
                    for (int j = posicao_inicial; j <= proximaPosicaoMediaData; j++)
                    {
                        if (proximaPosicaoMediaData == -1)
                        {
                            //Console.WriteLine("Acabaram os elementos DATA - break - DATA ATUAL: " + ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].Text);

                            break;
                        }


                        ProcessoAtualizacao ProcessoAtualizado = new ProcessoAtualizacao();

                        if (ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].GetAttribute("class").Contains("media data"))
                        {
                            Console.WriteLine("Acabaram os elementos DATA - continue - DATA ATUAL: " + ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].Text);


                            // Atribuindo a data convertida à propriedade DataMovimentacao


                            continue;
                        }

                        //inserindo a data para os registros de movimentação processual
                        if(proximaPosicaoMediaData != -1)
                        {
                            string dataString = ElementosDentroDeMovimentacaoProcessualINVERTIDO[proximaPosicaoMediaData].Text;
                            DateOnly data = DateOnly.ParseExact(dataString, "dd MMM yyyy", CultureInfo.InvariantCulture);
                            ProcessoAtualizado.DataMovimentacao = data;

                        }
                        if (ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].GetAttribute("class").Contains("media interno tipo-D"))
                        {
                            //encotrando o TITULO da movimentação processual

                            IWebElement MediaBodyBoxTipoD = ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].FindElement(By.ClassName("media-body"));

                            IList<IWebElement> MaisDeUmElementoSpanNoMediaBoxTipoD = MediaBodyBoxTipoD.FindElements(By.ClassName("texto-movimento"));


                            IWebElement SpanTextoMovimentacao = MediaBodyBoxTipoD.FindElement(By.TagName("span"));
                            ProcessoAtualizado.TituloMovimento = SpanTextoMovimentacao.Text;



                            ListaConteudoMovimentoProcessual = ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].FindElements(By.TagName("span")).Skip(1).ToList();


                            //montando uma variavel para armazenar toda a lista do 
                            foreach(var testa in ListaConteudoMovimentoProcessual.Skip(1))
                            {
                                ConteudoTipoD += testa.Text + " ";
                            }

                            //insere o conteúdo da atualizacao para o objeto

                            ProcessoAtualizado.ConteudoAtualizacao = ConteudoTipoD;


                            //esvazia a string para a próxima atualização de dados
                            ConteudoTipoD = string.Empty;
                        }
                        else if(ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].GetAttribute("class").Contains("media interno tipo-M"))
                        {
                            //encotrando o TITULO da movimentação processual
                            IWebElement MediaBodyBoxTipoM = ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].FindElement(By.ClassName("media-body"));
                            IWebElement SpanTextoMovimentacao = MediaBodyBoxTipoM.FindElement(By.ClassName("texto-movimento"));

                            IList<IWebElement> ListaDeSpansNoMediaBodyBox = MediaBodyBoxTipoM.FindElements(By.ClassName("texto-movimento"));

                            ProcessoAtualizado.TituloMovimento = SpanTextoMovimentacao.Text;

                            if(ListaDeSpansNoMediaBodyBox.Count <= 1)
                            {
                                ProcessoAtualizado.ConteudoAtualizacao = "Sem conteúdo no PJE";
                            }

                            //ALIMENTAR OS PROCESSOS AQUI

                            //ProcessoAtualizado.TituloMovimento = primeiroSpan.Text;

                        }








                        //insere o objeto na lista
                        ListaProcessosAtualizados.Add(ProcessoAtualizado);



                        //reinicia a lista após terminar ela
                        //ListaProcessosAtualizados.Clear();


                        //realizar de para dos objetos aqui
                        //implementar parametro em salvar movimentacao processual ( método ) para que dê o objeto processo
                        //para obter seu numero de processo e outros dados caso necessário







                        Console.WriteLine("Elemento: " + j + " :  " + ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].Text);



                    }
                    posicao_inicial = proximaPosicaoMediaData;

                }
                
                posicao++;
                LocalizadorElementoMediaData++;


                //for para verificar as proximas elementos datas no vetor
                if (proximaPosicaoMediaData == -1 )
                {
                    Console.WriteLine("Acabaram os elementos DATA ");
                    break;
                }
            }


            //ActionsPJE.EncerrarConsole();

            //testando a lista de movimentacao processual


            Console.WriteLine("\n\n\n\n");


            foreach (var teste in ListaProcessosAtualizados)
            {
                Console.WriteLine("Data: " + teste.DataMovimentacao + " - Titulo Mov: " + teste.TituloMovimento + " - Conteudo: " + teste.ConteudoAtualizacao);
            }

            //ActionsPJE.EncerrarConsole();

            ActionsPJE.RetornarParaJanelaPrincipal(driver);



        }


    }
}
