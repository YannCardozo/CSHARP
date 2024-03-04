using OpenQA.Selenium;
using OpenQA.Selenium.Chrome ;
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");

        IWebDriver driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://tjrj.pje.jus.br/1g/login.seam");
        driver.FindElement(By.Id("kc-form-login"));
        //driver.FindElement(By.Id("kc-form-login")).SendKeys("teste");
        driver.FindElement(By.Name("password")).SendKeys("teste");

    }
}