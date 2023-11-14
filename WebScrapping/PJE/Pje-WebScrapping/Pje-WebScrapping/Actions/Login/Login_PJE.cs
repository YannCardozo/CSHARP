using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Pje_WebScrapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Actions.Login
{
    public class Login_PJE
    {
        private static IWebDriver driver;
        public static IWebDriver IniciarPJE(string url)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);

            LoginModel LoginPJE = new LoginModel("15248945755", "Gabiroba22@", url);
            Thread.Sleep(4000);

            //esse comando ACESSSA o IFRAME dentro do html, que é um elemento dentro da tela de login que precisa ser acessado dessa forma.
            driver.SwitchTo().Frame(0);
            IWebElement usernameInput = driver.FindElement(By.Id("username"));
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Name("login"));

            usernameInput.SendKeys(LoginPJE.Username);
            Thread.Sleep(1000);
            passwordInput.SendKeys(LoginPJE.Password);
            Thread.Sleep(1000);
            loginButton.Click();

            //esse comando SAI DO IFRAME e retorna para a tela normal do pje.
            driver.SwitchTo().DefaultContent();
            Console.WriteLine("Loguei no PJE");
            return driver;
        }
    }
}
