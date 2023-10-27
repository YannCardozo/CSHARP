using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Pje_WebScrapping.Actions;

public class Program
{
    public static void Main()
    {
        ////objeto driver instanciado, driver será o objeto que fará acesso ao site designado e etc.
        //IWebDriver driver = new ChromeDriver();

        //driver.Navigate().GoToUrl("https://google.com");

        ////driver.Navigate().GoToUrl("https://tjrj.pje.jus.br/1g/login.seam");

        ////instancia um elemento a localizar na pagina web instanciada, nesse exemplo localizamos pelo name "q" ( barra de busca do google )
        //IWebElement elemento = driver.FindElement(By.Name("q"));

        //elemento.SendKeys("Executar automação");

        ////fecha a janela do driver.
        //driver.Close();

        string url = "https://google.com";
        string elementohtml = "q";



        //Console.WriteLine("Hello, World!");


        ActionsPJE.Initialize(url);
        var retorno =  ActionsPJE.GetWebElement(elementohtml);

        retorno.SendKeys("executar automação");


        //encerra a janela da aplicação
        ActionsPJE.CleanUp();
    }
}




