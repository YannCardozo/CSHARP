using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V116.SystemInfo;
using OpenQA.Selenium.Support.UI;
using Pje_WebScrapping;
using Pje_WebScrapping.Actions;
using Pje_WebScrapping.Actions.Login;
using Pje_WebScrapping.Actions.NavBarMenu;
using Pje_WebScrapping.Models;
using System;
using System.Diagnostics;
using System.Xml.Linq;

public class Program
{
    public static void Main()
    {
        //string url_PJE = "https://tjrj.pje.jus.br/1g/login.seam";

        IWebDriver Driver =  Login_PJE.IniciarPJE("https://tjrj.pje.jus.br/1g/login.seam");





        //VERIFICAR SE VAI PRECISAR SAIR DO FRAME OU NAO AO CONSEGUIR LOGAR.

        Thread.Sleep(4000);

       // ActionsPJE.RetornarIndexPJE(Driver);
        //Thread.Sleep(1000);
        NavBarActions.AcessarMenuNavPJE(Driver);
        Thread.Sleep(1000);
        NavBarActions.ListaMenuOpcoesNavBar(Driver,"PAINEL");
        Thread.Sleep(1000);
        //ActionsPJE.EncerrarPJE(Driver);

    }
}
