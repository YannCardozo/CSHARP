using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Program
{
    public static void Main()
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
