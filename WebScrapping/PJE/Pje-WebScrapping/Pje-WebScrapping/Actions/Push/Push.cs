using Justo.Entities.Entidades;
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

            //ActionsPJE.AguardarPje("Baixo");

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
                    var data_inclusao = PushActions.ETLDataInclusao(TdsDentroDoTr[2].Text);


                    //Observacao
                    var observacao = PushActions.ETLObservacao(TdsDentroDoTr[3].Text.ToString());

                    Console.WriteLine($"nº: {numeroprocesso}; data: {data_inclusao}; observacao: {observacao}");

                    Processo ProcessoFormadoPush = new()
                    {
                        Cliente = observacao,
                        CodPJEC = numeroprocesso,
                        DataAberturaDATETIME = data_inclusao,
                    };

                    //inserindo o PROCESSO INICIAL AGORA, antes de abrir o movimentação processual dele:
                    ConnectDB.SalvarProcessoInicial(ProcessoFormadoPush);
                    //Console.WriteLine("teste");



                    //INICIAREMOS MOVIMENTACAO PROCESSUAL AQUI:
                    ActionsPJE.AguardarPje("Baixo");
                    //clica no botão para ver a movimentação processual
                    BotaoLinkMovimentacaoProcessual[0].Click();
                    ActionsPJE.AguardarPje("Baixo");
                    //métiodo pára fazer a extração de movimentação processual



                    
                    PushMovimentacaoProcessual(PushWebDriver, Janela_Principal, ProcessoFormadoPush);
                }
                else
                {
                    Console.WriteLine("Sem processos a cadastrar.");
                    return PushWebDriver;
                }
            }
            //TabelaProcessos.FindElements(By.TagName("tr"));


            // essa tabela TERÁ QUEBRA DE PÁGINA.
            //vai quebbrar em algum momento aqui, ficar atento quanto a isso.
            return PushWebDriver;
        }


        //método que vai gerir toda a obtenção das informações da movimentação e do topo tambem.
        public static void PushMovimentacaoProcessual(IWebDriver driver, string Janela_Principal, Processo ProcessoPushInicial)
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
                        PushActions.DescerBarraDeRolagemPUSH(driver, "divTimeLine:divEventosTimeLine");

                        Console.WriteLine("Mudei para: " + driver.Title);
                        ActionsPJE.AguardarPje("Baixo");

                        IList<IWebElement> movimentacaoprocessual = driver.FindElements(By.Id("divTimeLine:eventosTimeLineElement"));

                        Console.WriteLine("\n\n\n\n\n O que é salvar dados processo: \n\n");




                        Console.WriteLine("\n\n\n\n\n O que é movimentação processual: \n\n");
                        //aqui inicia o webscrapping para armazenar a movimentação processual do processo.



                        //precisa colocar aqui movimentacaoprocessual antes de ir para detalhes


                        Console.WriteLine("Iniciando movimentação processual:");
                        SalvarMovimentacaoProcessual(driver, ProcessoPushInicial);

                        Console.WriteLine("Iniciando Detalhes TOPO:");
                        //leitura do topo do processo
                        MovimentacaoProcessualDetalhesPush(driver, ProcessoPushInicial);




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
                            if (ContadorDataAberturaProcessual == 1)
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

                        var testeprocesso = ConnectDB.LerProcesso(ProcessoEntidadeRetornado.CodPJEC);

                        if (testeprocesso != null)
                        {
                            Console.WriteLine($"Processo encontrado e meu CodPJEC é: {testeprocesso.CodPJEC} meu ID é: {testeprocesso.Id}");

                        }

                        //recebe o ID DIRETAMENTE DO BANCO da chave estrangeira da tabela processo em processoatualizacao
                        ProcessoAtualizado.ProcessoId = testeprocesso.Id;
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
        }




            //método que faz a leitura do TOPO do processo, onde constam as informações dos processos.
            public static void MovimentacaoProcessualDetalhesPush(IWebDriver driver, Processo ProcessoEntidadeRetornado)
            {
            int ContadorPartes = 0;
            int ContadorPartesPassivo = 0;
            
            IWebElement LinkDetalhesMovimentacaoProcessual = driver.FindElement(By.ClassName("titulo-topo"));

            //aqui abre o menu de detalhes ( acima da movimentacao processual )
            LinkDetalhesMovimentacaoProcessual.Click();
            ActionsPJE.AguardarPje("Baixo");
            IWebElement Detalhes = driver.FindElement(By.Id("maisDetalhes"));

            //POLO ATIVO

            IWebElement PoloAtivo = driver.FindElement(By.Id("poloAtivo"));


            IList<IWebElement> ElementosPoloAtivo = PoloAtivo.FindElements(By.TagName("span"));
            if (ElementosPoloAtivo.Count > 0)
            {
                IList<IWebElement> LisConsorcioAtivo = new List<IWebElement>();

                foreach (var registro in ElementosPoloAtivo)
                {
                    if (registro.Text.Contains("AUTOR") || registro.Text.Contains("RÉU"))
                    {
                        ContadorPartes++;
                        LisConsorcioAtivo.Add(registro);
                    }
                }



                //o span do réu ou autor está dentro de outro spán então ele contabiliza como se fosse 2 span para cada autor ou reu.
                //portanto precisa ser maior que 2 para configurar como lisconsorcio.
                if (ContadorPartes > 2)
                {
                    Console.WriteLine($"Processo {ProcessoEntidadeRetornado.CodPJEC} tem mais de uma pessoa no Polo ATIVO.");
                }

            }

            List<string> ElementosPoloAtivoUNICOS = new List<string>();


            //Polo Passivo

            IWebElement PoloPassivo = driver.FindElement(By.Id("poloPassivo"));
            IList<IWebElement> ElementosPoloPassivo = PoloPassivo.FindElements(By.TagName("span"));
            List<string> ElementosPoloPassivoUNICOS = new List<string>();
            IList<IWebElement> LisConsorcioPassivo = new List<IWebElement>();

            if (ElementosPoloPassivo.Count > 0)
            {
                foreach (var registro in ElementosPoloPassivo)
                {
                    if (registro.Text.Contains("AUTOR") || registro.Text.Contains("RÉU"))
                    {
                        ContadorPartesPassivo++;
                        LisConsorcioPassivo.Add(registro);
                    }
                }
                //o span do réu ou autor está dentro de outro spán então ele contabiliza como se fosse 2 span para cada autor ou reu.
                //portanto precisa ser maior que 2 para configurar como lisconsorcio.
                if (ContadorPartesPassivo > 2)
                {
                    Console.WriteLine($"Processo {ProcessoEntidadeRetornado.CodPJEC} tem mais de uma pessoa no Polo PASSIVO.");
                }

            }


            //fazendo uma lista de elementos do polo ativo sem DUPLICATAS
            foreach (var elemento in ElementosPoloAtivo)
            {
                if (!ElementosPoloAtivoUNICOS.Contains(elemento.Text) && !ElementosPoloAtivoUNICOS.Equals("Polo ativo"))
                {
                    ElementosPoloAtivoUNICOS.Add(elemento.Text);
                    Console.WriteLine($"sou: " + elemento.Text);
                }
            }
            foreach (var elemento in ElementosPoloPassivo)
            {
                if (!ElementosPoloPassivoUNICOS.Contains(elemento.Text) && !ElementosPoloAtivoUNICOS.Equals("Polo Passivo"))
                {
                    ElementosPoloPassivoUNICOS.Add(elemento.Text);
                    //LisConsorcioPassivo.Add(elemento);
                    Console.WriteLine($"sou: " + elemento.Text);
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
            List<Polo> PoloLisConsorcioAtivo = new();
            foreach (var DentroPoloAtivo in ElementosPoloAtivoUNICOS)
            {
                if (ProcessoEntidadeRetornado.PoloAtivo == null)
                {
                    ProcessoEntidadeRetornado.PoloAtivo = new Polo(); // ou qualquer método de inicialização apropriado
                }
                if (DentroPoloAtivo.Contains("(AUTOR)"))
                {
                    //LISTA de LISCONSORCIO:
                    if(ContadorPartes > 2)
                    {
                        Console.WriteLine($"Lendo LISCONSORCIO:");

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
                            PoloLisConsorcioAtivo.Add(new Polo
                            {
                                ProcessoId = ProcessoEntidadeRetornado.Id,
                                TipoParte = ProcessoEntidadeRetornado.PoloAtivo.TipoParte,
                                CPFCNPJParte = cnpjParte,
                                NomeParte = RazaoSocial,
                            });
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
                            PoloLisConsorcioAtivo.Add(new Polo
                            {
                                ProcessoId = ProcessoEntidadeRetornado.Id,
                                TipoParte = ProcessoEntidadeRetornado.PoloAtivo.TipoParte,
                                CPFCNPJParte = ParteCpfPolo,
                                NomeParte = NomePartePolo,
                            });
                        }
                    }
                    else
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

                }

                //Ignora Lisconsorcio
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
                    //LISTA de LISCONSORCIO:
                    if (ContadorPartes > 3)
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
                            PoloLisConsorcioAtivo.Add(new Polo
                            {
                                ProcessoId = ProcessoEntidadeRetornado.Id,
                                TipoParte = ProcessoEntidadeRetornado.PoloAtivo.TipoParte,
                                CPFCNPJParte = cnpjParte,
                                NomeParte = RazaoSocial,
                            });
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
                            PoloLisConsorcioAtivo.Add(new Polo
                            {
                                ProcessoId = ProcessoEntidadeRetornado.Id,
                                TipoParte = ProcessoEntidadeRetornado.PoloAtivo.TipoParte,
                                CPFCNPJParte = cpfPolo,
                                NomeParte = nomeReu,
                            });
                        }
                    }
                    //NÃO LISCONSORCIO
                    else
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

                //alimentando os dados de advogados para os polos
                if(PoloLisConsorcioAtivo != null && PoloLisConsorcioAtivo.Count > 0)
                {
                    foreach (var registro in PoloLisConsorcioAtivo)
                    {
                        registro.NomeAdvogado = ProcessoEntidadeRetornado.PoloAtivo.NomeAdvogado;
                        registro.OAB = ProcessoEntidadeRetornado.PoloAtivo.OAB;
                        registro.CPFAdvogado = ProcessoEntidadeRetornado.PoloAtivo.CPFAdvogado;
                        registro.DataCadastro = DateTime.Now;
                        registro.CadastradoPor = 5;
                        registro.DataAtualizacao = DateTime.Now;
                        registro.AtualizadoPor = 5;
                    }
                    Console.WriteLine("teste");
                }

            }

            if(PoloLisConsorcioAtivo != null && PoloLisConsorcioAtivo.Count > 0)
            {

                SalvarDados.MostraDadosPolo(PoloLisConsorcioAtivo);
                Console.WriteLine("TESTE");
            }

            //verificando elementos do polo passivo
            ProcessoEntidadeRetornado.PoloPassivo = new();
            List<Polo> PoloLisConsorcioPassivo = new();
            foreach (var DentroPoloPassivo in ElementosPoloPassivoUNICOS)
            {
                if (ProcessoEntidadeRetornado.PoloPassivo == null)
                {
                    ProcessoEntidadeRetornado.PoloPassivo = new Polo(); // ou qualquer método de inicialização apropriado
                }
                if (DentroPoloPassivo.Contains("(AUTOR)"))
                {
                    //caso tenha mais de um cliente no POLO
                    if(ContadorPartesPassivo > 2)
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
                            PoloLisConsorcioPassivo.Add(new Polo
                            {
                                ProcessoId = ProcessoEntidadeRetornado.Id,
                                TipoParte = ProcessoEntidadeRetornado.PoloPassivo.TipoParte,
                                CPFCNPJParte = cnpjParte,
                                NomeParte = RazaoSocial,
                            });
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

                            PoloLisConsorcioPassivo.Add(new Polo
                            {
                                ProcessoId = ProcessoEntidadeRetornado.Id,
                                TipoParte = ProcessoEntidadeRetornado.PoloPassivo.TipoParte,
                                CPFCNPJParte = ParteCpfPolo,
                                NomeParte = NomePartePolo,
                            });

                        }
                    }
                    //caso não tenha lisconsorcio procederá para sem lista
                    else
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
                    if (ContadorPartesPassivo > 2)
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
                            PoloLisConsorcioPassivo.Add(new Polo
                            {
                                ProcessoId = ProcessoEntidadeRetornado.Id,
                                TipoParte = ProcessoEntidadeRetornado.PoloPassivo.TipoParte,
                                CPFCNPJParte = ParteCpfPolo,
                                NomeParte = NomePartePolo,
                            });
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
                            PoloLisConsorcioPassivo.Add(new Polo
                            {
                                ProcessoId = ProcessoEntidadeRetornado.Id,
                                TipoParte = ProcessoEntidadeRetornado.PoloPassivo.TipoParte,
                                CPFCNPJParte = ParteCpfPolo,
                                NomeParte = NomePartePolo,
                            });
                        }
                    }
                    else
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
                //alimentando os dados de advogados para os polos
                if (PoloLisConsorcioPassivo != null && PoloLisConsorcioPassivo.Count > 0)
                {
                    foreach (var registro in PoloLisConsorcioPassivo)
                    {
                        registro.NomeAdvogado = ProcessoEntidadeRetornado.PoloPassivo.NomeAdvogado;
                        registro.OAB = ProcessoEntidadeRetornado.PoloPassivo.OAB;
                        registro.CPFAdvogado = ProcessoEntidadeRetornado.PoloPassivo.CPFAdvogado;
                        registro.DataCadastro = DateTime.Now;
                        registro.CadastradoPor = 5;
                        registro.DataAtualizacao = DateTime.Now;
                        registro.AtualizadoPor = 5;
                    }
                    Console.WriteLine("teste poloconsorciopassivo");
                }
            }

            ProcessoEntidadeRetornado.PartesProcesso = ProcessoEntidadeRetornado.PoloAtivo.NomeParte + " x " + ProcessoEntidadeRetornado.PoloPassivo.NomeParte;


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
            if (string.IsNullOrEmpty(ProcessoEntidadeRetornado.Nome))
            {
                ProcessoEntidadeRetornado.Nome = "Sem nome";
            }

            List<Cliente> ClientesDoLisConsorcio = new();
            var LocalizaProcessoComId = new Processo();
            Cliente ClienteASerFormado = new();
            List<Cliente> ListaClienteASerFormadoLisConsorcio = new();
            Cliente ClienteDoBanco = new();

            //LisConsorcioPassivo.Count > 0 && PoloLisConsorcioAtivo.Count > 0
            if (ContadorPartes > 2 || ContadorPartesPassivo > 2)
            {
                SalvarDados.MostraDadosPolo(PoloLisConsorcioAtivo);
                SalvarDados.MostraDadosPolo(PoloLisConsorcioPassivo);
                //passivo esta vindo VAZIO
                Console.WriteLine("Fim");
                SalvarDados.MostraDadosProcesso(ProcessoEntidadeRetornado);
                Console.WriteLine("Verifica");
                bool identificacliente = false;
                LocalizaProcessoComId = ConnectDB.LerProcesso(ProcessoEntidadeRetornado.CodPJEC);
                foreach (var registroPoloAtivo in PoloLisConsorcioAtivo)
                {
                    //PoloLisConsorcioAtivo
                    registroPoloAtivo.ProcessoId = LocalizaProcessoComId.Id;
                    if((registroPoloAtivo.CPFAdvogado == "152.489.457-55" || registroPoloAtivo.OAB == "RJ253001") && (registroPoloAtivo.NomeAdvogado == "NATALI CORDEIRO MARQUES"))
                    {
                        identificacliente = true;
                    }
                    else if(registroPoloAtivo.NomeParte == "NATALI CORDEIRO MARQUES" || registroPoloAtivo.CPFCNPJParte == "152.489.457-55")
                    {
                        identificacliente = true;
                    }
                }
                if(identificacliente == true)
                {
                    foreach(var PoloAtivoParaCliente in PoloLisConsorcioAtivo)
                    {
                        Cliente ClienteASerFormadoLisConsorcio = new()
                        {
                            EnderecoId = null,
                            Nome = PoloAtivoParaCliente.NomeParte,
                            Cpf = PoloAtivoParaCliente.CPFCNPJParte,
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
                            Tipo = PoloAtivoParaCliente.TipoParte,
                            ReuAutor = null,
                            DataCadastro = DateTime.Now,
                            CadastradoPor = 5,
                            DataAtualizacao = DateTime.Now,
                            AtualizadoPor = 5
                        };
                        ConnectDB.InserirCliente(ClienteASerFormadoLisConsorcio);
                    }
                }
                identificacliente = false;
                foreach (var registroPoloPassivo in PoloLisConsorcioPassivo)
                {
                    //PoloLisConsorcioPassivo
                    registroPoloPassivo.ProcessoId = LocalizaProcessoComId.Id;
                    if ((registroPoloPassivo.CPFAdvogado == "152.489.457-55" || registroPoloPassivo.OAB == "RJ253001") && (registroPoloPassivo.NomeAdvogado == "NATALI CORDEIRO MARQUES"))
                    {
                        identificacliente = true;
                    }
                    else if (registroPoloPassivo.NomeParte == "NATALI CORDEIRO MARQUES" || registroPoloPassivo.CPFCNPJParte == "152.489.457-55")
                    {
                        identificacliente = true;
                    }
                }


                if (identificacliente == true)
                {
                    foreach (var PoloPassivoParaCliente in PoloLisConsorcioPassivo)
                    {
                        Cliente ClienteASerFormadoLisConsorcio = new()
                        {
                            EnderecoId = null,
                            Nome = PoloPassivoParaCliente.NomeParte,
                            Cpf = PoloPassivoParaCliente.CPFCNPJParte,
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
                            Tipo = PoloPassivoParaCliente.TipoParte,
                            ReuAutor = null,
                            DataCadastro = DateTime.Now,
                            CadastradoPor = 5,
                            DataAtualizacao = DateTime.Now,
                            AtualizadoPor = 5
                        };
                        ConnectDB.InserirCliente(ClienteASerFormadoLisConsorcio);
                    }
                }
                ConnectDB.InserirPolosPartes(PoloLisConsorcioAtivo);
                ConnectDB.InserirPolosPartes(PoloLisConsorcioPassivo);
                //como conseguir distinguir se é cliente ou nao cliente
            }
            else
            {
                LocalizaProcessoComId = ConnectDB.LerProcesso(ProcessoEntidadeRetornado.CodPJEC);
                //Console.WriteLine(LocalizaProcessoComId.CodPJEC);
                Console.WriteLine("\n\nTESTANDO LOCALIZAPROCESSOCOMID:");
                SalvarDados.MostraDadosProcesso(LocalizaProcessoComId);
                Console.WriteLine("\n\n");
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
                ClienteDoBanco = ConnectDB.LerCliente(ClienteASerFormado.Cpf);
                //Console.WriteLine(ClienteDoBanco.Id);
                SalvarDados.MostraDadosProcesso(ProcessoEntidadeRetornado);
                Console.WriteLine("parar");
            }


            //caso não seja lisconsorcio o Polo será assim:
            if(ContadorPartes < 3 && ContadorPartesPassivo < 3)
            {
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
            }




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
            if (AdvogadoDoBanco != null)
            {
                ProcessoEntidadeRetornado.AdvogadoId = AdvogadoDoBanco.Id;
            }

            ProcessoEntidadeRetornado.ClienteId = ClienteDoBanco.Id;
            ProcessoEntidadeRetornado.AdvogadoId = AdvogadoDoBanco.Id;
            ConnectDB.AtualizarProcessoInicial(ProcessoEntidadeRetornado);
            Console.WriteLine($"\n\n\n\n meu clienteid é: {ProcessoEntidadeRetornado.ClienteId} e advogadoid é: {ProcessoEntidadeRetornado.AdvogadoId}");


            //aqui iniciaremos o relacionamento de cliente para processos
            //fazer aqui o relacionamento na tabela ClientePROCESSO, fazer webscrapping em caso de mais de um cliente antes.





            Console.WriteLine("Encerrei");


            //ActionsPJE.EncerrarConsole();


            //SALVAR MOVIMENTAÇÃO PROCESSUAL AQUI E PROCESSO TAMBÉM

            //fecha detalhes
            LinkDetalhesMovimentacaoProcessual.Click();
        }

    }
}
