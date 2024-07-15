using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V116.SystemInfo;
using OpenQA.Selenium.Support.UI;
using Pje_WebScrapping;
using Pje_WebScrapping.Actions;
using Pje_WebScrapping.Actions.Login;
using Pje_WebScrapping.Actions.NavBarMenu;
using Pje_WebScrapping.Actions.Push;
using Pje_WebScrapping.Models;
using System;
using System.Diagnostics;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

public class Program
{
    public static void Main()
    {
        //string url_PJE = "https://tjrj.pje.jus.br/1g/login.seam";

        IWebDriver Driver =  Login_PJE.IniciarPJE("https://tjrj.pje.jus.br/1g/login.seam");

        Driver = NavBarActions.InicializaPush(Driver);
        ActionsPJE.AguardarPje("Baixo");

        Push.PushProcessos(Driver);









        //TESTE
        ActionsPJE.AguardarPje("Baixo");



        //TESTE
        ActionsPJE.EncerrarPJE(Driver);


        //VERIFICAR SE VAI PRECISAR SAIR DO FRAME OU NAO AO CONSEGUIR LOGAR.

        ActionsPJE.AguardarPje("Alto");
        //Thread.Sleep(4000);

        // ActionsPJE.RetornarIndexPJE(Driver);
        //Thread.Sleep(1000);
        NavBarActions.AcessarMenuNavPJE(Driver);
        ActionsPJE.AguardarPje("Baixo");
        //Thread.Sleep(1000);

        //acessa o menu que contem value PAINEL , INDO PARA PAINELACTION DPS.
        NavBarActions.ListaMenuOpcoesNavBar(Driver,"PAINEL");
        ActionsPJE.AguardarPje("Baixo");
        //Thread.Sleep(1000);
        //ActionsPJE.EncerrarPJE(Driver);

        
    }
}
