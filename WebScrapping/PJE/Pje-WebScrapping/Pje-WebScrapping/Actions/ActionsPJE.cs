using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Pje_WebScrapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Actions
{
    public static class ActionsPJE
    {
        private static IWebDriver driver;  // Declara uma variável de classe para o driver


        //MÉTODOS DE ACESSO AO PJE

        public static IWebDriver IniciarPJE(string url)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);

            LoginModel LoginPJE = new LoginModel("15248945755", "Gabiroba22@", url);
            Thread.Sleep(4000);

            driver.SwitchTo().Frame(0);
            IWebElement usernameInput = driver.FindElement(By.Id("username"));
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Name("login"));

            usernameInput.SendKeys(LoginPJE.Username);
            passwordInput.SendKeys(LoginPJE.Password);

            loginButton.Click();

            Thread.Sleep(2000);
            //esse comando SAI DO IFRAME.
            driver.SwitchTo().DefaultContent();
            return driver;
        }

        public static void EncerrarPJE()
        {
            driver.Close();
            Console.WriteLine("url fechada");
        }



        public static IWebElement LoginPJE(string tagdoelemento)
        {
            // Certifique-se de que o driver tenha sido inicializado antes de usá-lo
            if (driver == null)
            {
                throw new InvalidOperationException("O driver não foi inicializado. Chame Initialize antes de usar GetWebElement.");
            }

            // Use o driver para encontrar o elemento desejado
            //IWebElement elemento = driver.FindElement(By.Name(tagdoelemento));

            IWebElement revealed = driver.FindElement(By.Id("revealed"));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
            {
                PollingInterval = TimeSpan.FromMilliseconds(300),
            };
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));

            driver.FindElement(By.Id("reveal")).Click();
            wait.Until(d => {
                revealed.SendKeys("Displayed");
                return true;
            });

            IWebElement added = driver.FindElement(By.Id("box0"));

            return revealed;
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
