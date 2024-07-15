using Justo.Entities.Entidades;
using OpenQA.Selenium;
using Pje_WebScrapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Actions.Push
{
    public class PushActions
    {
        public static void DescerBarraDeRolagemPUSH(IWebDriver driver, string elementId)
        {
            try
            {
                // Executar JavaScript para rolar a barra de rolagem para o máximo inferior possível
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                // Obter o elemento pelo ID
                IWebElement element = driver.FindElement(By.Id(elementId));

                // Altura inicial da barra de rolagem
                long scrollTop = 0;

                // Altura total da barra de rolagem
                long scrollHeight = (long)js.ExecuteScript("return arguments[0].scrollHeight;", element);

                // Altura visível do elemento
                long clientHeight = (long)js.ExecuteScript("return arguments[0].clientHeight;", element);

                // Loop até que a barra de rolagem atinja o final
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Descendo a barra de rolagem...");
                        scrollTop = (long)js.ExecuteScript("return arguments[0].scrollTop;", element);
                        js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollTop + arguments[0].clientHeight;", element);

                        // Espera um curto período para o JavaScript executar a rolagem
                        Thread.Sleep(500);

                        // Atualiza os valores após a rolagem
                        long newScrollTop = (long)js.ExecuteScript("return arguments[0].scrollTop;", element);
                        Console.WriteLine(newScrollTop.ToString() + scrollTop.ToString());
                        if (newScrollTop == scrollTop)
                        {
                            Console.WriteLine(newScrollTop);
                            Console.WriteLine(scrollTop);
                            break;
                        }
                        scrollTop = newScrollTop;
                    }
                    catch (StaleElementReferenceException)
                    {
                        Console.WriteLine("Elemento obsoleto encontrado. Tentando obter o elemento novamente...");
                        element = driver.FindElement(By.Id(elementId));
                        scrollHeight = (long)js.ExecuteScript("return arguments[0].scrollHeight;", element);
                        clientHeight = (long)js.ExecuteScript("return arguments[0].clientHeight;", element);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro em descer barra de rolagem: " + ex.Message);
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

        public static void MostraDadosProcesso(Processo ProcessoEntidadeParaImprimir)
        {
            foreach (var propriedade in typeof(Processo).GetProperties())
            {
                //listando atributos do objeto

                var valor = propriedade.GetValue(ProcessoEntidadeParaImprimir);
                Console.WriteLine($"{propriedade.Name}: {valor}");

            }
        }
        public static void MostraDadosCliente(Cliente ClienteEntidadeParaImprimir)
        {
            Console.WriteLine($"Lendo CLIENTE: {ClienteEntidadeParaImprimir.Cpf}\n\n");
            foreach (var propriedade in typeof(Cliente).GetProperties())
            {
                var valor = propriedade.GetValue(ClienteEntidadeParaImprimir);
                Console.WriteLine($"{propriedade.Name}: {valor}");

            }
        }

    }
}
