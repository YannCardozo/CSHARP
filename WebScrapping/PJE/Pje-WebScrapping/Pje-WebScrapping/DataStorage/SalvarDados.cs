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







            //LER ISSO:


            //preciso fazer agora com que identifique o tipo de objeto da lista
            //sendo: media interno tipo-M , media interno tipo-D ou media data

            //OBS:
            //classe media interno tipo-M será para as divs que contém APENAS titulos de movimentação processual sem ATUALIZAÇÕES
            //classe media interno tipo-D será para as divs que contenham anexos e informações alem dos titulos
            //media data é a data

            // Exemplo de como iterar sobre a lista de elementos filhos

            int contatipod = 0;
            int contaanexos = 0;
            int ContaTextoTipoM = 0;
            //foreach (var filho in listaDeElementosFilhos)
            //{
            //    //dar continuidade mais tarde para verificar se é  CLASSE OU NAO DO ELEMETNOS
            //    //Console.WriteLine("atributo filho " + filho.GetAttribute("class"));
            //    //Console.WriteLine("nome do filho: " + filho.Text);
            //    if(filho.GetAttribute("class") == "media interno tipo-D")
            //    {
            //        IList<IWebElement> AnexosDentroDeFilho = new List<IWebElement>();
            //        AnexosDentroDeFilho = filho.FindElements(By.ClassName("anexos"));
            //        if (AnexosDentroDeFilho.Count > 0)
            //        {
            //            Console.WriteLine("Encontrei o atributo 'anexos' com valor: " + AnexosDentroDeFilho);
            //            contaanexos++;
            //            Console.WriteLine("Anexo: " + contaanexos + " " + AnexosDentroDeFilho.ToString());
            //        }
            //        contatipod++;
            //    }


            //}


            //ActionsPJE.AguardarPje("Medio");

            //descer barra de rolagem para puxar todas as informações
            ActionsPJE.DescerBarraDeRolagem(driver, "divTimeLine:divEventosTimeLine");





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




            // Vou precisar criar um FOR e acessar indice por indice para fazer a distinção de quem e media data e de quem nao e







            //elemento div menu lateral contendo toda a movimentação processual
            //IList<IWebElement> paiElementos = driver.FindElements(By.Id("divTimeLine:eventosTimeLineElement"));



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
                            foreach(var testa in ListaConteudoMovimentoProcessual)
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

                            ProcessoAtualizado.TituloMovimento = SpanTextoMovimentacao.Text;

                            if(ProcessoAtualizado.ConteudoAtualizacao == "")
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

            ActionsPJE.EncerrarConsole();



            for (int i = 0; i < listaDeElementosFilhos.Count; i++)
            {
                //se for o media box com ANEXOS ( caixa da movimentação processual de cada link de processo )

                //necessário colocar um if aqui dentro para verificar onde a div da data vai aparecer

                //IList<IWebElement> elementoComClassesA = listaDeElementosFilhos[i].FindElements(By.ClassName("data-interna"));
                
                //for (int z = 0; z < elementoComClassesA.Count; z++)
                //{
                //    if (!String.IsNullOrEmpty(elementoComClassesA[z].Text))
                //    {
                //        Console.WriteLine("Testando Datas: " + elementoComClassesA[z].Text);
                //        Console.WriteLine("\n");
                //    }
                //}



                    if (listaDeElementosFilhos[i].GetAttribute("class") == "media interno tipo-D")
                    {
                        try
                        {
                            //instancia uma lista de objetos chamado anexos, para conter cada atualização de anexos
                            IList<IWebElement> AnexosDentroDeFilho = listaDeElementosFilhos[i].FindElements(By.ClassName("anexos"));
                            if (AnexosDentroDeFilho.Count > 0)
                            {
                               // Console.WriteLine("Encontrei o atributo 'anexos' com valor:");
                                contaanexos++;

                                // Itera sobre os elementos da lista e imprime os textos individualmente
                                foreach (var anexo in AnexosDentroDeFilho.Reverse())
                                {
                                    //instanciar a lista de objetos de tag <a>
                                    if (anexo != null)
                                    {
                                        IList<IWebElement> LinksTagADeAnexo = anexo.FindElements(By.TagName("a"));
                                        IList<IWebElement> ThreeClassUL = anexo.FindElements(By.ClassName("tree"));
                                    
                                        //CONTINUAR DAQUI, ESTA ACHANDO O UL CORRETAMENTE JÁ.
                                        foreach(var ClasseUlArvore in ThreeClassUL)
                                        {
                                            Console.WriteLine("Achei o ul: " + ClasseUlArvore.Text);
                                        }

                                        //foreach(var TagA in LinksTagADeAnexo.Reverse())
                                        //{
                                        //    if (!string.IsNullOrEmpty(TagA.Text))
                                        //    {
                                        //        Console.WriteLine("TAG A: " + TagA.Text);

                                        //    }
                                        //}

                                        //Console.WriteLine("Anexo: " + contaanexos + " " + anexo.Text);


                                    }
                                    else
                                    {
                                        Console.WriteLine("Anexos null");
                                    }
                                }
                            }
                            contatipod++;
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.ToString() + " erro em media interno tipo D: " + listaDeElementosFilhos[i].Text + " Contador : " + contaanexos);
                        }
                    }

                else if(listaDeElementosFilhos[i].GetAttribute("class") == "media interno tipo-M")
                {
                    IList<IWebElement> TextoDentroTipoM = listaDeElementosFilhos[i].FindElements(By.ClassName("texto-movimento"));

                    try
                    {
                        foreach(var TextoAtual in TextoDentroTipoM)
                        {
                            ContaTextoTipoM++;
                            Console.WriteLine("texto: " + TextoAtual.Text);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Erro em media interno tipo M: obter dados " + ex.Message);
                    
                    }
                }
            }





            Console.WriteLine("achei " + contatipod + " TIPO-D");
            Console.WriteLine("achei " + contaanexos + " anexos");
            Console.WriteLine("achei " + ContaTextoTipoM + " textos do tipo-m");
            Console.WriteLine("\n\n\n\n");



        }


    }
}
