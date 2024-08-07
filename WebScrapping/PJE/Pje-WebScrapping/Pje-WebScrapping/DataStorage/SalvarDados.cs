﻿using AngleSharp.Dom;
using Justo.Entities.Entidades;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Pje_WebScrapping.Actions;
using Pje_WebScrapping.Models;
using Pje_WebScrapping.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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

            //variavel para converter formato
            DateTime? UltimaMovimentacaoProcessualDataDateTime = null;





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
                if(string.IsNullOrEmpty(dataLimite))
                {
                    ProcessoEntidade.ProximoPrazoData = "Sem data próximo prazo";
                }
                else
                {
                    ProcessoEntidade.ProximoPrazoData = dataLimite;
                }

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

                    //if (DateTime.TryParseExact(UltimaMovimentacaoProcessualData.Trim(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ultimaMovimentacaoData))
                    //{
                    //    // Conversão bem-sucedida
                    //    UltimaMovimentacaoProcessualDataDateTime = ultimaMovimentacaoData;
                    //    Console.WriteLine($"Data convertida: {UltimaMovimentacaoProcessualDataDateTime}");

                    //}
                    //else
                    //{
                    //    // Conversão falhou, trate o erro ou defina um valor padrão
                    //    Console.WriteLine("Erro ao converter a data: " + UltimaMovimentacaoProcessualData);
                    //    UltimaMovimentacaoProcessualDataDateTime = DateTime.MinValue; // ou atribua um valor padrão adequado
                    //}
                    //ActionsPJE.EncerrarConsole();
                }

                ProcessoEntidade.TituloProcesso = TituloProcesso;
                ProcessoEntidade.PartesProcesso = TituloPartes;
                ProcessoEntidade.ComarcaInicial = ComarcaInicial;
                ProcessoEntidade.UltimaMovimentacaoProcessual = UltimaMovimentacao;
                ProcessoEntidade.UltimaMovimentacaoProcessualData = UltimaMovimentacaoProcessualData;

                //obtém a data da ultima movimentação processual para verificar no banco e comparar os valores.
                try
                {
                    DateTime.TryParseExact(UltimaMovimentacaoProcessualData.Trim(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None,out DateTime ultimaMovimentacaoData);
                    if(ultimaMovimentacaoData != null && ultimaMovimentacaoData != DateTime.MinValue)
                    {
                        UltimaMovimentacaoProcessualDataDateTime = ultimaMovimentacaoData;
                        Console.WriteLine($"Data convertida: {UltimaMovimentacaoProcessualDataDateTime}");
                    }

                }
                catch
                {

                }
                //ActionsPJE.EncerrarConsole();
            }

            Console.WriteLine("\n SegundaColDados");
            Console.WriteLine("Titulo processo: " + ProcessoEntidade.TituloProcesso);
            Console.WriteLine("Partes do Processo: " + ProcessoEntidade.PartesProcesso);
            Console.WriteLine("Comarca Inicial: " + ProcessoEntidade.ComarcaInicial);
            Console.WriteLine("Última Movimentação: " + ProcessoEntidade.UltimaMovimentacaoProcessual);
            Console.WriteLine("Última Movimentação DATA: " + ProcessoEntidade.UltimaMovimentacaoProcessualData);


            Console.WriteLine("\n\n\n");
            MostraDadosProcesso(ProcessoEntidade);
            Console.WriteLine("\n\n\n");


            TituloProcesso = "";
            TituloPartes = "";
            ComarcaInicial = "";
            UltimaMovimentacao = "";
            UltimaMovimentacaoProcessualData = "";



            var VerificaSeProcessoEstaSalvo = ConnectDB.LerProcessosPorPJECAcao(ProcessoEntidade.CodPJECAcao);
            if (VerificaSeProcessoEstaSalvo != null)
            {
                Console.WriteLine($"Localizei: {VerificaSeProcessoEstaSalvo.CodPJECAcao}");
            }
            else
            {

                ConnectDB.SalvarProcessoInicial(ProcessoEntidade);
            }

            //if(VerificaSeProcessoEstaSalvo.UltimaMovimentacaoProcessualData_DATETIME != UltimaMovimentacaoProcessualDataDateTime)
            //{
            //    //botar para dar continuidadade a salvar movimentacao processual aqui
            //}
            //if (!string.IsNullOrEmpty(UltimaMovimentacaoProcessualData))
            //{

            //}

            //dar continuidade a partir da verificao  do metodo LerProcesso
            var ProcessoVerifica = ConnectDB.LerProcesso(NumProcessoSegundaColDados); 
            if(ProcessoVerifica != null)
            {
                Console.WriteLine($"Inseri o processo no banco: {ProcessoVerifica.CodPJEC}");
            }
            else
            {
                Console.WriteLine($"{ProcessoEntidade.CodPJEC}");

            }
            UltimaMovimentacaoProcessualDataDateTime = null;
            //ActionsPJE.EncerrarConsole();
            return ProcessoEntidade;
 
        }

        public static void SalvarMovimentacaoProcessual(IWebDriver driver, Processo ProcessoEntidadeRetornado)
        {

            int ponto_de_parada = 0;
            //instanciar um novo perfil de advogado e salvar usando o perfil dele no banco?
            int ContadorDataAberturaProcessual = 0;


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
            for (int i = ElementosDentroDeMovimentacaoProcessual.Count - 1, j = 0; i >= 0; i--, j++)
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
                        ProcessoAtualizado.CodPJEC = ProcessoEntidadeRetornado.CodPJEC;

                        if (ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].GetAttribute("class").Contains("media data"))
                        {
                            Console.WriteLine("Acabaram os elementos DATA - continue - DATA ATUAL: " + ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].Text);
                            ContadorDataAberturaProcessual++;
                            if(ContadorDataAberturaProcessual == 1)
                            {
                                ProcessoEntidadeRetornado.DataAbertura = ActionsPJE.ConverterFormatoData(ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].Text);
                            }
                            // Atribuindo a data convertida à propriedade DataMovimentacao


                            continue;
                        }

                        //inserindo a data para os registros de movimentação processual
                        if (proximaPosicaoMediaData != -1)
                        {
                            string dataString = ElementosDentroDeMovimentacaoProcessualINVERTIDO[proximaPosicaoMediaData].Text;

                            try
                            {
                                //DateOnly dataConvertida = ActionsPJE.ConverterFormatoData(dataString);
                                DateTime dataConvertida = ActionsPJE.ConverterFormatoStringParaDatetime(dataString);
                                //Console.WriteLine("Data é: " + dataString);
                                Console.WriteLine("Data é: " + dataConvertida);
                                ProcessoAtualizado.DataMovimentacao = dataConvertida;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"erro : {ex.Message}");
                                Console.WriteLine("teste erro em aquisicao de data processoatualizado SalvarDados");
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
                            foreach (var testa in ListaConteudoMovimentoProcessual.Skip(1))
                            {
                                ConteudoTipoD += testa.Text + " ";
                            }

                            //insere o conteúdo da atualizacao para o objeto

                            ProcessoAtualizado.ConteudoAtualizacao = ConteudoTipoD;


                            //esvazia a string para a próxima atualização de dados
                            ConteudoTipoD = string.Empty;
                        }
                        else if (ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].GetAttribute("class").Contains("media interno tipo-M"))
                        {
                            //encotrando o TITULO da movimentação processual
                            IWebElement MediaBodyBoxTipoM = ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].FindElement(By.ClassName("media-body"));
                            IWebElement SpanTextoMovimentacao = MediaBodyBoxTipoM.FindElement(By.ClassName("texto-movimento"));

                            IList<IWebElement> ListaDeSpansNoMediaBodyBox = MediaBodyBoxTipoM.FindElements(By.ClassName("texto-movimento"));

                            ProcessoAtualizado.TituloMovimento = SpanTextoMovimentacao.Text;

                            if (ListaDeSpansNoMediaBodyBox.Count <= 1)
                            {
                                ProcessoAtualizado.ConteudoAtualizacao = "Sem Documentos ou Anexos no PJE";
                            }

                            //ALIMENTAR OS PROCESSOS AQUI

                            //ProcessoAtualizado.TituloMovimento = primeiroSpan.Text;

                        }


                        //inserindo a chave estrangeira de processo em processo atualizacao

                        var testeprocesso = ConnectDB.LerProcessosPorPJECAcao(ProcessoEntidadeRetornado.CodPJECAcao);

                        if(testeprocesso != null)
                        {
                            Console.WriteLine($"Processo encontrado e meu CodPJECAcao é: {testeprocesso.CodPJECAcao} meu ID é: {testeprocesso.Id}");
                        
                        }

                        //recebe o ID DIRETAMENTE DO BANCO da chave estrangeira da tabela processo em processoatualizacao
                        ProcessoAtualizado.ProcessoId = testeprocesso.Id;
                        ProcessoAtualizado.PJECAcao = testeprocesso.CodPJECAcao;
                        ProcessoAtualizado.Nome = "Vazio";
                        //atualizando entidade base:
                        ProcessoAtualizado.DataCadastro = DateTime.Now;
                        ProcessoAtualizado.CadastradoPor = 5;
                        ProcessoAtualizado.DataAtualizacao = DateTime.Now;
                        ProcessoAtualizado.AtualizadoPor = 5;
                        ListaProcessosAtualizados.Add(ProcessoAtualizado);

                        //5 será o numero para o webscrapping

                        //foreach (var propriedade in typeof(Processo).GetProperties())
                        //{
                        //    var valor = propriedade.GetValue(testeprocesso);
                        //    Console.WriteLine($"{propriedade.Name}: {valor}");


                        //}
                        //Console.WriteLine("teste");


                        //insere o objeto na lista


                        //ConnectDB.SalvarProcessoMovimentacaoProcessual(ListaProcessosAtualizados);



                        //reinicia a lista após terminar ela
                        //ListaProcessosAtualizados.Clear();


                        //realizar de para dos objetos aqui
                        //implementar parametro em salvar movimentacao processual ( método ) para que dê o objeto processo
                        //para obter seu numero de processo e outros dados caso necessário






                        Console.WriteLine("Elemento: " + j + " :  " + ElementosDentroDeMovimentacaoProcessualINVERTIDO[j].Text);

                        //Console.WriteLine("\n\n\n\n Lendo Processo atualizado");

                        //foreach (var propriedade in typeof(ProcessoAtualizacao).GetProperties())
                        //{
                        //    var valor = propriedade.GetValue(ProcessoAtualizado);
                        //    Console.WriteLine($"{propriedade.Name}: {valor}");


                        //}

                        //Console.WriteLine("Acabei de ler: " + ProcessoAtualizado.CodPJEC + " atualizados agora!");

                    }
                    posicao_inicial = proximaPosicaoMediaData;
                }

                posicao++;
                LocalizadorElementoMediaData++;

                // Verifica se é o último elemento
                if (i == ElementosDentroDeMovimentacaoProcessualINVERTIDO.Count - 1)
                {
                    ConnectDB.SalvarProcessoMovimentacaoProcessual(ListaProcessosAtualizados);
                }

                //for para verificar as proximas elementos datas no vetor
                if (proximaPosicaoMediaData == -1)
                {
                    Console.WriteLine("Acabaram os elementos DATA ");
                    break;
                }
            }
            //ActionsPJE.EncerrarConsole();

            //ActionsPJE.EncerrarConsole();

            //testando a lista de movimentacao processual


            Console.WriteLine("\n\n\n\n");

            //Console.WriteLine("Encerrei");
            //ActionsPJE.EncerrarConsole();

            //ActionsPJE.EncerrarConsole();

            //implementar aqui testes para abrir o menu

            //AQUI SERÁ LIDO OS DETALHES DO PROCESSO EM MOVIMENTACAO PROCESSUAL.
            IWebElement LinkDetalhesMovimentacaoProcessual = driver.FindElement(By.ClassName("titulo-topo"));

            //aqui abre o menu de detalhes ( acima da movimentacao processual )
            LinkDetalhesMovimentacaoProcessual.Click();
            ActionsPJE.AguardarPje("Baixo");
            IWebElement Detalhes = driver.FindElement(By.Id("maisDetalhes"));

            //POLO ATIVO

            IWebElement PoloAtivo = driver.FindElement(By.Id("poloAtivo"));
            IList<IWebElement> ElementosPoloAtivo = PoloAtivo.FindElements(By.TagName("span"));

            List<string> ElementosPoloAtivoUNICOS = new List<string>();

            //Polo Passivo

            IWebElement PoloPassivo = driver.FindElement(By.Id("poloPassivo"));
            IList<IWebElement> ElementosPoloPassivo = PoloPassivo.FindElements(By.TagName("span"));

            List<string> ElementosPoloPassivoUNICOS = new List<string>();

            //fazendo uma lista de elementos do polo ativo sem DUPLICATAS

            foreach (var elemento in ElementosPoloAtivo)
            {
                if (!ElementosPoloAtivoUNICOS.Contains(elemento.Text))
                {
                    ElementosPoloAtivoUNICOS.Add(elemento.Text);
                }
            }

            foreach (var elemento in ElementosPoloPassivo)
            {
                if (!ElementosPoloPassivoUNICOS.Contains(elemento.Text))
                {
                    ElementosPoloPassivoUNICOS.Add(elemento.Text);
                }
            }

            IList<IWebElement> ElementosDentroDeDetalhes = Detalhes.FindElements(By.TagName("dt"));


            //detalhes aba lateral esquerda
            for (int zelta = 0; zelta < ElementosDentroDeDetalhes.Count; zelta++)
            {
                // Extrai o texto de <dt> e <dd> correspondente

                //tag do conteudo
                string dtText = ElementosDentroDeDetalhes[zelta].Text;

                //conteudo 
                string ddText = ElementosDentroDeDetalhes[zelta].FindElement(By.XPath("./following-sibling::dd")).Text;

                if (((!string.IsNullOrEmpty(ddText) || !string.IsNullOrEmpty(dtText)) && !string.IsNullOrEmpty(dtText)) && dtText == "Assunto")
                {
                    ProcessoEntidadeRetornado.MotivosProcesso = ddText;
                    Console.WriteLine($"{dtText}: {ddText}");
                }
                else if (((!string.IsNullOrEmpty(ddText) || !string.IsNullOrEmpty(dtText)) && !string.IsNullOrEmpty(dtText)) && dtText == "Competência")
                {
                    ProcessoEntidadeRetornado.Competencia = ddText;
                    Console.WriteLine($"{dtText}: {ddText}");
                }
                else if (((!string.IsNullOrEmpty(ddText) || !string.IsNullOrEmpty(dtText)) && !string.IsNullOrEmpty(dtText)) && dtText == "Órgão julgador")
                {
                    ProcessoEntidadeRetornado.OrgaoJulgador = ddText;
                    Console.WriteLine($"{dtText}: {ddText}");
                }
                else if (((!string.IsNullOrEmpty(ddText) || !string.IsNullOrEmpty(dtText)) && !string.IsNullOrEmpty(dtText)) && dtText == "Valor da causa")
                {
                    ProcessoEntidadeRetornado.ValorCausa = ddText;
                    Console.WriteLine($"{dtText}: {ddText}");
                }
                else if (((!string.IsNullOrEmpty(ddText) || !string.IsNullOrEmpty(dtText)) && !string.IsNullOrEmpty(dtText)) && dtText == "Segredo de justiça?")
                {
                    ProcessoEntidadeRetornado.SegredoJustica = ddText;
                    Console.WriteLine($"{dtText}: {ddText}");
                }
                else if (((!string.IsNullOrEmpty(ddText) || !string.IsNullOrEmpty(dtText)) && !string.IsNullOrEmpty(dtText)) && dtText == "Justiça gratuita?")
                {
                    ProcessoEntidadeRetornado.JusGratis = ddText;
                    Console.WriteLine($"{dtText}: {ddText}");
                }
                else if (((!string.IsNullOrEmpty(ddText) || !string.IsNullOrEmpty(dtText)) && !string.IsNullOrEmpty(dtText)) && dtText == "Tutela/liminar?")
                {
                    ProcessoEntidadeRetornado.TutelaLiminar = ddText;
                    Console.WriteLine($"{dtText}: {ddText}");
                }
                else if (((!string.IsNullOrEmpty(ddText) || !string.IsNullOrEmpty(dtText)) && !string.IsNullOrEmpty(dtText)) && dtText == "Prioridade?")
                {
                    ProcessoEntidadeRetornado.Prioridade = ddText;
                    Console.WriteLine($"{dtText}: {ddText}");
                }
                else if (((!string.IsNullOrEmpty(ddText) || !string.IsNullOrEmpty(dtText)) && !string.IsNullOrEmpty(dtText)) && dtText == "Autuação")
                {
                    ProcessoEntidadeRetornado.Autuacao = ddText;
                    Console.WriteLine($"{dtText}: {ddText}");
                }
                else if (((!string.IsNullOrEmpty(ddText) || !string.IsNullOrEmpty(dtText)) && !string.IsNullOrEmpty(dtText)) && dtText == "Jurisdição")
                {
                    ProcessoEntidadeRetornado.Comarca = ddText;
                    Console.WriteLine($"{dtText}: {ddText}");
                }



                // Imprime o resultado
                //Console.WriteLine($"{dtText}: {ddText}");
            }


            //verificando elementos do polo ativo unico
            string NomePartePolo = "";
            string ParteCpfPolo = "";
            ProcessoEntidadeRetornado.PoloAtivo = new();
            foreach (var DentroPoloAtivo in ElementosPoloAtivoUNICOS)
            {
                if (ProcessoEntidadeRetornado.PoloAtivo == null)
                {
                    ProcessoEntidadeRetornado.PoloAtivo = new Polo(); // ou qualquer método de inicialização apropriado
                }
                if (DentroPoloAtivo.Contains("(AUTOR)"))
                {
                    Console.WriteLine($"Autor é : {DentroPoloAtivo}");
                    if (DentroPoloAtivo.Contains("CNPJ"))
                    {
                        var cnpjParte = ActionsPJE.ExtrairCNPJDeDetalhes(DentroPoloAtivo).Trim();
                        var RazaoSocial = ActionsPJE.ExtrairNomeDeDetalhes(DentroPoloAtivo).Trim();
                        ProcessoEntidadeRetornado.PoloAtivo.TipoParte = "PJ";
                        ProcessoEntidadeRetornado.PoloAtivo.CPFCNPJParte = cnpjParte;
                        ProcessoEntidadeRetornado.PoloAtivo.NomeParte = RazaoSocial;
                        if (!string.IsNullOrEmpty(RazaoSocial))
                        {
                            ProcessoEntidadeRetornado.PoloAtivo.NomeParte = RazaoSocial;
                        }
                        if (!string.IsNullOrEmpty(cnpjParte))
                        {
                            ProcessoEntidadeRetornado.PoloAtivo.CPFCNPJParte = cnpjParte;
                        }
                    }
                    else
                    {
                        NomePartePolo = ActionsPJE.ExtrairNomeDeDetalhes(DentroPoloAtivo).Trim();
                        ParteCpfPolo = ActionsPJE.ExtrairCPFDeDetalhes(DentroPoloAtivo).Trim();
                        ProcessoEntidadeRetornado.PoloAtivo.TipoParte = "PF";
                        if (!string.IsNullOrEmpty(NomePartePolo))
                        {
                            ProcessoEntidadeRetornado.PoloAtivo.NomeParte = NomePartePolo;
                        }
                        if (!string.IsNullOrEmpty(ParteCpfPolo))
                        {
                            ProcessoEntidadeRetornado.PoloAtivo.CPFCNPJParte = ParteCpfPolo;
                        }
                    }

                }
                else if (DentroPoloAtivo.Contains("(ADVOGADO)"))
                {
                    Console.WriteLine($"Adv é : {DentroPoloAtivo}");
                    var nomeAdvogado = ActionsPJE.ExtrairNomeDeDetalhes(DentroPoloAtivo).Trim();
                    var oabAdvogado = ActionsPJE.ExtrairOABDeDetalhes(DentroPoloAtivo).Trim();
                    var cpfAdvogado = ActionsPJE.ExtrairCPFDeDetalhes(DentroPoloAtivo).Trim();


                    //dados da natali ADV
                    if ((cpfAdvogado == "152.489.457-55" || oabAdvogado == "RJ253001") && (nomeAdvogado == "NATALI CORDEIRO MARQUES"))
                    {
                        ProcessoEntidadeRetornado.ClienteCPF = ParteCpfPolo;
                        ProcessoEntidadeRetornado.Cliente = NomePartePolo;
                        ProcessoEntidadeRetornado.Advogada = nomeAdvogado;
                        ProcessoEntidadeRetornado.AdvogadaOAB = oabAdvogado;
                        ProcessoEntidadeRetornado.AdvogadaCPF = cpfAdvogado;
                    }



                    if (!string.IsNullOrEmpty(nomeAdvogado))
                    {
                        ProcessoEntidadeRetornado.PoloAtivo.NomeAdvogado = nomeAdvogado;
                    }
                    if (!string.IsNullOrEmpty(oabAdvogado))
                    {
                        ProcessoEntidadeRetornado.PoloAtivo.OAB = oabAdvogado;
                    }
                    if (!string.IsNullOrEmpty(cpfAdvogado))
                    {
                        ProcessoEntidadeRetornado.PoloAtivo.CPFAdvogado = cpfAdvogado;
                    }
                }
                else if (DentroPoloAtivo.Contains("(RÉU)"))
                {
                    Console.WriteLine($"Réu é : {DentroPoloAtivo}");
                    if (DentroPoloAtivo.Contains("CNPJ"))
                    {

                        var cnpjParte = ActionsPJE.ExtrairCNPJDeDetalhes(DentroPoloAtivo).Trim();
                        var RazaoSocial = ActionsPJE.ExtrairNomeDeDetalhes(DentroPoloAtivo).Trim();
                        ProcessoEntidadeRetornado.PoloAtivo.TipoParte = "PJ";
                        ProcessoEntidadeRetornado.PoloAtivo.CPFCNPJParte = cnpjParte;
                        ProcessoEntidadeRetornado.PoloAtivo.NomeParte = RazaoSocial;
                        if (!string.IsNullOrEmpty(RazaoSocial))
                        {
                            ProcessoEntidadeRetornado.PoloAtivo.NomeParte = RazaoSocial;
                        }
                        if (!string.IsNullOrEmpty(cnpjParte))
                        {
                            ProcessoEntidadeRetornado.PoloAtivo.CPFCNPJParte = cnpjParte;
                        }
                    }
                    else
                    {
                        var cpfPolo = ActionsPJE.ExtrairCPFDeDetalhes(DentroPoloAtivo).Trim();
                        var nomeReu = ActionsPJE.ExtrairNomeDeDetalhes(DentroPoloAtivo).Trim();
                        ProcessoEntidadeRetornado.PoloAtivo.TipoParte = "PF";
                        if (!string.IsNullOrEmpty(nomeReu))
                        {
                            ProcessoEntidadeRetornado.PoloAtivo.NomeParte = nomeReu;
                        }
                        if (!string.IsNullOrEmpty(cpfPolo))
                        {
                            ProcessoEntidadeRetornado.PoloAtivo.CPFCNPJParte = cpfPolo;
                        }
                    }
                }


            }

            ProcessoEntidadeRetornado.PoloPassivo = new();
            //verificando elementos do polo passivo
            foreach (var DentroPoloPassivo in ElementosPoloPassivoUNICOS)
            {
                if (ProcessoEntidadeRetornado.PoloPassivo == null)
                {
                    ProcessoEntidadeRetornado.PoloPassivo = new Polo(); // ou qualquer método de inicialização apropriado
                }
                if (DentroPoloPassivo.Contains("(AUTOR)"))
                {
                    Console.WriteLine($"Autor é : {DentroPoloPassivo}");
                    if (DentroPoloPassivo.Contains("CNPJ"))
                    {
                        var cnpjParte = ActionsPJE.ExtrairCNPJDeDetalhes(DentroPoloPassivo).Trim();
                        var RazaoSocial = ActionsPJE.ExtrairNomeDeDetalhes(DentroPoloPassivo).Trim();

                        Console.WriteLine("cnpj do passivo e " + cnpjParte);

                        ProcessoEntidadeRetornado.PoloPassivo.TipoParte = "PJ";
                        ProcessoEntidadeRetornado.PoloPassivo.CPFCNPJParte = cnpjParte;
                        ProcessoEntidadeRetornado.PoloPassivo.NomeParte = RazaoSocial;
                        if (!string.IsNullOrEmpty(RazaoSocial))
                        {
                            ProcessoEntidadeRetornado.PoloPassivo.NomeParte = RazaoSocial;
                        }
                        if (!string.IsNullOrEmpty(cnpjParte))
                        {
                            ProcessoEntidadeRetornado.PoloPassivo.CPFCNPJParte = cnpjParte;
                        }
                    }
                    else
                    {
                        NomePartePolo = ActionsPJE.ExtrairNomeDeDetalhes(DentroPoloPassivo).Trim();
                        ParteCpfPolo = ActionsPJE.ExtrairCPFDeDetalhes(DentroPoloPassivo).Trim();
                        ProcessoEntidadeRetornado.PoloPassivo.TipoParte = "PF";
                        if (!string.IsNullOrEmpty(NomePartePolo))
                        {
                            ProcessoEntidadeRetornado.PoloPassivo.NomeParte = NomePartePolo;
                        }
                        if (!string.IsNullOrEmpty(ParteCpfPolo))
                        {
                            ProcessoEntidadeRetornado.PoloPassivo.CPFCNPJParte = ParteCpfPolo;
                        }

                    }

                }
                else if (DentroPoloPassivo.Contains("(ADVOGADO)"))
                {
                    Console.WriteLine($"Adv é : {DentroPoloPassivo}");
                    var nomeAdvogado = ActionsPJE.ExtrairNomeDeDetalhes(DentroPoloPassivo).Trim();
                    var oabAdvogado = ActionsPJE.ExtrairOABDeDetalhes(DentroPoloPassivo).Trim();
                    var cpfAdvogado = ActionsPJE.ExtrairCPFDeDetalhes(DentroPoloPassivo).Trim();


                    //dados da natali ADV
                    if ((cpfAdvogado == "152.489.457-55" || oabAdvogado == "RJ253001") && (nomeAdvogado == "NATALI CORDEIRO MARQUES"))
                    {
                        ProcessoEntidadeRetornado.ClienteCPF = ParteCpfPolo;
                        ProcessoEntidadeRetornado.Cliente = NomePartePolo;
                        ProcessoEntidadeRetornado.Advogada = nomeAdvogado;
                        ProcessoEntidadeRetornado.AdvogadaOAB = oabAdvogado;
                        ProcessoEntidadeRetornado.AdvogadaCPF = cpfAdvogado;
                    }




                    if (!string.IsNullOrEmpty(nomeAdvogado))
                    {
                        ProcessoEntidadeRetornado.PoloPassivo.NomeAdvogado = nomeAdvogado;
                    }
                    if (!string.IsNullOrEmpty(oabAdvogado))
                    {
                        ProcessoEntidadeRetornado.PoloPassivo.OAB = oabAdvogado;
                    }
                    if (!string.IsNullOrEmpty(cpfAdvogado))
                    {
                        ProcessoEntidadeRetornado.PoloPassivo.CPFAdvogado = cpfAdvogado;
                    }
                }
                else if (DentroPoloPassivo.Contains("(RÉU)"))
                {
                    Console.WriteLine($"Réu é : {DentroPoloPassivo}");
                    if (DentroPoloPassivo.Contains("CNPJ"))
                    {

                        var cnpjParte = ActionsPJE.ExtrairCNPJDeDetalhes(DentroPoloPassivo).Trim();
                        var RazaoSocial = ActionsPJE.ExtrairNomeDeDetalhes(DentroPoloPassivo).Trim();
                        ProcessoEntidadeRetornado.PoloPassivo.TipoParte = "PJ";
                        ProcessoEntidadeRetornado.PoloPassivo.CPFCNPJParte = cnpjParte;
                        ProcessoEntidadeRetornado.PoloPassivo.NomeParte = RazaoSocial;
                        if (!string.IsNullOrEmpty(RazaoSocial))
                        {
                            ProcessoEntidadeRetornado.PoloPassivo.NomeParte = RazaoSocial;
                        }
                        if (!string.IsNullOrEmpty(cnpjParte))
                        {
                            ProcessoEntidadeRetornado.PoloPassivo.CPFCNPJParte = cnpjParte;
                        }
                    }
                    else
                    {
                        var cpfPolo = ActionsPJE.ExtrairCPFDeDetalhes(DentroPoloPassivo).Trim();
                        var nomeReu = ActionsPJE.ExtrairNomeDeDetalhes(DentroPoloPassivo).Trim();
                        ProcessoEntidadeRetornado.PoloPassivo.TipoParte = "PF";
                        if (!string.IsNullOrEmpty(nomeReu))
                        {
                            ProcessoEntidadeRetornado.PoloPassivo.NomeParte = nomeReu;
                        }
                        if (!string.IsNullOrEmpty(cpfPolo))
                        {
                            ProcessoEntidadeRetornado.PoloPassivo.CPFCNPJParte = cpfPolo;
                        }
                    }
                }
            }

            ProcessoEntidadeRetornado.PartesProcesso = ProcessoEntidadeRetornado.PoloAtivo.NomeParte + " x " + ProcessoEntidadeRetornado.PoloPassivo.NomeParte ;












            //alimentando entidade base
            ProcessoEntidadeRetornado.DataCadastro = DateTime.Now;
            ProcessoEntidadeRetornado.CadastradoPor = 5;
            ProcessoEntidadeRetornado.DataAtualizacao = DateTime.Now;
            ProcessoEntidadeRetornado.AtualizadoPor = 5;



            //fazer o UPDATE AQUI dos dados de detalhes.



            ElementosPoloAtivoUNICOS.Clear();
            ElementosPoloPassivoUNICOS.Clear();


            //ActionsPJE.EncerrarConsole();

            Console.WriteLine("\n\n\n\n");

            foreach (var propriedade in typeof(Processo).GetProperties())
            {
                //listando atributos do objeto

                var valor = propriedade.GetValue(ProcessoEntidadeRetornado);
                Console.WriteLine($"{propriedade.Name}: {valor}");

            }

            //fazer update aqui de tudo que foi obtido, verificar dentro do for se as variaveis estao chegando normalmente
            //e tal.


                Console.WriteLine("\n\n\n\n Listando POLO ATIVO");
                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloAtivo.NomeParte}");
                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloAtivo.TipoParte}");
                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloAtivo.CPFCNPJParte}");
                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloAtivo.NomeAdvogado}");
                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloAtivo.CPFAdvogado}");
                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloAtivo.OAB}");

                Console.WriteLine("\n\n\n\n Listando POLO PASSIVO");

                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloPassivo.NomeParte}");
                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloPassivo.TipoParte}");
                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloPassivo.CPFCNPJParte}");
                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloPassivo.NomeAdvogado}");
                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloPassivo.CPFAdvogado}");
                Console.WriteLine($"{ProcessoEntidadeRetornado.PoloPassivo.OAB}");
                if(string.IsNullOrEmpty(ProcessoEntidadeRetornado.Nome))
                {
                    ProcessoEntidadeRetornado.Nome = "Sem nome";
                }

                var LocalizaProcessoComId = ConnectDB.LerProcesso(ProcessoEntidadeRetornado.CodPJEC);
                Cliente ClienteASerFormado = new();

                if (LocalizaProcessoComId != null)
                {
                    Console.WriteLine($"Processo encontrado e meu id é: {LocalizaProcessoComId.CodPJEC} : {LocalizaProcessoComId.Id}");
                    ClienteASerFormado = new()
                    {
                        EnderecoId = null,
                        Nome = ProcessoEntidadeRetornado.Cliente,
                        Cpf = ProcessoEntidadeRetornado.ClienteCPF,
                        NomeMae = null,
                        Rg = null,
                        ComprovanteDeResidencia = null,
                        Cnh = null,
                        ContratoSocialCliente = null,
                        Cnpj = null,
                        CertificadoReservista = null,
                        ProcuracaoRepresentacaoLegal = null,
                        PisPasep = null,
                        CodClt = null,
                        NIS = null,
                        Genero = null,
                        DataNascimento = null,
                        Ocupacao = null,
                        Renda = null,
                        Escolaridade = null,
                        Nacionalidade = null,
                        EstadoCivil = null,
                        Banco = null,
                        AgenciaBancaria = null,
                        ContaCorrente = null,
                        Telefone = null,
                        Contato = null,
                        Email = null,
                        Tipo = null,
                        ReuAutor = null,
                        DataCadastro = DateTime.Now,
                        CadastradoPor = ProcessoEntidadeRetornado.CadastradoPor,
                        DataAtualizacao = DateTime.Now,
                        AtualizadoPor = ProcessoEntidadeRetornado.AtualizadoPor
                    };
                }
                ConnectDB.InserirCliente(ClienteASerFormado);
                //inserir cliente antes e botar processoentidaderetornado para receber clienteid
                //precisa fazer o LERCLIENTE para devolver corretamente o id da chave estrangeira
                Cliente ClienteDoBanco = ConnectDB.LerCliente(ClienteASerFormado.Cpf);
                Console.WriteLine(ClienteDoBanco.Id);
                MostraDadosProcesso(ProcessoEntidadeRetornado);
                Console.WriteLine("parar");
                //recebe o ID DIRETAMENTE DO BANCO da chave estrangeira da tabela processo em processoatualizacao
                //ProcessoAtualizado.ProcessoId = testeprocesso.Id;


                Polo PoloAtivoDTO = new Polo 
                {
                    ProcessoId = LocalizaProcessoComId.Id,
                    NomeParte = ProcessoEntidadeRetornado.PoloAtivo.NomeParte,
                    CPFCNPJParte = ProcessoEntidadeRetornado.PoloAtivo.CPFCNPJParte,
                    NomeAdvogado = ProcessoEntidadeRetornado.PoloAtivo.NomeAdvogado,
                    CPFAdvogado = ProcessoEntidadeRetornado.PoloAtivo.CPFAdvogado,
                    OAB = ProcessoEntidadeRetornado.PoloAtivo.OAB,
                    Nome = ProcessoEntidadeRetornado.Nome,
                    CadastradoPor = ProcessoEntidadeRetornado.CadastradoPor,
                    DataCadastro = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                    AtualizadoPor = ProcessoEntidadeRetornado.AtualizadoPor
                };
                Polo PoloPassivoDTO = new Polo
                {
                    ProcessoId = LocalizaProcessoComId.Id,
                    NomeParte = ProcessoEntidadeRetornado.PoloPassivo.NomeParte,
                    CPFCNPJParte = ProcessoEntidadeRetornado.PoloPassivo.CPFCNPJParte,
                    NomeAdvogado = ProcessoEntidadeRetornado.PoloPassivo.NomeAdvogado,
                    CPFAdvogado = ProcessoEntidadeRetornado.PoloPassivo.CPFAdvogado,
                    OAB = ProcessoEntidadeRetornado.PoloPassivo.OAB,
                    Nome = ProcessoEntidadeRetornado.Nome,
                    CadastradoPor = ProcessoEntidadeRetornado.CadastradoPor,
                    DataCadastro = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                    AtualizadoPor = ProcessoEntidadeRetornado.AtualizadoPor
                };

                List<Polo> PolosDoProcesso = new();
                PolosDoProcesso.Add(PoloAtivoDTO);
                PolosDoProcesso.Add(PoloPassivoDTO);
                ConnectDB.InserirPolosPartes(PolosDoProcesso);

                Advogado AdvogadoParaInserir = new Advogado()
                {
                    Nome = ProcessoEntidadeRetornado.Advogada,
                    Cpf = ProcessoEntidadeRetornado.AdvogadaCPF,
                    Oab = ProcessoEntidadeRetornado.AdvogadaOAB,
                    CadastradoPor = ProcessoEntidadeRetornado.CadastradoPor,
                    AtualizadoPor = ProcessoEntidadeRetornado.AtualizadoPor,
                    DataCadastro = DateTime.Now,
                    DataAtualizacao = DateTime.Now
                };


                ConnectDB.InserirAdvogados(AdvogadoParaInserir);
                var AdvogadoDoBanco = ConnectDB.LerAdvogado(AdvogadoParaInserir.Cpf);
                if(AdvogadoDoBanco != null)
                {
                    ProcessoEntidadeRetornado.AdvogadoId = AdvogadoDoBanco.Id;
                }

                ProcessoEntidadeRetornado.ClienteId = ClienteDoBanco.Id;
                ProcessoEntidadeRetornado.AdvogadoId = AdvogadoDoBanco.Id;
                ConnectDB.AtualizarProcessoInicial(ProcessoEntidadeRetornado);
                Console.WriteLine($"\n\n\n\n meu clienteid é: {ProcessoEntidadeRetornado.ClienteId} e advogadoid é: {ProcessoEntidadeRetornado.AdvogadoId}");

                //fazer aqui o insert na tabela POLO com esses dados, n esquecer de inserir as chaves estrangeiras
                //nos locais certos.


                Console.WriteLine("Encerrei");


                //ActionsPJE.EncerrarConsole();


                //SALVAR MOVIMENTAÇÃO PROCESSUAL AQUI E PROCESSO TAMBÉM

                //fecha detalhes
                LinkDetalhesMovimentacaoProcessual.Click();

                //ActionsPJE.EncerrarConsole();

                ActionsPJE.RetornarParaJanelaPrincipal(driver);



            }

            public static void MostraDadosProcesso(Processo ProcessoEntidadeParaImprimir)
            {
                foreach (var propriedade in typeof(Processo).GetProperties())
                {
                    //listando atributos do objeto

                    var valor = propriedade.GetValue(ProcessoEntidadeParaImprimir);
                    Console.WriteLine($"{propriedade.Name}: {valor}");

                }
            }
        public static void MostraDadosPolo(List<Polo> Polos)
        {
            Console.WriteLine("Iniciando MostraDadosPolo:");
            foreach (var polo in Polos)
            {
                foreach (var propriedade in typeof(Polo).GetProperties())
                {
                    // listando atributos do objeto
                    var valor = propriedade.GetValue(polo);
                    Console.WriteLine($"{propriedade.Name}: {valor}");
                }
            }
        }
    }


}
    