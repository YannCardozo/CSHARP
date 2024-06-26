﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Pje_WebScrapping.DataStorage;
using Pje_WebScrapping.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pje_WebScrapping.Actions
{
    public static class ActionsPJE
    {
        private static IWebDriver driver;  // Declara uma variável de classe para o driver

        //SEPARAR EM NOVAS CAMADAS DENTRO DE ACTIONS POR FUNÇÕES, EX: METODOS RELACIONADOS AO NAVBAR, CRIAR A CAMADA ACTIONS NAVBAR
        //MÉTODOS DE ACESSO AO PJE

        public static void RetornarIndexPJE(IWebDriver driver)
        {
            //instancia o elemento para receber o botão de redirecionar para a home, no navmenu
            IWebElement ImgPJEHome = driver.FindElement(By.XPath("//*[@id=\"home\"]"));
            Thread.Sleep(1000);
            ImgPJEHome.Click();
            Console.WriteLine("Retornei para a home!");
        }
        public static void EncerrarConsole()
        {
            //encerra o programa TOTALMENTE
            Console.WriteLine("Estou terminando a aplicação agora.");
            Environment.Exit(0);

        }
        public static void EncerrarPJE(IWebDriver driver)
        {
            //fecha a url atual
            driver.Close();
            Console.WriteLine("url fechada.");
            Environment.Exit(0);

        }

        public static string RetornarParaJanelaPrincipal(IWebDriver driver)
        {
            string endereço_do_navegador = driver.CurrentWindowHandle;
            return endereço_do_navegador;
        }

        //estipula um valor em milisecundos de forma randomica, para a thread com mínimo e máximo, algo em torno
        //os extremos.
        public static void AguardarPje(string tempodeespera)
        {
            int tempoDeEsperaDaThread = 0;
            

            if (tempodeespera == "Baixo")
            {
                tempoDeEsperaDaThread = GerarNumeroAleatorio(2000, 3000);
            }
            else if(tempodeespera == "Medio")
            {
                tempoDeEsperaDaThread = GerarNumeroAleatorio(3000, 4000);
            }
            else
            {
                tempoDeEsperaDaThread = GerarNumeroAleatorio(4000, 5000);
            }

            Thread.Sleep(tempoDeEsperaDaThread);
        }

        //gera um valor ( em milisecundos ) , para implementar como tempo de espera da thread em AguardarPje
        public static int GerarNumeroAleatorio(int minValue, int maxValue)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue);
        }






        //objetivo desse método é descer a barra de rolagem de movimentação processual
        //para que carregue os eventos e faça renderizar o resto do processo que falta carregar
        //para permitir o webscrapping raspar as informações corretas
        public static void DescerBarraDeRolagem(IWebDriver driver, string elementId)
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
                while (scrollTop + clientHeight < scrollHeight)
                {
                    Console.WriteLine("Descendo a barra de rolagem...");
                    //AguardarPje("Baixo");
                    scrollTop = (long)js.ExecuteScript("return arguments[0].scrollTop;", element);
                    js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollTop + arguments[0].clientHeight;", element);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro em descer barra de rolagem: " + ex.Message);
            }
        }

        public static string ExtrairNomeDeDetalhes(string texto)
        {
            string padrao = @"([^-\n]+)\s*-\s*";
            Match match = Regex.Match(texto, padrao);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }
            else
            {
                return null;
            }
        }

        public static string ExtrairCPFDeDetalhes(string texto)
        {
            string padrao = @"CPF:\s*([0-9]{3}\.[0-9]{3}\.[0-9]{3}-[0-9]{2})";
            Match match = Regex.Match(texto, padrao);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }
            else
            {
                return null;
            }
        }

        public static string ExtrairOABDeDetalhes(string texto)
        {
            string padrao = @"OAB\s*([A-Z]{2}\d+)";
            Match match = Regex.Match(texto, padrao);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }
            else
            {
                return null;
            }
        }


        public static string ExtrairCNPJDeDetalhes(string texto)
        {
            string padrao = @"CNPJ:\s*([0-9]{2}\.[0-9]{3}\.[0-9]{3}/[0-9]{4}-[0-9]{2})";
            Match match = Regex.Match(texto, padrao);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }
            else
            {
                return null;
            }
        }

        public static DateOnly ConverterFormatoData(string dataString)
        {
            string[] partes = dataString.Split(' ');
            int dia = int.Parse(partes[0]);
            int mes = MesParaNumero(partes[1]);
            int ano = int.Parse(partes[2]);

            // Formando a data no formato desejado
            DateOnly dataConvertida = new DateOnly(ano, mes, dia);

            return dataConvertida;
        }

        public static DateTime ConverterFormatoStringParaDatetime(string dataString)
        {
            string[] partes = dataString.Split(' ');
            int dia = int.Parse(partes[0]);
            int mes = MesParaNumero(partes[1]);
            int ano = int.Parse(partes[2]);

            // Formando a data no formato desejado
            DateTime dataConvertida = new DateTime(ano, mes, dia);

            return dataConvertida;
        }

        public static int MesParaNumero(string mes)
        {
            switch (mes.ToLower())
            {
                case "jan": return 1;
                case "fev": return 2;
                case "mar": return 3;
                case "abr": return 4;
                case "mai": return 5;
                case "jun": return 6;
                case "jul": return 7;
                case "ago": return 8;
                case "set": return 9;
                case "out": return 10;
                case "nov": return 11;
                case "dez": return 12;
                default: return 0; // Mês inválido
            }
        }









        //MÉTODOS DE MANIPULAÇÃO DE ELEMENTOS AO SITE PJE

        public static void EnviarTexto(IWebDriver driver,string elemento, string value, string tipoelemento)
        {
            if(tipoelemento == "id")
            {
                driver.FindElement(By.Id(elemento)).SendKeys(value);
            }
            if(tipoelemento == "Name")
            {
                driver.FindElement(By.Name(elemento)).SendKeys(value);
            }
        }

        //funciona em botoes, checkbox, options e etc.
        public static void Clicar(IWebDriver driver, string elemento,string tipoelemento)
        {
            if (tipoelemento == "id")
            {
                driver.FindElement(By.Id(elemento)).Click();
            }
            if (tipoelemento == "Name")
            {
                driver.FindElement(By.Name(elemento)).Click();
            }
        }

        public static void SelecionarDropDown(IWebDriver driver, string elemento, string value, string tipoelemento)
        {
            if (tipoelemento == "id")
            {
                new SelectElement(driver.FindElement(By.Id(elemento))).SelectByText(value);
            }
            if (tipoelemento == "Name")
            {
                new SelectElement(driver.FindElement(By.Id(elemento))).SelectByText(value);
            }
        }
        public static DateTime? StringParaDatetime(string converterparadatetime)
        {
            if (string.IsNullOrEmpty(converterparadatetime))
            {
                Console.WriteLine("String vazia ou nula recebida.");
                return null;
            }

            if (DateTime.TryParseExact(converterparadatetime.Trim(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataDateTime))
            {
                return dataDateTime;
            }
            else
            {
                Console.WriteLine($"Falha ao converter '{converterparadatetime}' para DateTime.");
                return null;
            }
        }

        public static DateTime? AjustarDataParaSql(DateTime? data)
        {
            if (data == null || data < (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue)
            {
                Console.WriteLine($"Data inválida ou nula recebida: {data}. Ajustando para a data mínima permitida.");
                return (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            }
            return data;
        }
        public static DateTime? DateOnlyToDateTime(DateOnly? dateOnly)
        {
            if (dateOnly.HasValue && dateOnly.Value.ToDateTime(TimeOnly.MinValue) >= SqlDateTime.MinValue.Value)
            {
                return dateOnly.Value.ToDateTime(TimeOnly.MinValue);
            }
            return null;
        }

    }
}
