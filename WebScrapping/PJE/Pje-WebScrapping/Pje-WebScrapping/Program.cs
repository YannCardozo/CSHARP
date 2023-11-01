using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V116.SystemInfo;
using OpenQA.Selenium.Support.UI;
using Pje_WebScrapping;
using System;
using System.Diagnostics;
using System.Xml.Linq;

public class Program
{
    public static void Main()
    {

        LoginPage teste = new LoginPage();
        teste.ExecuteLogin();


        //    IWebDriver Driver = new ChromeDriver();
        //    Driver.Navigate().GoToUrl("https://tjrj.pje.jus.br/1g/login.seam");


        //    Thread.Sleep(4000);

        //    // Espera até que o elemento de entrada do login esteja presente
        //    var usernameInput = Driver.FindElement(By.Id("username"));

        //    // Espera até que o elemento de entrada da senha esteja presente
        //    var passwordInput = Driver.FindElement(By.Id("password"));

        //    // Espera até que o botão de login esteja presente
        //    var loginButton = Driver.FindElement(By.Name("login"));

        //    // Agora que os elementos estão presentes, você pode interagir com eles
        //    usernameInput.SendKeys("Teste");
        //    passwordInput.SendKeys("teste");
        //    loginButton.Click();
        //





    }
}
