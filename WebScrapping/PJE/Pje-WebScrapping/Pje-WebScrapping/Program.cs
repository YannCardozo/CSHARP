using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Pje_WebScrapping.Actions;

public class Program
{
    public static void Main()
    {

        string url = "https://tjrj.pje.jus.br/1g/login.seam";
        string elementohtml = "q";



        //Console.WriteLine("Hello, World!");


        ActionsPJE.IniciarPJE(url);
        var retorno =  ActionsPJE.LoginPJE(elementohtml);

        retorno.SendKeys("executar automação");


        //encerra a janela da aplicação
        ActionsPJE.EncerrarPJE();
    }
}




