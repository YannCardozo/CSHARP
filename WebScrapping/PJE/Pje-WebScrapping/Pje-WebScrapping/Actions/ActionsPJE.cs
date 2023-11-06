using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Pje_WebScrapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

        public static void EncerrarPJE(IWebDriver driver)
        {
            driver.Close();
            Console.WriteLine("url fechada");
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

    }
}
