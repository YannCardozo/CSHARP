using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

            for (int i = 0; i < contadorderegistrosrepresentanteprocessual.Count; i++)
            {
                if (i % 2 == 1)
                {
                    IWebElement registro = contadorderegistrosrepresentanteprocessual[i];
                    string registroText = registro.Text;
                    Console.WriteLine("Valor numérico do registro: " + registroText);
                    Thread.Sleep(1000);

                    //convertendo o valor da string em registrotext em numero inteiro , CASO VALIDO.
                    var numero = int.TryParse(registroText, out int temnotificacao);

                    //verificando se o numero convertido é maior que zero, ou seja, se existe notificação no span ou não.
                    if (temnotificacao > 0)
                    {

                        //verificar como criar um for de maneira generica que acesse todos os processos que sejam > 0 , e retorne todos eles como dados para enfim , preenchimento no banco.
                        //talvez criar um novo método e chamar aqui seja o ideal.
                        // Agora você pode usar 'numero' como um valor inteiro
                        //Console.WriteLine("Número como inteiro: " + numero);
                        //PAINELACTIONPROCESSOS(driver, contadorderegistrosrepresentanteprocessual);

                        Thread.Sleep(1000);
                        registro.Click();
                        Thread.Sleep(1000);





                        IWebElement elementoEncontrado = null;
                        for (int j = 0; j < i; j++)
                        {
                            if(elementoEncontrado == null && i < contadorderegistrosrepresentanteprocessual.Count)
                            {
                                IWebElement nometarefa = contadorderegistrosrepresentanteprocessual[i];
                                string titleAttribute = nometarefa.GetAttribute("title");

                                if (titleAttribute != null && titleAttribute.Equals("Clique para abrir este agrupador"))
                                {
                                    elementoEncontrado = nometarefa;
                                    Console.WriteLine(elementoEncontrado + "  " + elementoEncontrado.Text);



                                    Thread.Sleep(2000);
                                }
                            }
                        }
                        
                        Thread.Sleep(2000);



                        //SIMULE NOVAMENTE E OBSERVE EM INSPECIONAR ELEMENTO, E TENTE PUXAR PELA PROPRIEDADE TITLE,
                        //title="Clique para abrir este agrupador"


                        Thread.Sleep(2000);
                            //submenucomnotificacao.Click();
                            Thread.Sleep(2000);
                            break;
                            //IWebElement nometarefadois = contadorderegistrosrepresentanteprocessual[i];
                            //IWebElement nometarefa = contadorderegistrosrepresentanteprocessual[j];

                            //Console.WriteLine(nometarefa.Text);
                            //Console.WriteLine(nometarefa.TagName);
                            //Thread.Sleep(2000);

                            //Console.WriteLine(nometarefa.Text);
                            //Console.WriteLine(nometarefa.TagName);
                        



                        //IWebElement teste = contadorderegistrosrepresentanteprocessual[i + 1];
                        //Console.WriteLine(teste.Text);
                        //Console.WriteLine(teste.TagName);
                        //Thread.Sleep(1000);
                        //nometarefa.Click();

                    }
                    else
                    {
                        Console.WriteLine("Não tem notificação em: " + registro.TagName + " com: " + registroText);
                    }
                }
            }

        }



        //public static void PAINELACTIONPROCESSOS(IWebDriver driver, IList<IWebElement> spanscomnotificacao)
        //{
        //    foreach (var iten in spanscomnotificacao)
        //    {

        //    }

        //}

    }
}
