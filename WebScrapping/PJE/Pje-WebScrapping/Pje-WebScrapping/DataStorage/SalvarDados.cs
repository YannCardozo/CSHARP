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
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
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
        public static Processo SalvarDadosProcesso(IWebElement PrimeiraColDados, IWebElement SegundaColDados, IWebElement NumProcesso)
        {
            Processo ProcessoEntidade = new Processo();
            Console.WriteLine("\n\n\n");

            //primeiracoldados
            string spanDestinatario = "";
            string spanTipoDeDocumento = "";
            string spanMeioDeComunicacao = "";
            string DataCienciaProcesso = "";
            string PrazoManifestacao = "";
            string MeioDeComunicacaoData = "";
            string numeroTipoDocumento = "";
            string DataPrevistaLimiteManifestacao = "";
            string TipoDocumento = "";
            string dataLimite = "";


            //segundacoldados
            string TituloProcesso = "";
            string TituloPartes = "";
            string ComarcaInicial = "";
            string UltimaMovimentacao = "";
            string NumProcessoSegundaColDados = NumProcesso.Text;
            string UltimaMovimentacaoProcessualData = "";


            IList<IWebElement> ListaElementosPrimeiraColDados = PrimeiraColDados.FindElements(By.XPath(".//*"));
            Console.WriteLine("\n\n\n ENTREI");

            //PrimeiraColDados
            foreach (IWebElement elemento in ListaElementosPrimeiraColDados)
            {
                string title = elemento.GetAttribute("title");
                if (!string.IsNullOrEmpty(title) && title.Equals("Destinatário", StringComparison.OrdinalIgnoreCase))
                {
                    spanDestinatario = elemento.Text;
                }
                else if (!string.IsNullOrEmpty(title) && title == "Tipo de documento" && (elemento.Text.StartsWith("Decisão", StringComparison.OrdinalIgnoreCase) || elemento.Text.StartsWith("Intimação", StringComparison.OrdinalIgnoreCase)))
                {
                    TipoDocumento = elemento.Text;
                    numeroTipoDocumento = elemento.Text;
                    numeroTipoDocumento = numeroTipoDocumento.Replace("Decisão (", "");
                    numeroTipoDocumento = numeroTipoDocumento.Replace("Intimação (", "");
                    numeroTipoDocumento = numeroTipoDocumento.Replace(")", "");

                }
                else if (!string.IsNullOrEmpty(title) && title.Equals("Meio de comunicação", StringComparison.OrdinalIgnoreCase))
                {

                    spanMeioDeComunicacao = elemento.Text;
                }
                else if (!string.IsNullOrEmpty(title) && MeioDeComunicacaoData == "" && title.Equals("Data de criação do expediente", StringComparison.OrdinalIgnoreCase))
                {
                    MeioDeComunicacaoData = elemento.Text;
                }
                else if (!string.IsNullOrEmpty(title) && title.Equals("Prazo para manifestação", StringComparison.OrdinalIgnoreCase))
                {
                    PrazoManifestacao = elemento.Text;
                    PrazoManifestacao = PrazoManifestacao.Replace("Prazo:" ,"");
                }
                string divId = elemento.GetAttribute("id");
                if (!string.IsNullOrEmpty(divId) && divId.Contains(numeroTipoDocumento) && !string.IsNullOrEmpty(numeroTipoDocumento) && DataCienciaProcesso == "")
                {
                    DataCienciaProcesso = elemento.Text;
                }

                string TextoDentroDiv = elemento.Text;
                if (TextoDentroDiv.Contains("Data limite prevista"))
                {
                    DataPrevistaLimiteManifestacao = elemento.Text;
                    // Use expressão regular para extrair a data do texto
                    Match match = Regex.Match(TextoDentroDiv, @"\d{2}/\d{2}/\d{4} \d{2}:\d{2}");
                    if (match.Success)
                    {
                        dataLimite = match.Value;
                        // Agora, você pode armazenar a dataLimite onde desejar.
                    }
                    if(string.IsNullOrEmpty(DataPrevistaLimiteManifestacao))
                    {
                        DataPrevistaLimiteManifestacao = "Sem próximo prazo";
                    }
                    if(string.IsNullOrEmpty(dataLimite))
                    {
                        dataLimite = "Sem data próximo prazo";
                    }
                }

                ProcessoEntidade.Cliente = spanDestinatario;
                ProcessoEntidade.CodPJECAcao = TipoDocumento;
                ProcessoEntidade.MeioDeComunicacao = spanMeioDeComunicacao;
                ProcessoEntidade.MeioDeComunicacaoData = MeioDeComunicacaoData;
                ProcessoEntidade.AdvogadaCiente = DataCienciaProcesso;
                ProcessoEntidade.ProximoPrazo = DataPrevistaLimiteManifestacao;
                ProcessoEntidade.ProximoPrazoData = dataLimite;
                ProcessoEntidade.Prazo = PrazoManifestacao;
                ProcessoEntidade.CodPJEC = NumProcesso.Text;
            }

            Console.WriteLine("\n PrimeiraColDados");
            Console.WriteLine("Cliente: " + ProcessoEntidade.Cliente);
            Console.WriteLine($"Nº Processo: {ProcessoEntidade.CodPJEC}");
            Console.WriteLine("CodPJECAcao: " + ProcessoEntidade.CodPJECAcao);
            Console.WriteLine("MeioDeComunicacao: " + ProcessoEntidade.MeioDeComunicacao);
            Console.WriteLine("MeioDeComunicacaoData: " + ProcessoEntidade.MeioDeComunicacaoData);
            Console.WriteLine("AdvogadaCiente: " + ProcessoEntidade.AdvogadaCiente);
            Console.WriteLine("ProximoPrazo: " + ProcessoEntidade.ProximoPrazo);
            Console.WriteLine("ProximoPrazoData: " + ProcessoEntidade.ProximoPrazoData);
            Console.WriteLine("Prazo: " + ProcessoEntidade.Prazo);


            //spanDestinatario = "";
            TipoDocumento = "";
            spanMeioDeComunicacao = "";
            MeioDeComunicacaoData = "";
            DataCienciaProcesso = "";
            DataPrevistaLimiteManifestacao = "";
            dataLimite = "";
            PrazoManifestacao = "";


            //começar a verificar a SegundaColDados:
            IList<IWebElement> ListaElementosSegundaColDados = SegundaColDados.FindElements(By.XPath(".//*"));

            //SegundaColDados
            foreach (IWebElement elemento in ListaElementosSegundaColDados)
            {

                if (elemento.Text.Contains(NumProcessoSegundaColDados) && elemento.TagName == "div")
                {
                    TituloProcesso = elemento.Text;
                    TituloProcesso = TituloProcesso.Replace(NumProcessoSegundaColDados, "");
                }
                else if(elemento.Text.Contains(spanDestinatario) && (!elemento.Text.Contains("Último movimento: ") && !elemento.Text.Contains("Decorrido prazo de")))
                {
                    TituloPartes = elemento.Text;
                }
                else if(elemento.Text.Contains("º") || elemento.Text.Contains("Juizado") || elemento.Text.Contains("Especial") || elemento.Text.Contains("Comarca"))
                {
                    ComarcaInicial = elemento.Text;
                    ComarcaInicial = ComarcaInicial.Replace("/","");
                }
                if(elemento.Text.Contains("Último movimento: ") || elemento.Text.Contains("Publicado") || elemento.Text.Contains("Decorrido prazo de"))
                {
                    UltimaMovimentacao = elemento.Text;
                    UltimaMovimentacaoProcessualData = UltimaMovimentacao;
                    UltimaMovimentacaoProcessualData = UltimaMovimentacaoProcessualData.Replace("Último movimento: ", "");
                    int index = UltimaMovimentacaoProcessualData.IndexOf("-");
                    if (index != -1)
                    {
                        UltimaMovimentacaoProcessualData = UltimaMovimentacaoProcessualData.Substring(0, index);
                    }
                }

                ProcessoEntidade.TituloProcesso = TituloProcesso;
                ProcessoEntidade.PartesProcesso = TituloPartes;
                ProcessoEntidade.ComarcaInicial = ComarcaInicial;
                ProcessoEntidade.UltimaMovimentacaoProcessual = UltimaMovimentacao;
                ProcessoEntidade.UltimaMovimentacaoProcessualData = UltimaMovimentacaoProcessualData;

            }

            Console.WriteLine("\n SegundaColDados");
            Console.WriteLine("Titulo processo: " + ProcessoEntidade.TituloProcesso);
            Console.WriteLine("Partes do Processo: " + ProcessoEntidade.PartesProcesso);
            Console.WriteLine("Comarca Inicial: " + ProcessoEntidade.ComarcaInicial);
            Console.WriteLine("Última Movimentação: " + ProcessoEntidade.UltimaMovimentacaoProcessual);
            Console.WriteLine("Última Movimentação DATA: " + ProcessoEntidade.UltimaMovimentacaoProcessualData);

            TituloProcesso = "";
            TituloPartes = "";
            ComarcaInicial = "";
            UltimaMovimentacao = "";
            UltimaMovimentacaoProcessualData = "";

            //ActionsPJE.EncerrarConsole();



            return ProcessoEntidade;
 
        }

        public static void SalvarMovimentacaoProcessual(IList<IWebElement> ListaDeMovimentacaoProcessual,
            IWebDriver driver, Processo ProcessoEntidadeRetornado)
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
                            try
                            {
                                DateOnly data = DateOnly.ParseExact(dataString, "dd MMM yyyy", CultureInfo.CreateSpecificCulture("pt-br"));
                                Console.WriteLine("Data é: " + data);
                                ProcessoAtualizado.DataMovimentacao = data;
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine($"erro : {ex.Message}");
                                Console.WriteLine("teste erro ");
                            }
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

            //implementar aqui testes para abrir o menu


            IWebElement LinkDetalhesMovimentacaoProcessual = driver.FindElement(By.ClassName("titulo-topo"));


            LinkDetalhesMovimentacaoProcessual.Click();
            ActionsPJE.AguardarPje("Baixo");
            IWebElement Detalhes = driver.FindElement(By.Id("maisDetalhes"));

            IList<IWebElement> ElementosDentroDeDetalhes = Detalhes.FindElements(By.ClassName("dl-horizontal"));

            for(int zelta = 0; zelta < ElementosDentroDeDetalhes.Count; zelta++)
            {
                Console.WriteLine($"Cada elemento: {ElementosDentroDeDetalhes[zelta].Text}");
            }



            Console.WriteLine("terminei");
            ActionsPJE.EncerrarConsole();




            //obter elementos de DETALHES agora:

            ProcessoEntidadeRetornado.Comarca = "";


            Console.WriteLine("\n\n\n\n");

            foreach (var propriedade in typeof(Processo).GetProperties())
            {
                var valor = propriedade.GetValue(ProcessoEntidadeRetornado);
                Console.WriteLine($"{propriedade.Name}: {valor}");
            }

            Console.WriteLine("testeeeee");






        //public string Comarca { get; set; }
        //assunto
        //public string CausasProcesso { get; set; }
        //public DateOnly DataDistribuicao { get; set; }
        //public string ValorCausa { get; set; }
        //public bool JusGratis { get; set; }
        //public bool TutelaLiminar { get; set; }
        //public bool Prioridade { get; set; }










            //poloAtivo













            //SALVAR MOVIMENTAÇÃO PROCESSUAL AQUI E PROCESSO TAMBÉM

            //fecha detalhes
            LinkDetalhesMovimentacaoProcessual.Click();

            ActionsPJE.EncerrarConsole();

            ActionsPJE.RetornarParaJanelaPrincipal(driver);



        }


    }
}
    