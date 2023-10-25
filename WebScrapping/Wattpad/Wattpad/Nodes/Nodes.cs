using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Wattpad.DTO;

namespace Wattpad.Nodes
{
    public static class Nodes
    {
        public static void NodeV1()
        {
            var html = new HtmlWeb();

            var pagina_html = html.Load("https://www.expressvpn.com/sign-in");

            var node = pagina_html.DocumentNode.SelectSingleNode("//*[@id=\"signin-form\"]/input[2]").Attributes["value"].Value;

            Console.WriteLine("Token: " + node);


            Console.ReadKey();
        }
        public static void NodeV3()
        {
            //Console.WriteLine("aaa");
            HtmlWeb web = new HtmlWeb();

            //Exemplo site example:

            //HtmlDocument document = web.Load("https://example.com/");

            //var title = document.DocumentNode.SelectNodes("//div/h1").First().InnerText;
            //var description = document.DocumentNode.SelectNodes("//div/p").First().InnerText;

            //Console.WriteLine(title);
            //Console.WriteLine();
            //Console.WriteLine(description);

            //Exemplo WIKIPEDIA:

            HtmlDocument document = web.Load("https://pt.wikipedia.org/wiki/HTML");
            var titulo = document.DocumentNode.SelectNodes("//*[@id=\"firstHeading\"]").First().InnerText;
            var texto = document.DocumentNode.SelectNodes("//*[@id=\"mw-content-text\"]/div[1]/p");

            Console.WriteLine(titulo);
            Console.WriteLine();
            texto.ToList().ForEach(i => Console.WriteLine(i.InnerText));
        }
    }
}
