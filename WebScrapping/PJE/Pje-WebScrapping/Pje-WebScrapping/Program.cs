using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V116.SystemInfo;
using OpenQA.Selenium.Support.UI;
using Pje_WebScrapping;
using Pje_WebScrapping.Actions;
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

        IWebDriver Driver =  ActionsPJE.IniciarPJE("https://tjrj.pje.jus.br/1g/login.seam");





        //VERIFICAR SE VAI PRECISAR SAIR DO FRAME OU NAO AO CONSEGUIR LOGAR.

        Thread.Sleep(4000);
        ActionsPJE.EncerrarPJE();

    }
}
