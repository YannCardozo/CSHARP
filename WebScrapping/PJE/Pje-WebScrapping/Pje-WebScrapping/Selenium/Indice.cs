using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Selenium
{
    public class Indice
    {
        public static void MostrarFuncoesSelenium()
        {


            //tags facilitadoras:


            //IWEBELEMENT

            //i.ID – driver.findElement(By.id("IdName"))
            //ii.Name – driver.findElement(By.name("Name"))
            //iii.Class Name – driver.findElement(By.className("Element Class"))
            //iv.Tag Name – driver.findElement(By.tagName("HTML Tag Name"))
            //v.Link Text – driver.findElement(By.linkText("LinkText"))
            //vi.Partial Link Text – driver.findElement(By.partialLinkText("partialLinkText"))
            //vii.CSS Selector – driver.findElement(By.cssSelector(“value”))
            //viii.XPath – driver.findElement(By.xpath("XPath"))


            //IFRAME : 


            //driver.SwitchTo().Frame(0);  <- Esse comando entra para o IFRAME desejado na pagina html.
            //driver.SwitchTo().DefaultContent(); <- Sai do Iframe e volta ao navegador normal.

            //IWEBDRIVER

            //instanciar objeto do navegador, abrindo o navegador
            IWebDriver Teste = new ChromeDriver();

            //faz o navegador direcionar para determinada url
            Teste.Navigate().GoToUrl("");

            //maximiza a tela do navegador
            Teste.Manage().Window.Maximize();

            //faz o navegador voltar para a ultima pagina acessada
            Teste.Navigate().Back();

            //faz o navegador avançar para a próxima página acessada
            Teste.Navigate().Forward();

            //faz o navegador dar reload na página, recarregar.
            Teste.Navigate().Refresh();

            //Fecha o navegador
            Teste.Close();
        }
    }
}
